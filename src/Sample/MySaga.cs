using NServiceBus;
using NServiceBus.Logging;

public class MySaga :
    Saga<MySagaData>,
    IAmStartedByMessages<StartSaga>,
    IHandleMessages<CompleteSaga>,
    IHandleTimeouts<CancelSaga>
{
    static ILog log = LogManager.GetLogger<MySaga>();

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
    {
        mapper.ConfigureMapping<StartSaga>(message => message.TheId)
            .ToSaga(sagaData => sagaData.TheId);
        mapper.ConfigureMapping<CompleteSaga>(message => message.TheId)
            .ToSaga(sagaData => sagaData.TheId);
    }

    public async Task Handle(StartSaga message, IMessageHandlerContext context)
    {
        Data.TheId = message.TheId;
        var completeOrder = new CompleteSaga
        {
            TheId = Data.TheId
        };
        var sendOptions = new SendOptions();
        sendOptions.DelayDeliveryWith(TimeSpan.FromSeconds(1));
        sendOptions.RouteToThisEndpoint();
        await context.Send(completeOrder, sendOptions)
            .ConfigureAwait(false);

        var timeout = DateTime.UtcNow.AddSeconds(3);
        await RequestTimeout<CancelSaga>(context, timeout)
            .ConfigureAwait(false);
    }

    public Task Handle(CompleteSaga message, IMessageHandlerContext context)
    {
        log.Info($"CompleteSaga received with TheId {message.TheId}");
        MarkAsComplete();
        return Task.CompletedTask;
    }

    public Task Timeout(CancelSaga state, IMessageHandlerContext context)
    {
        log.Info($"CompleteSaga not received soon enough TheId {Data.TheId}. Calling MarkAsComplete");
        MarkAsComplete();
        return Task.CompletedTask;
    }
}