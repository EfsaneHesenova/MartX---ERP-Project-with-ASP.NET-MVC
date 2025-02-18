using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;

namespace MartX.BL.DTOs.SupplierDtos;

public class SupplierGetDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string ContactPerson { get; set; }
    public DateTime DeletedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Address { get; set; }
    public ICollection<Brand>? Brands { get; set; }
}
