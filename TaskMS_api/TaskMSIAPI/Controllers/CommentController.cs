using Application.Requests.CommentInfo.Commands;
using Application.Requests.CommentInfo.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskMSIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ActionName("GetCommentListByTaskId")]
        public async Task<IActionResult> GetCommentListByTaskId( string taskId)
        {
            return Ok(await _mediator.Send(new GetCommentListByTaskId() {  TaskId = taskId }));
        }
        [HttpPost]
        [ActionName("InsertComment")]
        public async Task<IActionResult> InsertComment(CreateCommentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ValidationErrors);
        }

    }
}
