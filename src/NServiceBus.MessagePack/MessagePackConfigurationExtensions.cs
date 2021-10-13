using MessagePack;
using NServiceBus.Configuration.AdvancedExtensibility;
using NServiceBus.Serialization;
using NServiceBus.Settings;

namespace NServiceBus;

/// <summary>
/// Extensions for <see cref="SerializationExtensions{T}"/> to manipulate how messages are serialized.
/// </summary>
public static class MessagePackConfigurationExtensions
{
    /// <summary>
    /// Configures the <see cref="MessagePackSerializerOptions"/> to use.
    /// </summary>
    /// <param name="config">The <see cref="SerializationExtensions{T}"/> instance.</param>
    /// <param name="options">The <see cref="MessagePackSerializerOptions"/> to use.</param>
    public static void Options(this SerializationExtensions<MessagePack.MessagePackSerializer> config, MessagePackSerializerOptions options)
    {
        var settings = config.GetSettings();
        settings.Set(options);
    }

    internal static MessagePackSerializerOptions GetOptions(this ReadOnlySettings settings)
    {
        return settings.GetOrDefault<MessagePackSerializerOptions>();
    }

    /// <summary>
    /// Configures string to use for <see cref="Headers.ContentType"/> headers.
    /// </summary>
    /// <remarks>
    /// Defaults to "messagepack".
    /// </remarks>
    /// <param name="config">The <see cref="SerializationExtensions{T}"/> instance.</param>
    /// <param name="contentTypeKey">The content type key to use.</param>
    public static void ContentTypeKey(this SerializationExtensions<MessagePack.MessagePackSerializer> config, string contentTypeKey)
    {
        Guard.AgainstEmpty(contentTypeKey, nameof(contentTypeKey));
        var settings = config.GetSettings();
        settings.Set("NServiceBus.MessagePack.ContentTypeKey", contentTypeKey);
    }

    internal static string GetContentTypeKey(this ReadOnlySettings settings)
    {
        return settings.GetOrDefault<string>("NServiceBus.MessagePack.ContentTypeKey");
    }
}