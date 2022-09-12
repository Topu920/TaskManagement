using Application.Common.Messages;

namespace Application.Common.Results
{
    public class BaseResult : IBaseResult, ISystemMessageList<ISystemMessage>
    {
        public List<ISystemMessage> Messages { get; set; } = null!;

        public bool Success { get; set; }

        public void AddErrorMessage(string message)
        {
            this.Messages.Add(new SystemMessage
            {
                Title = message,
                MessageType = SystemMessageType.Error.ToString()
            });
        }

        public void AddInfoMessage(string message)
        {
            this.Messages.Add(new SystemMessage
            {
                Title = message,
                MessageType = SystemMessageType.Info.ToString()
            });
        }

        public void AddMessage(ISystemMessage message)
        {
            this.Messages.Add(message);
        }

        public void AddSuccessMessage(string message)
        {
            this.Messages.Add(new SystemMessage
            {
                Title = message,
                MessageType = SystemMessageType.Success.ToString()
            });
        }

        public void AddWarningMessage(string message)
        {
            this.Messages.Add(new SystemMessage
            {
                Title = message,
                MessageType = SystemMessageType.Warning.ToString()
            });
        }
    }
}
