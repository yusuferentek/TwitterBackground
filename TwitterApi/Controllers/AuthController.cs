using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterApi.Application.Commands.AuthCommands;
using TwitterApi.Controllers.Base;

namespace TwitterApi.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> NewUser([FromBody] NewUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
