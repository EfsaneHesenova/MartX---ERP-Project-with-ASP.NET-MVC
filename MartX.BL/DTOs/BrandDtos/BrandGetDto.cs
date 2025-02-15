using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MartX.BL.DTOs.BrandDtos;

public class BrandGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Product>? Products { get; set; }
    public string ImageUrl { get; set; }
    public Guid SupplierId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Supplier? Supplier { get; set; }
}
