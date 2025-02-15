using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models.Common;

namespace MartX.Core.Models;

public class Checkout : AuditableEntity
{
    public List<CheckoutItem>? Items { get; set; }
    public decimal TotalPrice { get; set; }
}
