using Common.Service.Repositories;
using Domain.Entities.Models;

namespace Application.Requests.StatuesInfo
{
    public interface IStatuesService : IAsyncRepository<CmnStatus>
    {
        Task<List<StatuesInfoDto>> GetStatuesByFlagNo(int requestId);
    }
}
