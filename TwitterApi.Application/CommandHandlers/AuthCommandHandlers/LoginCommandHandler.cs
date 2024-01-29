using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Application.Commands.AuthCommands;
using TwitterApi.Application.Services.Auth;
using TwitterApi.Infrastructure.Entities.Users;
using TwitterApi.Infrastructure.Repositories.UserRepos;
using TwitterApi.Mappings.Models.UserModels;
using Utils.Enums;
using Utils.General;
using Utils.Wrappers;

namespace TwitterApi.Application.CommandHandlers.AuthCommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ServiceResult<UserResponseModel>>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public LoginCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _repository = userRepository;
            _authService = authService;
        }

        public async Task<ServiceResult<UserResponseModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = _repository.GetUserByUsernameAndPass(request.Username, request.Password);
            if (user == null)
            {
                return new ServiceResult<UserResponseModel>
                {
                    ResultType = ResultTypes.Failed,
                    Message = SystemMessages.DataNotFound
                };
            }
            return new ServiceResult<UserResponseModel>
            {
                ResultType = ResultTypes.Success,
                Message = SystemMessages.LoginSuccessful,
                Obj = new UserResponseModel()
                {
                    Id = user.Id,
                    FullName = user.Fullname,
                    Token = _authService.GetToken(user),
                    Avatar = user.ProfilePic,
                    UserName = user.Username,
                }
            };
        }
    }
}
