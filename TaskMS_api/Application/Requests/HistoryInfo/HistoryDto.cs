using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mappings;
using Domain.Entities.Models;

namespace Application.Requests.HistoryInfo
{
    public class HistoryDto : IMapFrom<History>
    {
        public Guid HistoryId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public string? HistoryDescription { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
    }
}
