using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MartX.BL.DTOs.DocumentImageUrlDtos;

public class DocumentImageUrlGetDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
