using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Enums;
using MartX.Core.Models.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MartX.Core.Models;

public class Product : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid BrandId { get; set; }
    public Brand? Brand { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public decimal RealPrice { get; set; }
    public int SalePercent { get; set; } = 0;
    public string ImageUrl { get; set; }
    public decimal? SalePrice { get; set; }
    public Sizes Size { get; set; }
    public int StockQuantity { get; set; }
}
