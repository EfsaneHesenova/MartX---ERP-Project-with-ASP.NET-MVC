using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using MartX.DAL.Contexts;
using MartX.DAL.Repositories.Abstractions;

namespace MartX.DAL.Repositories.Implementations;

public class DepartmentWriteRepository : WriteRepository<Department>, IDepartmentWriteRepository
{
    public DepartmentWriteRepository(AppDbContext context) : base(context)
    {
    }
}
