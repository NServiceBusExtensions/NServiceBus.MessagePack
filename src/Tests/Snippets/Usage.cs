using MessagePack.Resolvers;
using NServiceBus;
using MessagePackSerializer = NServiceBus.MessagePack.MessagePackSerializer;

class Usage
{
    Usage(EndpointConfiguration configuration)
    {
        #region MessagePackSerialization

        configuration.UseSerialization<MessagePackSerializer>();

        #endregion
    }

    void CustomSettings(EndpointConfiguration configuration)
    {
        #region MessagePackResolver

        var serialization = configuration.UseSerialization<MessagePackSerializer>();
        serialization.Resolver(ContractlessStandardResolver.Instance);

        #endregion
    }

    void ContentTypeKey(EndpointConfiguration configuration)
    {
        #region MessagePackContentTypeKey

        var serialization = configuration.UseSerialization<MessagePackSerializer>();
        serialization.ContentTypeKey("custom-key");

        #endregion
    }

}
