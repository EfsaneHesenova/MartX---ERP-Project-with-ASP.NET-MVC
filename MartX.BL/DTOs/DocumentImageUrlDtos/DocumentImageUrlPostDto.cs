using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.DTOs.DocumentImageUrlDtos;

public class DocumentImageUrlPostDto
{
    public string Title { get; set; }
    public IFormFile Image { get; set; }
    public Guid EmployeeId { get; set; }
    public ICollection<SelectListItem>? Employees { get; set; }
}
