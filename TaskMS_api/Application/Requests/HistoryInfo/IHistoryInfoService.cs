using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Service.Repositories;
using Domain.Entities.Models;

namespace Application.Requests.HistoryInfo
{
    public interface IHistoryInfoService: IAsyncRepository<History>
    {
        Task<List<HistoryDto>> GetAllHistoryListById(string? requestProjectId, string? requestTaskId);
    }
}
