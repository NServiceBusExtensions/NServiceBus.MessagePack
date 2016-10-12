using System;

namespace NServiceBus.MessagePack
{
    public class ScheduledTaskWrapper
    {
        public Guid TaskId { get; set; }
        public string Name { get; set; }
        public TimeSpan Every { get; set; }
    }
}