using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Mappings.Models.MentionModels;
using Utils.Wrappers;

namespace TwitterApi.Application.Queries.MentionQueries
{
    public record GetMentionsFollowQuery : IRequest<ServiceResult<List<MentionModel>>>
    {
        public int UserId { get; set; }
    }
}
