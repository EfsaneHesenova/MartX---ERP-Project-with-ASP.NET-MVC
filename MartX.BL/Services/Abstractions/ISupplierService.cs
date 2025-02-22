using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.SupplierDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Abstractions;

public interface ISupplierService
{
    Task<bool> CreateSupplierAsync(SupplierPostDto supplierPostDto);
    Task DeleteSupplierAsync(Guid id);
    Task SoftDeleteSupplierAsync(Guid id);
    Task RestoreSupplierAsync(Guid id);
    Task UpdateSupplierAsync(SupplierPutDto supplierPutDto);
    Task<ICollection<SupplierGetDto>> GetAllSoftDeletedSupplier();
    Task<ICollection<SupplierGetDto>> GetAllSupplier(int size = 10, int page = 0);
    Task<ICollection<SupplierGetDto>> GetAllSupplierAsync();
    Task<SupplierGetDto> GetByIdSupplierAsync(Guid id);
    Task<ICollection<SelectListItem>> SelectAllSupplier();
}
