using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.MessagePack;

class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        var endpointConfiguration = new EndpointConfiguration("MessagePackSerializerSample");
        endpointConfiguration.UseSerialization<MessagePackSerializer>();
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.UseTransport<LearningTransport>();

        var endpoint = await Endpoint.Start(endpointConfiguration);
        try
        {
            var message = new MyMessage
            {
                DateSend = DateTime.Now,
            };
            await endpoint.SendLocal(message);
            Console.WriteLine("\r\nPress any key to stop program\r\n");
            Console.Read();
        }
        finally
        {
            await endpoint.Stop();
        }
    }
}