namespace Domain.Interfaces
{
    public interface IAppLogger
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}
