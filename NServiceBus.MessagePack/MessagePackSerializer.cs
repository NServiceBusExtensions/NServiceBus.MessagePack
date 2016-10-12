using NServiceBus.MessageInterfaces;
using NServiceBus.Settings;
using System;
using NServiceBus.Serialization;

namespace NServiceBus.MessagePack
{

    /// <summary>
    /// Defines the capabilities of the MessagePack serializer
    /// </summary>
    public class MessagePackSerializer : SerializationDefinition
    {

        /// <summary>
        /// <see cref="SerializationDefinition.Configure"/>
        /// </summary>
        public override Func<IMessageMapper, IMessageSerializer> Configure(ReadOnlySettings settings)
        {
            return mapper =>
            {
                var context = settings.GetContext();
                var contentTypeKey = settings.GetContentTypeKey();
                return new MessageSerializer(contentTypeKey, context);
            };
        }
    }
}