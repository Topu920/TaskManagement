using Application.Requests.GroupInfo.Commands;
using Application.Requests.GroupInfo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskMSIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupMemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupMemberController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ActionName("InsertGroupMember")]
        public async Task<IActionResult> InsertGroupMember(CreateOrUpdateGroupInfo command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ValidationErrors);
        }

        [HttpGet]
        [ActionName("GetAllGroupList")]
        public async Task<IActionResult> GetAllGroupList(long userId)
        {
            return Ok(await _mediator.Send(new GetAllGroupList(userId)));
        }

        [HttpGet]
        [ActionName("GetAllGroupMemberList")]
        public async Task<IActionResult> GetAllGroupMemberList(long userId)
        {
            return Ok(await _mediator.Send(new GetGroupMemberList(userId)));
        }


    }
}
