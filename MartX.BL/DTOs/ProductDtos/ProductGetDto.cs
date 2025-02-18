using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Enums;
using MartX.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MartX.BL.DTOs.ProductDtos;

public class ProductGetDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DeletedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid BrandId { get; set; }
    public Brand? Brand { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public string ImageUrl { get; set; }
    public decimal RealPrice { get; set; }
    public int SalePercent { get; set; } = 0;
    public decimal? SalePrice { get; set; }
    public Sizes Size { get; set; }
    public int StockQuantity { get; set; }
}
