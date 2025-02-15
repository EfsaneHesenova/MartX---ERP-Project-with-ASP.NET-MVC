using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.DAL.Repositories.Abstractions;

public interface ISupplierReadRepository : IReadRepository<Supplier>
{
    Task<ICollection<SelectListItem>> SelectAllSupplierAsync();
}
