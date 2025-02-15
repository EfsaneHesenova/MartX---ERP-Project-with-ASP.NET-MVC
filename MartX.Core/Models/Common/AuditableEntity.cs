using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MartX.Core.Models.Common
{
    public class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public IdentityUser CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IdentityUser? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public IdentityUser? DeletedBy { get; set; }
    }
}
