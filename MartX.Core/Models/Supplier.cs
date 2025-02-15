using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models.Common;

namespace MartX.Core.Models
{
    public class Supplier : AuditableEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public ICollection<Brand>? Brands { get; set; }
    }
}
