using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Infrastructure.Entities.Users;

namespace TwitterApi.Mappings.Models.UserModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ProfilePic {  get; set; }
        public string Fullname { get; set;}


        
    }
}
