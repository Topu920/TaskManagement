using Application.Requests.UserInfos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskMSIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ActionName("UserLogIn")]
        public async Task<IActionResult> UserLogIn(CheckUserInfo command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ValidationErrors);
            // return Ok(result);
        }
    }
}
