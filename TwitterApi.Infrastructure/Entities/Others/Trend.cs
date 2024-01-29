using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Infrastructure.Entities.Base;

namespace TwitterApi.Infrastructure.Entities.Others
{
    [Table("Trend")]
    public class Trend : BaseEntity
    {
        public string Location { get; set; }
        public string Hashtag { get; set; }
        public string Count { get; set; }
    }
}
