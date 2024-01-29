using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Infrastructure.Entities.Base;
using TwitterApi.Infrastructure.Entities.Users;

namespace TwitterApi.Infrastructure.Entities.Mentions;

[Table("Comment")]
public class Comment : BaseEntity
{
    public int MentionId { get; set; }
    public virtual Mention Mention { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Content { get; set; }
    public int FavoriteCount { get; set; }
    public int CommentCount { get; set; }
    public int ReplyCount { get; set; }
}

