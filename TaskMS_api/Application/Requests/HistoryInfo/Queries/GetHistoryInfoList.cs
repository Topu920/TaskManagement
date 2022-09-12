using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Application.Requests.HistoryInfo.Queries
{
    public class GetHistoryInfoList:IRequest<List<HistoryDto>>
    {
        public string? ProjectId { get; set; } = null!;
        public string? TaskId { get; set; } = null!;
    }

    public class GetHistoryInfoListHandler : IRequestHandler<GetHistoryInfoList, List<HistoryDto>>
    {
        private readonly IHistoryInfoService _historyInfoService;
        private readonly IMapper _mapper;

        public GetHistoryInfoListHandler(IMapper mapper, IHistoryInfoService historyInfoService)
        {
            _mapper = mapper;
            _historyInfoService = historyInfoService;
        }

        public async Task<List<HistoryDto>> Handle(GetHistoryInfoList request, CancellationToken cancellationToken)
        {
            var list = await _historyInfoService.GetAllHistoryListById(request.ProjectId,request.TaskId);

            return _mapper.Map<List<HistoryDto>>(list);
        }
    }
}
