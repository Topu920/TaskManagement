using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Requests.ProjectInfo.Commands
{
    public class DeleteProjectCommand: IRequest<CreateProjectInfoCommandResponse>
    {
        public string ProjectId { get; set; } = null!;
    }
    public class DeleteProjectHandler: IRequestHandler<DeleteProjectCommand,CreateProjectInfoCommandResponse>
    {
        private readonly IProjectInfoService _projectInfoService;

        public DeleteProjectHandler(IProjectInfoService projectInfoService)
        {
            _projectInfoService = projectInfoService;
        }

        public async Task<CreateProjectInfoCommandResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateProjectInfoCommandResponse();
            try
            {
                var project =await _projectInfoService.GetByIdAsync(Guid.Parse(request.ProjectId));
                project.IsActive = false;
                project = await _projectInfoService.Update(project);
                response.Message = project.ProjectName + " Deleted Successfully";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;
            }
            return response;
        }
    }
}
