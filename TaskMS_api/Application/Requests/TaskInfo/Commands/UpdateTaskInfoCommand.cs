using Application.Requests.HistoryInfo;
using Application.Requests.MemberInfo;
using Common.Service.Responses;
using Domain.Entities.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Requests.TaskInfo.Commands
{
    public class UpdateTaskInfoCommand : IRequest<UpdateTaskInfoResponse>
    {

        public Guid TaskId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid StatusId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public string? Eddate { get; set; }
        public string? StartingDate { get; set; }
        public string? FinishingDate { get; set; }
        public long? FinishedBy { get; set; }
        public long? CreateBy { get; set; }
       // public bool? IsActive { get; set; }


    }

    public class UpdateTaskInfoResponse : BaseResponse
    {
        public TaskInfoDto TaskInfoDto { get; set; } = null!;
    }

    public class UpdateTaskInfoCommandHandler : IRequestHandler<UpdateTaskInfoCommand, UpdateTaskInfoResponse>
    {
        private readonly ITaskInfoService _taskInfoService;
        private readonly ILogger<UpdateTaskInfoCommandHandler> _logger;
        private readonly IHistoryInfoService _historyInfoService;
        private readonly IMemberInfoService _memberInfoService;
        public UpdateTaskInfoCommandHandler(ITaskInfoService taskInfoService, ILogger<UpdateTaskInfoCommandHandler> logger, IHistoryInfoService historyInfoService, IMemberInfoService memberInfoService)
        {
            _taskInfoService = taskInfoService;
            _logger = logger;
            _historyInfoService = historyInfoService;
            _memberInfoService = memberInfoService;
        }

        public async Task<UpdateTaskInfoResponse> Handle(UpdateTaskInfoCommand request, CancellationToken cancellationToken)
        {
            UpdateTaskInfoResponse response = new();
            try
            {
                ProjectTask task = new()
                {
                    TaskId = request.TaskId,
                    ProjectId = request.ProjectId,
                    StatusId = request.StatusId,
                    TaskName = request.TaskName,
                    TaskDescription = request.TaskDescription,
                    Eddate = request.Eddate != null ? ConvertDate(request.Eddate) : null,
                    StartingDate = request.StartingDate != null ? ConvertDate(request.StartingDate) : null,
                    FinishingDate = request.FinishingDate != null ? ConvertDate(request.FinishingDate) : null,
                    FinishedBy = request.FinishedBy,
                    CreateBy=request.CreateBy,
                    CreateDate = DateTime.Now,
              //      IsActive = request.IsActive
                };
                string message;
                if (task.TaskId == Guid.Empty)
                {
                    task = await _taskInfoService.AddAsync(task);
                    response.Message = task.TaskName + " Saved Successfully";
                    _logger.LogInformation($"{response.Message = task.TaskName + " is Successfully Created"}");
                    message = "Task \"" + task.TaskName + "\" Created by ";
                }
                else
                {
                    task = await _taskInfoService.Update(task);
                    message = "Task \"" + task.TaskName + "\" Updated by ";
                    response.Message = task.TaskName + " Updated Successfully";
                }
                var ss = await SaveHistory(task.ProjectId, task.TaskId, task.CreateBy, message);

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

        private async Task<string> SaveHistory(Guid projectId, Guid taskId, long? createBy, string message)
        {
            try
            {
                var user = _memberInfoService.GetMember(createBy);
                var history = new History
                {

                    ProjectId = projectId,
                    TaskId = taskId,
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
