using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.CategoryDtos;
using MartX.BL.DTOs.ProductDtos;

namespace MartX.BL.DTOs.CheckoutItemDtos;

public class CheckoutGetDto
{
    public CheckoutDto CheckoutDto { get; set; }
    public ICollection<ProductGetDto> Products { get; set; }
    public ICollection<CategoryGetDto> Categories { get; set; }
    public ICollection<BrandGetDto> Brands { get; set; }
}
