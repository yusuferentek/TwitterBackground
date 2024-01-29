using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace TwitterApi.Application.Commands.MentionCommands
{
    public class NewMentionCommand : IRequest<ServiceResult<int>>
    {
        public int Id{ get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CDate { get; set; }

    }
}
