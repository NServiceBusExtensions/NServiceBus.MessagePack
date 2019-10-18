using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NServiceBus.Serialization;

class MessageSerializer :
    IMessageSerializer
{
    IFormatterResolver resolver;

    public MessageSerializer(string contentType, IFormatterResolver resolver)
    {
        if (resolver == null)
        {
            this.resolver = MessagePackSerializer.DefaultResolver;
        }
        else
        {
            this.resolver = resolver;
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

        MessagePackSerializer.NonGeneric.Serialize(message.GetType(), stream, message, resolver);
    }

    public object[] Deserialize(Stream stream, IList<Type> messageTypes)
    {
        return new[]
        {
            DeserializeInner(stream, messageTypes)
        };
    }

    object DeserializeInner(Stream stream, IList<Type> messageTypes)
    {
        var messageType = messageTypes.First();
        return MessagePackSerializer.NonGeneric.Deserialize(messageType, stream, resolver);
    }

    public string ContentType { get; }
}