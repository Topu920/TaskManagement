using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface ILogToDatabaseService
    {
        Task<Result> Save(RequestLoggerEntity loggerEntity, CancellationToken cancellationToken);
    }
}
