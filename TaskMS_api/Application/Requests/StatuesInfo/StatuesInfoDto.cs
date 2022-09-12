using Application.Common.Mappings;
using Domain.Entities.Models;

namespace Application.Requests.StatuesInfo
{
    public class StatuesInfoDto : IMapFrom<CmnStatus>
    {
        public Guid StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public int? FlagNo { get; set; }
        public int? OrderNo { get; set; }
        public string? ModuleName { get; set; }
    }
}
