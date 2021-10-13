using NServiceBus;

public class CompleteSaga :
    IMessage
{
    public Guid TheId { get; set; }
}