using Common.Service.Responses;
using MediatR;

namespace Application.Requests.ProjectInfo.Commands
{
    public class CreateProjectInfoCommand : IRequest<CreateProjectInfoCommandResponse>
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? ProjectDescription { get; set; }
        public string? StartingDate { get; set; }
        public string? DueDate { get; set; }
        public string? FinishingDate { get; set; }
        public Guid? StatusId { get; set; }

        public long CreateBy { get; set; }
       

    }

    public class CreateProjectInfoCommandResponse : BaseResponse
    {
        public ProjectInfoDto ProjectInfoDto { get; set; } = null!;
    }
}
