using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Enums;
using MartX.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.DTOs.ProductDtos;

public class ProductPostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal RealPrice { get; set; }
    public int SalePercent { get; set; } = 0;
    public IFormFile Image { get; set; }
    public decimal? SalePrice { get; set; }
    public Sizes Size { get; set; }
    public ICollection<SelectListItem>? Brands { get; set; }
    public ICollection<SelectListItem>? Categories { get; set; }
    public int StockQuantity { get; set; }
}
