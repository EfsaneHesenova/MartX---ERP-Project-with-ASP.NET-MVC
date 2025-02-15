using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.DepartmentDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Abstractions
{
    public interface IDepartmentService
    {
        Task<bool> CreateDepartmentAsync(DepartmentPostDto departmentPostDto);
        Task DeleteDepartmentAsync(Guid id);
        Task SoftDeleteDepartmentAsync(Guid id);
        Task RestoreDepartmentAsync(Guid id);
        Task UpdateDepartmentAsync(DepartmentPutDto departmentPutDto);
        Task<ICollection<DepartmentGetDto>> GetAllDepartmentAsync();
        Task<DepartmentGetDto> GetByIdDepartmentAsync(Guid id);
        Task<ICollection<SelectListItem>> SelectAllDepartment();
    }
}
