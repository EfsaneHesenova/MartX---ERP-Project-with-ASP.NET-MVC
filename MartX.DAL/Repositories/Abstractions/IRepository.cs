using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace MartX.DAL.Repositories.Abstractions
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        DbSet<T> Table { get; }
    }
}
