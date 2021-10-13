using NServiceBus;

public class MySagaData :
    IContainSagaData
{
    public Guid Id { get; set; }
    public string? Originator { get; set; }
    public string? OriginalMessageId { get; set; }
    public Guid TheId { get; set; }
}