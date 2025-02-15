using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.DTOs.BrandDtos;

public class BrandPutDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    public ICollection<SelectListItem>? Suppliers { get; set; }
    public Guid SupplierId { get; set; }
}
