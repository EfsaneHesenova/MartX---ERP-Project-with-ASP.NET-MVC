using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models.Common;

namespace MartX.Core.Models;

public class DocumentImageUrl : BaseEntity
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
