using System.ComponentModel.DataAnnotations.Schema;
using TwitterApi.Infrastructure.Entities.Base;
using TwitterApi.Infrastructure.Entities.Users;

namespace TwitterApi.Infrastructure.Entities.Mentions;

[Table("Mention")]
public class Mention : BaseEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Content { get; set; }
    public int FavoriteCount { get; set; }
    public int CommentCount { get; set; }
    public int ReplyCount { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}

