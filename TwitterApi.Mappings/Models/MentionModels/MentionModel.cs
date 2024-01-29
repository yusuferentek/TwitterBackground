using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Mappings.Models.UserModels;

namespace TwitterApi.Mappings.Models.MentionModels
{
    public record struct MentionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel? User { get; set; }
        public string Content { get; set; }
        public int FavoriteCount { get; set; }
        public int ReplyCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime? CDate { get; set; }
    }
}
