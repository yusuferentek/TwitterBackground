using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterApi.Application.Commands.MentionCommands;
using TwitterApi.Application.Queries.MentionQueries;
using TwitterApi.Controllers.Base;

namespace TwitterApi.Controllers
{
    [Route("[controller]")]
    public class MentionController : BaseController
    {
        private readonly IMediator _mediator;

        public MentionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> NewMention([FromBody] NewMentionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()//CTable
        {
            var _query = new GetMentionsQuery() { };
            return Ok(await _mediator.Send(_query));
        }

        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetFollowedMentions(int userId)//CTable
        {
            var _query = new GetMentionsFollowQuery()
            {
                UserId = userId
            };
            return Ok(await _mediator.Send(_query));
        }
    }
}
