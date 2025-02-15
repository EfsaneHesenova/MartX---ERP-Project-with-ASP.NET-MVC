using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.DAL.Repositories.Abstractions;

public interface IDepartmentReadRepository : IReadRepository<Department>
{
    Task<ICollection<SelectListItem>> SelectAllDepartmentAsync();
}
