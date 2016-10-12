using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace NServiceBus.MessagePack
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using MsgPack.Serialization;
    using Serialization;

    class MessageSerializer : IMessageSerializer
    {
        SerializationContext context;
        ConcurrentDictionary<RuntimeTypeHandle, Func<object>> emptyTypesBag = new ConcurrentDictionary<RuntimeTypeHandle, Func<object>>();

        public MessageSerializer(string contentType, SerializationContext context)
        {
            if (context == null)
            {
                this.context = SerializationContext.Default;
            }
            else
            {
                this.context = context;
            }
            if (contentType == null)
            {
                ContentType = "messagepack";
            }
            else
            {
                ContentType = contentType;
            }
        }

        public void Serialize(object message, Stream stream)
        {
            var messageType = message.GetType();
            if (messageType.Name.EndsWith("__impl"))
            {
                throw new Exception("Interface based message are not supported. Create a class that implements the desired interface.");
            }

            if (messageType.IsScheduleTask())
            {
                var wrapperSerializer = context.GetSerializer<ScheduledTaskWrapper>();
                wrapperSerializer.Pack(stream, ScheduledTaskHelper.ToWrapper(message));
                return;
            }


            var handle = messageType.TypeHandle;
            if (emptyTypesBag.ContainsKey(handle))
            {
                return;
            }
            MsgPack.Serialization.MessagePackSerializer serializer;
            try
            {
                serializer = context.GetSerializer(messageType);
            }
            catch (SerializationException exception)
                when (IsEmptyTypeException(exception))
            {
                stream.WriteByte(0);
                emptyTypesBag[handle] = ConstructorDelegateBuilder.BuildConstructorFunc(messageType);
                return;
            }
            serializer.Pack(stream, message);
        }

        static bool IsEmptyTypeException(SerializationException exception)
        {
            return exception.Message.Contains("because it does not have any serializable fields nor properties.");
        }

        public object[] Deserialize(Stream stream, IList<Type> messageTypes)
        {
            var deserializeInner = DeserializeInner(stream, messageTypes);
            return new[]
            {
                deserializeInner
            };
        }

        object DeserializeInner(Stream stream, IList<Type> messageTypes)
        {
            var messageType = messageTypes.First();


            if (messageType.IsScheduleTask())
            {
                var wrapperSerializer = context.GetSerializer<ScheduledTaskWrapper>();
                var scheduledTaskWrapper = wrapperSerializer.Unpack(stream);
                return ScheduledTaskHelper.FromWrapper(scheduledTaskWrapper);
            }


            var typeHandle = messageType.TypeHandle;
            Func<object> constructor;
            if (emptyTypesBag.TryGetValue(typeHandle, out constructor))
            {
                return constructor();
            }
            MsgPack.Serialization.MessagePackSerializer serializer;
            try
            {
                serializer = context.GetSerializer(messageType);
            }
            catch (SerializationException exception)
                when (IsEmptyTypeException(exception))
            {
                constructor = emptyTypesBag.GetOrAdd(
                    key: typeHandle,
                    valueFactory: handle => ConstructorDelegateBuilder.BuildConstructorFunc(messageType));
                return constructor();
            }

            return serializer.Unpack(stream);
        }

        public string ContentType { get; }
    }
}