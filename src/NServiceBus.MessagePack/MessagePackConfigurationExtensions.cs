using MessagePack;
using NServiceBus.Configuration.AdvancedExtensibility;
using NServiceBus.Serialization;
using NServiceBus.Settings;

namespace NServiceBus
{
    /// <summary>
    /// Extensions for <see cref="SerializationExtensions{T}"/> to manipulate how messages are serialized.
    /// </summary>
    public static class MessagePackConfigurationExtensions
    {
        /// <summary>
        /// Configures the <see cref="IFormatterResolver"/> to use.
        /// </summary>
        /// <param name="config">The <see cref="SerializationExtensions{T}"/> instance.</param>
        /// <param name="resolver">The <see cref="IFormatterResolver"/> to use.</param>
        public static void Resolver(this SerializationExtensions<MessagePack.MessagePackSerializer> config, IFormatterResolver resolver)
        {
            Guard.AgainstNull(config, nameof(config));
            Guard.AgainstNull(resolver, nameof(resolver));
            var settings = config.GetSettings();
            settings.Set(resolver);
        }

        internal static IFormatterResolver GetResolver(this ReadOnlySettings settings)
        {
            return settings.GetOrDefault<IFormatterResolver>();
        }

        /// <summary>
        /// Configures string to use for <see cref="Headers.ContentType"/> headers.
        /// </summary>
        /// <remarks>
        /// Defaults to "wire".
        /// </remarks>
        /// <param name="config">The <see cref="SerializationExtensions{T}"/> instance.</param>
        /// <param name="contentTypeKey">The content type key to use.</param>
        public static void ContentTypeKey(this SerializationExtensions<MessagePack.MessagePackSerializer> config, string contentTypeKey)
        {
            Guard.AgainstNull(config, nameof(config));
            Guard.AgainstNullOrEmpty(contentTypeKey, nameof(contentTypeKey));
            var settings = config.GetSettings();
            settings.Set("NServiceBus.MessagePack.ContentTypeKey", contentTypeKey);
        }

        internal static string GetContentTypeKey(this ReadOnlySettings settings)
        {
            return settings.GetOrDefault<string>("NServiceBus.MessagePack.ContentTypeKey");
        }
    }
}