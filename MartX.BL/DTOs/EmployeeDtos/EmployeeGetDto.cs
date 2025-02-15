using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MartX.BL.DTOs.EmployeeDtos;

public class EmployeeGetDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte Age { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string ImageUrl { get; set; }
    public Guid DepartmentId { get; set; }
    public Department? Department { get; set; }
    public ICollection<DocumentImageUrl>? DocumentImageUrls { get; set; }
}
