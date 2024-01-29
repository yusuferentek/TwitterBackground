using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Application.Queries.MentionQueries;
using TwitterApi.Infrastructure.Entities.Users;
using TwitterApi.Infrastructure.Repositories.MentionRepos;
using TwitterApi.Infrastructure.Repositories.UserRepos;
using TwitterApi.Mappings.Models.MentionModels;
using TwitterApi.Mappings.Models.UserModels;
using Utils.Wrappers;

namespace TwitterApi.Application.QueryHandlers.MentionQueryHandlers
{
    public record GetMentionsQueryHandler : IRequestHandler<GetMentionsQuery, ServiceResult<List<MentionModel>>>
    {
        private readonly IMentionRepository _repository;
        private readonly IUserRepository _userRepository;

        public GetMentionsQueryHandler(IMentionRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<List<MentionModel>>> Handle(GetMentionsQuery request, CancellationToken cancellationToken)
        {

            var mentionModels = await _repository.GetAll()
                .Include(m => m.User) // User ilişkisini çek
        .Select(x => new MentionModel
        {
            Id = x.Id,
            UserId = x.UserId,
            User = new UserModel
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
        })
    .ToListAsync();

            return new ServiceResult<List<MentionModel>>()
            {
                ResultType = Utils.Enums.ResultTypes.Success,
                Obj = mentionModels
            };
        }
    }
}
