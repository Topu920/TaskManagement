namespace Application.Common.Messages
{
    public class SystemMessage : ISystemMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string? Body { get; set; }
        public string MessageType { get; set; } = null!;
    }
}
