using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;

namespace MartX.BL.DTOs.CheckoutItemDtos;

public class CheckoutGetDto
{
    public CheckoutDto CheckoutDto { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Brand> Brands { get; set; }
}
