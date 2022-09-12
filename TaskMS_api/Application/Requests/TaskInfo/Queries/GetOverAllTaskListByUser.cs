using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Application.Requests.TaskInfo.Queries
{
    public class GetOverAllTaskListByUser : IRequest<List<TaskInfoDto>>
    {
        public long EmpId { get; set; }
    }
    public class GetOverAllTaskListByUserHandler : IRequestHandler<GetOverAllTaskListByUser, List<TaskInfoDto>>
    {
        private readonly ITaskInfoService _taskInfoService;
        private readonly IMapper _mapper;

        public GetOverAllTaskListByUserHandler(ITaskInfoService taskInfoService, IMapper mapper)
        {
            _taskInfoService = taskInfoService;
            _mapper = mapper;
        }

        public async Task<List<TaskInfoDto>> Handle(GetOverAllTaskListByUser request, CancellationToken cancellationToken)
        {
            var list = await _taskInfoService.GetOverAllTaskListByUser(request.EmpId);

            return _mapper.Map<List<TaskInfoDto>>(list);
        }
    }
}
