using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Application.Commands.AuthCommands;
using TwitterApi.Infrastructure.Entities.Users;
using TwitterApi.Infrastructure.Repositories.UserRepos;
using Utils.Enums;
using Utils.Extensions;
using Utils.General;
using Utils.Wrappers;

namespace TwitterApi.Application.CommandHandlers.AuthCommandHandlers
{
    public class NewUserCommandHandler : IRequestHandler<NewUserCommand, ServiceResult<int>>
    {
        private readonly IUserRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public NewUserCommandHandler (IUserRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<int>> Handle(NewUserCommand request, CancellationToken cancellationToken)
        {
            string profilePicUrl;
            if (_repository.GetAll().Any(x => x.Username == request.Username))
                return new ServiceResult<int>() {
                    ResultType = Utils.Enums.ResultTypes.Failed,
                    Message = SystemMessages.DuplicateUsername
                };
            if (_repository.GetAll().Any(x => x.Email == request.Email))
                return new ServiceResult<int>()
                {
                    ResultType = Utils.Enums.ResultTypes.Failed,
                    Message = SystemMessages.DuplicateEmail
                };
            if (request.ProfilePic.IsNullOrEmpty())
            {
				profilePicUrl = "default_avatar";

			}
            else
            {
				string ftpUrl = "ftp://ftp.sirv.com";
				string fileName = request.Username + "_"  + "ProfilePic" ;
				string address = ftpUrl + "/TwitterApi/" + fileName;
				var ftpClient = (FtpWebRequest)FtpWebRequest.Create(address);
				ftpClient.Credentials = new NetworkCredential("*******", "******");
				ftpClient.Method = WebRequestMethods.Ftp.UploadFile;
				ftpClient.UseBinary = true;
				ftpClient.KeepAlive = true;

				Stream requestStream = ftpClient.GetRequestStream();
				ftpClient.ContentLength = request.ProfilePic.Length;
				requestStream.Write(request.ProfilePic, 0, request.ProfilePic.Length);
				requestStream.Close();

				var uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
				var value = uploadResponse.StatusDescription;
				uploadResponse.Close();
				profilePicUrl = fileName;
			}
            var pass = (request.Username + request.Password).ToPass();
            var newUser = new User()
            {
                Username = request.Username,
                Email = request.Email,
                Password = pass,
                CDate = DateTime.Now,
                Phone = request.Phone,
                Fullname = request.Fullname,
                ProfilePic = profilePicUrl
			};
            var cTran = await _repository.UnitOfWork.BeginTransactionAsync();
            var res = _repository.Add(newUser);
            await _repository.UnitOfWork.CommitTransactionAsync(cTran);
            if(res.Id != 0)
            {
                return new ServiceResult<int>()
                {
                    ResultType = ResultTypes.Success,
                    Obj = newUser.Id
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
