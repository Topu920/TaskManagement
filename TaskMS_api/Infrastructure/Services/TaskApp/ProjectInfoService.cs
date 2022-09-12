using Application.Requests.ProjectInfo;
using Application.Requests.TaskInfo;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TaskApp
{
    public class ProjectInfoService : BaseRepository<Project>, IProjectInfoService
    {
        private readonly  ITaskInfoService _taskInfoService;
        public ProjectInfoService(TaskManagementContext dbContext, ITaskInfoService taskInfoService) : base(dbContext)
        {
            _taskInfoService = taskInfoService;
        }


        public async Task<List<ProjectInfoDto>> GetProjectById(string searchId, string dataType)
        {
            try
            {
                var allData = await DbContext.Projects.Where(p=>p.IsActive!=false).AsNoTracking()
                    .Include(s => s.Status)
                    .Select(a => new ProjectInfoDto
                    {
                        ProjectId = a.ProjectId,
                        ProjectName = a.ProjectName,
                        ProjectDescription = a.ProjectDescription ?? "",
                        StartingDate = a.StartingDate,
                        DueDate = a.DueDate,
                        FinishingDate = a.FinishingDate,
                        StatusId = a.StatusId,
                        StatusName = a.Status!.StatusName,
                        CreateDate = a.CreateDate,
                        CreateBy = a.CreateBy,
                        IsActive = a.IsActive,

                    }).OrderByDescending(c => c.CreateDate).ToListAsync();
              var  data = dataType == "guid" ? allData.Where(p => p.ProjectId == Guid.Parse(searchId)).ToList() : allData.Where(p => p.CreateBy == Convert.ToInt64(searchId)).ToList();

              if (dataType == "guid") return data;
              var model = await _taskInfoService.GetAllTaskListByUser(Convert.ToInt64(searchId));
                foreach (var item in from item in model let check = data.FirstOrDefault(x => x.ProjectId == item.ProjectId) where check == null select item)
                {
                    data.Add(allData.First(x=>x.ProjectId==item.ProjectId));
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<List<ProjectInfoDto>> GetAllProject()
        {
            try
            {
                var data = await DbContext.Projects.Where(p => p.IsActive != false).AsNoTracking()
                    .Include(s => s.Status)
                    .Select(a => new ProjectInfoDto
                    {
                        ProjectId = a.ProjectId,
                        ProjectName = a.ProjectName,
                        ProjectDescription = a.ProjectDescription ?? "",
                        StartingDate = a.StartingDate,
                        DueDate = a.DueDate,
                        FinishingDate = a.FinishingDate,
                        StatusId = a.StatusId,
                        StatusName = a.Status!.StatusName,
                        CreateDate = a.CreateDate,
                        CreateBy = a.CreateBy,
                        IsActive = a.IsActive,
                    }).OrderByDescending(c => c.CreateDate).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
