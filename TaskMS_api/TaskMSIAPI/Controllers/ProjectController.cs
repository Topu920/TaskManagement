using Application.Requests.ProjectInfo.Commands;
using Application.Requests.ProjectInfo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskMSIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ActionName("InsertProjectInfo")]
        public async Task<IActionResult> InsertProjectInfo(CreateProjectInfoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ValidationErrors);
        }

        [HttpGet]
        [ActionName("GetProjectList")]
        public async Task<IActionResult> GetProjectList()
        {
            return Ok(await _mediator.Send(new GetProjectList()));
        }
        [HttpGet]
        [ActionName("GetProjectListById")]
        public async Task<IActionResult> GetProjectListById(string id)
        {
            return Ok(await _mediator.Send(new GetProjectList { Id = id }));
        }
        [HttpGet]
        [ActionName("GetProjectListByUserId")]
        public async Task<IActionResult> GetProjectListByUserId(string id)
        {
            return Ok(await _mediator.Send(new GetProjectList { Id = id }));
        }

        [HttpGet]
        [ActionName("DeleteProjectById")]
        public async Task<IActionResult> DeleteProjectById(string id)
        {
            return Ok(await _mediator.Send(new DeleteProjectCommand { ProjectId = id }));
        }



    }
}
