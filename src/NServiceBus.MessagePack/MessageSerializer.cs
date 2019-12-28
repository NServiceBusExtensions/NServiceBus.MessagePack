using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NServiceBus.Serialization;

class MessageSerializer :
    IMessageSerializer
{
    MessagePackSerializerOptions options;

    public MessageSerializer(string contentType, MessagePackSerializerOptions options)
    {
        if (options == null)
        {
            this.options = MessagePackSerializerOptions.Standard;
        }
        else
        {
            this.options = options;
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

        MessagePackSerializer.Serialize(message.GetType(), stream, message, options);
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
        return MessagePackSerializer.Deserialize(messageType, stream, options);
    }

    public string ContentType { get; }
}