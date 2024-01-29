using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Application.Queries.MentionQueries;
using TwitterApi.Infrastructure.Entities.Mentions;
using TwitterApi.Infrastructure.Repositories.MentionRepos;
using TwitterApi.Mappings.Models.MentionModels;
using Utils.Enums;
using Utils.Wrappers;

namespace TwitterApi.Application.QueryHandlers.MentionQueryHandlers
{
    public record GetMentionsFollowQueryHandler : IRequestHandler<GetMentionsFollowQuery, ServiceResult<List<MentionModel>>>
    {
        private readonly IMentionRepository _mentionRepository;

        public GetMentionsFollowQueryHandler(IMentionRepository mentionRepository)
        {
            _mentionRepository = mentionRepository;
        }

        public async Task<ServiceResult<List<MentionModel>>> Handle(GetMentionsFollowQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var mentions = _mentionRepository.GetMentionsForFollowedUsers(request.UserId).Include(m => m.User)
                .Select(x => new MentionModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    User = new Mappings.Models.UserModels.UserModel
                    {
                        Id = x.User.Id,
                        Username = x.User.Username,
                        ProfilePic = x.User.ProfilePic,
                        Fullname = x.User.Fullname
                    },
                    Content = x.Content,
                    FavoriteCount = x.FavoriteCount,
                    ReplyCount = x.ReplyCount,
                    CommentCount = x.CommentCount,
                    CDate = x.CDate
                }).ToList();
                return new ServiceResult<List<MentionModel>>()
                {
                    ResultType = Utils.Enums.ResultTypes.Success,
                    Obj = mentions
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List< MentionModel >> ()
                {
                    ResultType = Utils.Enums.ResultTypes.Error,
                    Message = ex.Message
                };
            }
            
        }
    }
}
