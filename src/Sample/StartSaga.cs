using NServiceBus;

public class StartSaga :
    IMessage
{
    public Guid TheId { get; set; }
}