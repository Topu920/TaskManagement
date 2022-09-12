using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Requests.HistoryInfo;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TaskApp
{
    public class HistoryInfoService : BaseRepository<History>, IHistoryInfoService
    {
        public HistoryInfoService(TaskManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<HistoryDto>> GetAllHistoryListById(string? requestProjectId, string? requestTaskId)
        {
            try
            {

                var data = await DbContext.Histories
                        .Select(x => new HistoryDto()
                        {
                            HistoryId = x.HistoryId,
                            ProjectId = x.ProjectId,
                            TaskId = x.TaskId,
                            HistoryDescription = x.HistoryDescription ?? "",
                            CreateDate = x.CreateDate,
                            CreateBy = x.CreateBy,

                        }).OrderByDescending(x => x.CreateDate)
                        .ToListAsync();
                if (requestProjectId != "00000000-0000-0000-0000-000000000000" && !string.IsNullOrEmpty(requestProjectId))
                {
                    data = data.Where(h => h.ProjectId == Guid.Parse(requestProjectId)).ToList();
                }
                if (requestTaskId != "00000000-0000-0000-0000-000000000000" && !string.IsNullOrEmpty(requestTaskId))
                {
                    data = data.Where(h => h.TaskId == Guid.Parse(requestTaskId)).ToList();
                }

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
