using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;

namespace MartX.BL.DTOs.CheckoutItemDtos;

public class CheckoutDto
{
    public List<CheckoutItemDto> Items { get; set; }
}
