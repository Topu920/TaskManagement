namespace Application.Common.Messages
{
    public interface ISystemMessageList<in T> where T : class
    {
        void AddMessage(T message);
        void AddErrorMessage(string message);
        void AddInfoMessage(string message);
        void AddSuccessMessage(string message);
        void AddWarningMessage(string message);
    }
}
