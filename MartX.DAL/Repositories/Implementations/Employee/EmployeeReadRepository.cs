using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using MartX.DAL.Contexts;
using MartX.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MartX.DAL.Repositories.Implementations;

public class EmployeeReadRepository : ReadRepository<Employee>, IEmployeeReadRepository
{
    private readonly AppDbContext _context;
    public EmployeeReadRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<SelectListItem>> SelectAllEmployeeAsync()
    {
        return await _context.Employees.Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.FirstName,
        }).ToListAsync();
    }
}
