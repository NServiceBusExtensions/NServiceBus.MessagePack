namespace NServiceBus.MessagePack
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using MsgPack.Serialization;
    using NServiceBus.Serialization;

    /// <summary>
    /// MessagePack serializer.
    /// </summary>
    public class MessageSerializer : IMessageSerializer
    {
        
        /// <summary>
        /// <see cref="IMessageSerializer.Serialize"/>
        /// </summary>
        public void Serialize(object message, Stream stream)
        {
            var messageType = message.GetType();
            if (messageType.Name.EndsWith("__impl"))
            {
                throw new Exception("Interface based message are not currently supported. Create a class that implements your desired interface. If you want to send an interface feel free to send a pull request.");
            }
            
            var serializer = SerializationContext.Default.GetSerializer(messageType);
            serializer.Pack(stream, message);
        }

        /// <summary>
        /// <see cref="IMessageSerializer.Deserialize"/>
        /// </summary>
        public object[] Deserialize(Stream stream, IList<Type> messageTypes)
        {
            var serializer = SerializationContext.Default.GetSerializer(messageTypes.First());
            var unpacked = serializer.Unpack(stream);
            return new[]
                   {
                       unpacked
                   };
        }

        /// <summary>
        /// Gets the content type into which this serializer serializes the content to 
        /// </summary>
        public string ContentType
        {
            get { return "MessagePack"; }
        }

    }
}