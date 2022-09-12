using Application.Requests.HistoryInfo.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskMSIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ActionName("GetHistoryInfoList")]
        public async Task<IActionResult> GetHistoryInfoList(string? projectId, string? taskId)
        {
            return Ok(await _mediator.Send(new GetHistoryInfoList {ProjectId = projectId, TaskId = taskId}));
        }
    }
}
