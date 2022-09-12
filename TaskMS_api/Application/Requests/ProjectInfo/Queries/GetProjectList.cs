using AutoMapper;
using MediatR;

namespace Application.Requests.ProjectInfo.Queries
{
    public class GetProjectList : IRequest<List<ProjectInfoDto>>
    {
        public string Id { get; set; } = null!;
    }

    public class GetProjectListHandler : IRequestHandler<GetProjectList, List<ProjectInfoDto>>
    {
        private readonly IProjectInfoService _projectInfoService;
        private readonly IMapper _mapper;
        public GetProjectListHandler(IProjectInfoService projectInfoService, IMapper mapper)
        {
            _projectInfoService = projectInfoService;
            _mapper = mapper;
        }
        public async Task<List<ProjectInfoDto>> Handle(GetProjectList request, CancellationToken cancellationToken)
        {

            var dataType = "";

            if (Guid.TryParse(request.Id, out _))
            {
                dataType = "guid";
            }

            if (int.TryParse(request.Id, out _))
            {
                dataType = "int";
            }

            if ((request.Id != "00000000-0000-0000-0000-000000000000" && !string.IsNullOrEmpty(request.Id)) || dataType != "")
            {
                var projectList = await _projectInfoService.GetProjectById(request.Id, dataType);
                return _mapper.Map<List<ProjectInfoDto>>(projectList);
            }



            var list = await _projectInfoService.GetAllProject();

            return _mapper.Map<List<ProjectInfoDto>>(list);
        }
    }
}
