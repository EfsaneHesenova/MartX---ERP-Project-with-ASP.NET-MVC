using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.EmployeeDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Abstractions;

public interface IEmployeeService
{
    Task<bool> CreateEmployeeAsync(EmployeePostDto employeePostDto);
    Task DeleteEmployeeAsync(Guid id);
    Task SoftDeleteEmployeeAsync(Guid id);
    Task RestoreEmployeeAsync(Guid id);
    Task UpdateEmployeeAsync(EmployeePutDto employeePutDto);
    Task<ICollection<EmployeeGetDto>> GetAllSoftDeletedEmployee();
    Task<ICollection<EmployeeGetDto>> GetAllEmployee(int size = 10, int page = 0);
    Task<ICollection<EmployeeGetDto>> GetAllEmployeeAsync();
    Task<EmployeeGetDto> GetByIdEmployeeAsync(Guid id);
    Task<ICollection<SelectListItem>> SelectAllEmployee();
}
