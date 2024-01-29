using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Mappings.Models.UserModels;
using Utils.Wrappers;

namespace TwitterApi.Application.Commands.AuthCommands
{
    public class LoginCommand : IRequest<ServiceResult<UserResponseModel>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
