using Application.Requests.TaskInfo.Commands;
using Application.Requests.TaskInfo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskMSIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ActionName("InsertTaskInfo")]
        public async Task<IActionResult> InsertTaskInfo(CreateTaskInfoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ValidationErrors);
        }
        [HttpPost]
        [ActionName("UpdateTaskInfoByMember")]
        public async Task<IActionResult> UpdateTaskInfoByMember(UpdateTaskInfoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ValidationErrors);
        }

        [HttpGet]
        [ActionName("GetAllTaskListByProjectId")]
        public async Task<IActionResult> GetAllTaskListByProjectId(string projectId)
        {
            return Ok(await _mediator.Send(new GetTaskInfoList { Id = projectId }));
        }
        [HttpGet]
        [ActionName("GetAllTaskList")]
        public async Task<IActionResult> GetAllTaskList()
        {
            return Ok(await _mediator.Send(new GetTaskInfoList()));
        }

        [HttpGet]
        [ActionName("GetAllTaskListByUser")]
        public async Task<IActionResult> GetAllTaskListByUser(long empId)
        {
            return Ok(await _mediator.Send(new GetAllTaskListByUser { EmpId = empId }));
        }

        [HttpGet]
        [ActionName("GetOverAllTaskListByUser")]
        public async Task<IActionResult> GetOverAllTaskListByUser(long empId)
        {
            return Ok(await _mediator.Send(new GetOverAllTaskListByUser { EmpId = empId }));
        }




    }
}
