using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;

namespace MartX.BL.DTOs.CategoryDtos
{
    public class CategoryPostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
