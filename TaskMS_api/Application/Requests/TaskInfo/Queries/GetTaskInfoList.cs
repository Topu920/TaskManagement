using AutoMapper;
using MediatR;

namespace Application.Requests.TaskInfo.Queries
{
    public class GetTaskInfoList : IRequest<List<TaskInfoDto>>
    {
        public string Id { get; set; } = null!;
    }

    public class GetTaskInfoListHandler : IRequestHandler<GetTaskInfoList, List<TaskInfoDto>>
    {
        private readonly ITaskInfoService _taskInfoService;
        private readonly IMapper _mapper;
        public GetTaskInfoListHandler(IMapper mapper, ITaskInfoService taskInfoService)
        {
            _mapper = mapper;
            _taskInfoService = taskInfoService;
        }

        public async Task<List<TaskInfoDto>> Handle(GetTaskInfoList request, CancellationToken cancellationToken)
        {

            if (Guid.TryParse(request.Id, out _))
            {
                if (request.Id != "00000000-0000-0000-0000-000000000000" && !string.IsNullOrEmpty(request.Id))
                {
                    var model = await _taskInfoService.GetAllTaskInfoByProjectId(Guid.Parse(request.Id));

                    return _mapper.Map<List<TaskInfoDto>>(model);
                }
            }

            var list = await _taskInfoService.GetAllTaskInfo();

            return _mapper.Map<List<TaskInfoDto>>(list);
        }
    }
}
