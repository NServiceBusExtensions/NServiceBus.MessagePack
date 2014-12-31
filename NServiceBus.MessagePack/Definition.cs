namespace NServiceBus.MessagePack
{
    using System;
    using NServiceBus.Serialization;

    /// <summary>
    /// Defines the capabilities of the MessagePack serializer
    /// </summary>
    public class MessagePackSerializer : SerializationDefinition
    {
        /// <summary>
        /// <see cref="SerializationDefinition.ProvidedByFeature"/>
        /// </summary>
        protected override Type ProvidedByFeature()
        {
            return typeof(SerializationFeature);
        }
    }

}
