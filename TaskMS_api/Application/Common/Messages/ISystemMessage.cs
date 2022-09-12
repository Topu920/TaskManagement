namespace Application.Common.Messages
{
    public interface ISystemMessage
    {
        Guid Id { get; set; }
        string Title { get; set; }
        string? Body { get; set; }
        string MessageType { get; set; }
    }
}
