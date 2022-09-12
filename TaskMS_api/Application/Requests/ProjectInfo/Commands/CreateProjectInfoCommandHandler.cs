using Application.Requests.HistoryInfo;
using Application.Requests.MemberInfo;
using AutoMapper;
using Domain.Entities.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Requests.ProjectInfo.Commands
{
    public class CreateProjectInfoCommandHandler : IRequestHandler<CreateProjectInfoCommand, CreateProjectInfoCommandResponse>
    {
        private readonly IProjectInfoService _projectInfoService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectInfoCommandHandler> _logger;
        private readonly IHistoryInfoService _historyInfoService;
        private readonly IMemberInfoService _memberInfoService;
        public CreateProjectInfoCommandHandler(IProjectInfoService projectInfoService, IMapper mapper, ILogger<CreateProjectInfoCommandHandler> logger, IHistoryInfoService historyInfoService, IMemberInfoService memberInfoService)
        {
            _projectInfoService = projectInfoService;
            _mapper = mapper;
            _logger = logger;
            _historyInfoService = historyInfoService;
            _memberInfoService = memberInfoService;
        }

        public async Task<CreateProjectInfoCommandResponse> Handle(CreateProjectInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateProjectInfoCommandResponse();
            try
            {
                Project project = new()
                {
                    ProjectId = request.ProjectId,
                    ProjectName = request.ProjectName,
                    ProjectDescription = request.ProjectDescription,
                    StartingDate = request.StartingDate != null ? ConvertDate(request.StartingDate) : null,
                    DueDate = request.DueDate != null ? ConvertDate(request.DueDate) : null,
                    FinishingDate = request.FinishingDate != null ? ConvertDate(request.FinishingDate) : null,
                    StatusId = request.StatusId,
                    CreateDate = DateTime.Now,
                    CreateBy = request.CreateBy,
                    IsActive = true
                };
                string? message;
                if (project.ProjectId == Guid.Empty)
                {

                    project = await _projectInfoService.AddAsync(project);
                    response.Message = project.ProjectName + " Saved Successfully";
                    _logger.LogInformation($"{response.Message = project.ProjectName + " is Successfully Created"}");
                    message = "Project \"" + project.ProjectName + "\" Created by ";



                }
                else
                {
                    project = await _projectInfoService.Update(project);
                    response.Message = project.ProjectName + " Updated Successfully";
                    message = "Project \"" + project.ProjectName + "\" Updated by ";
                }
                var ss=await SaveHistory(project.ProjectId, project.CreateBy, message);
                response.ProjectInfoDto = _mapper.Map<ProjectInfoDto>(project);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;

            }


            return response;
        }

        private static DateTime ConvertDate(string time)
        {
            return DateTime.Parse(time);
        }

        private async Task<string> SaveHistory(Guid projectId, long? createBy, string message)
        {
            try
            {
                var user = _memberInfoService.GetMember(createBy);
                var history = new History
                {

                    ProjectId = projectId,

                    HistoryDescription = message + user.Name,
                    CreateBy = createBy,
                    CreateDate = DateTime.Now
                };

                history = await _historyInfoService.AddAsync(history);
                return history.HistoryId.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
               
            
        }

    }
}
