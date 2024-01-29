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
    public record GetMentionsQuery : IRequest<ServiceResult<List<MentionModel>>>
    {
    }
}
