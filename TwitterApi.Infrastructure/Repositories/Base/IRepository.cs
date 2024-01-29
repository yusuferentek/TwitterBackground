using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Infrastructure.Database;
using TwitterApi.Infrastructure.Entities.Base;

namespace TwitterApi.Infrastructure.Repositories.Base
{
    public interface IRepository<T> where T: BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
