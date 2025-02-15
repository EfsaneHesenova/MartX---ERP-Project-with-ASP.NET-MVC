using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;

namespace MartX.BL.DTOs.DepartmentDtos;

public class DepartmentGetDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Employee>? Employees { get; set; }
}
