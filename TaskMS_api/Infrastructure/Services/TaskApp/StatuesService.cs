using Application.Requests.StatuesInfo;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TaskApp
{
    public class StatuesService : BaseRepository<CmnStatus>, IStatuesService
    {
        public StatuesService(TaskManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<StatuesInfoDto>> GetStatuesByFlagNo(int requestId)
        {
            try
            {
                var data = await DbContext.CmnStatuses.Where(p => p.FlagNo == requestId).AsNoTracking()
                    .Select(a => new StatuesInfoDto()
                    {
                        StatusId = a.StatusId,
                        StatusName = a.StatusName,
                        FlagNo = a.FlagNo,
                        OrderNo = a.OrderNo,
                        ModuleName = a.ModuleName,

                    }).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
