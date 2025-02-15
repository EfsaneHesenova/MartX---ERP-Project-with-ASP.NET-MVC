using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.DAL.Repositories.Abstractions;

public interface IEmployeeReadRepository : IReadRepository<Employee>
{
    Task<ICollection<SelectListItem>> SelectAllEmployeeAsync();
}
