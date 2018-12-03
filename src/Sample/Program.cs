using System;
using System.Threading.Tasks;
using MessagePack.Resolvers;
using NServiceBus;
using NServiceBus.MessagePack;

class Program
{
    static async Task Main()
    {
        var configuration = new EndpointConfiguration("MessagePackSerializerSample");
        var serialization = configuration.UseSerialization<MessagePackSerializer>();
        serialization.Resolver(ContractlessStandardResolver.Instance);
        configuration.UsePersistence<InMemoryPersistence>();
        configuration.UseTransport<LearningTransport>();

        var endpoint = await Endpoint.Start(configuration);
        var message = new MyMessage
        {
            DateSend = DateTime.Now,
        };
        await endpoint.SendLocal(message);
        var startSaga = new StartSaga
        {
            TheId = Guid.NewGuid(),
        };
        await endpoint.SendLocal(startSaga);

        Console.WriteLine("Press any key to stop program");
        Console.Read();
        await endpoint.Stop();
    }
}