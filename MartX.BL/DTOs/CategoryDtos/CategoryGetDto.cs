﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;

namespace MartX.BL.DTOs.CategoryDtos;

public class CategoryGetDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DeletedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Product>? Products { get; set; }
}
