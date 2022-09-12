using Application.Requests.MemberInfo;
using Application.Requests.MemberInfo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskMSIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        [HttpPost]
        [ActionName("GetOnSearchMemberList")]
        public async Task<ActionResult<List<MemberInfoDto>>> GetOnSearchMemberList([FromBody] string searchKey)
        {
            var data = await _mediator.Send(new GetMemberList { SearchKey = searchKey });
            return Ok(data);
        }

        [HttpGet]
        [ActionName("GetMemberList")]
        public async Task<ActionResult<List<MemberInfoDto>>> GetMemberList()
        {
            var data = await _mediator.Send(new GetMemberList { SearchKey = "" });
            return Ok(data);
        }


    }
}
