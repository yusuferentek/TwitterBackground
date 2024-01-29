using MediatR;
using TwitterApi.Application.Commands.MentionCommands;
using TwitterApi.Infrastructure.Entities.Mentions;
using TwitterApi.Infrastructure.Repositories.MentionRepos;
using Utils.Enums;
using Utils.Wrappers;

namespace TwitterApi.Application.CommandHandlers.MentionCommandHandlers
{
    public class NewMentionCommandHandler : IRequestHandler<NewMentionCommand, ServiceResult<int>>
    {
        private readonly IMentionRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public NewMentionCommandHandler(IMentionRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<int>> Handle(NewMentionCommand request, CancellationToken cancellationToken)
        {
            var newMent = new Mention()
            {
                UserId = request.UserId,
                Content = request.Content,
                CDate = request.CDate,
            };
            var cTran = await _repository.UnitOfWork.BeginTransactionAsync();
            var res = _repository.Add(newMent);
            await _repository.UnitOfWork.CommitTransactionAsync(cTran);
            if (res.Id != 0)
            {
                return new ServiceResult<int>()
                {
                    ResultType = ResultTypes.Success,
                    Obj = newMent.Id
                };
            }
            else
            {
                return new ServiceResult<int>()
                {
                    ResultType = ResultTypes.Failed,
                    Message = "lms-error"
                };
            }
        }
    }
}
