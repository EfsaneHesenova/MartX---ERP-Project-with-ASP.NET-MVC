using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MartX.Core.Models;

public class Department : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Employee>? Employees { get; set; }

}
