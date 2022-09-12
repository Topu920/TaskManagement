using AutoMapper;
using MediatR;

namespace Application.Requests.TaskInfo.Queries
{
    public class GetAllTaskListByUser : IRequest<List<TaskInfoDto>>
    {
        public long EmpId { get; set; }
    }
    public class GetAllTaskListByUserHandler : IRequestHandler<GetAllTaskListByUser, List<TaskInfoDto>>
    {
        private readonly ITaskInfoService _taskInfoService;
        private readonly IMapper _mapper;

        public GetAllTaskListByUserHandler(ITaskInfoService taskInfoService, IMapper mapper)
        {
            _taskInfoService = taskInfoService;
            _mapper = mapper;
        }

        public async Task<List<TaskInfoDto>> Handle(GetAllTaskListByUser request, CancellationToken cancellationToken)
        {
            var list = await _taskInfoService.GetAllTaskListByUser(request.EmpId);

            return _mapper.Map<List<TaskInfoDto>>(list);
        }
    }
}
