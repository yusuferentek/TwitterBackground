using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Infrastructure.Entities.Base;

namespace TwitterApi.Infrastructure.Entities.General
{
    [Table("LeftMenu")]
    public class LeftMenu : BaseEntity
    {
        public string Icon { get; set; }
        public string Title { get; set; }
    }
}
