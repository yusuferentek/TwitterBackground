using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TwitterApi.Infrastructure.Entities.Base;
using TwitterApi.Infrastructure.Entities.Mentions;

namespace TwitterApi.Infrastructure.Entities.Users;

[Table("User")]
public class User : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? ProfilePic { get; set; }
    public string Fullname { get; set; }
    public ICollection<Mention> Mentions { get; set; } = new List<Mention>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<User> Followers { get; set; } = new List<User>();
    public virtual ICollection<User> Following { get; set; } = new List<User>();


}
