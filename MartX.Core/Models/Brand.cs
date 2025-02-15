using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models.Common;

namespace MartX.Core.Models;

public class Brand : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Product>? Products { get; set; }
    public Guid SupplierId { get; set; }
    public string ImageUrl { get; set; }
    public Supplier? Supplier { get; set; }
}
