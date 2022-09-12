using Common.Service.Repositories;
using Domain.Entities.Models;

namespace Application.Requests.ProjectInfo
{
    public interface IProjectInfoService : IAsyncRepository<Project>
    {
        Task<List<ProjectInfoDto>> GetProjectById(string searchId, string dataType);
        Task<List<ProjectInfoDto>> GetAllProject();
    }
}
