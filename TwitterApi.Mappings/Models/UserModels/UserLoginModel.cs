using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApi.Mappings.Models.UserModels
{
    public record struct UserLoginModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public record struct UserResponseModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }

    }

}
