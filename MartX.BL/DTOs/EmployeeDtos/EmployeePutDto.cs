using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.DTOs.EmployeeDtos;

public class EmployeePutDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte Age { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public IFormFile Image { get; set; }
    public Guid DepartmentId { get; set; }
    public ICollection<SelectListItem>? Departments { get; set; }
    public ICollection<DocumentImageUrl>? DocumentImageUrls { get; set; }
}
