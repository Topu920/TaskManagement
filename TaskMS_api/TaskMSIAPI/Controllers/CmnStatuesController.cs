using Application.Requests.StatuesInfo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskMSIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CmnStatuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CmnStatuesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ActionName("GetStatuesByFlagNo")]
        public async Task<IActionResult> GetStatuesByFlagNo(int id)
        {
            return Ok(await _mediator.Send(new GetStatuesBysearchId { Id = id }));
        }
    }
}
