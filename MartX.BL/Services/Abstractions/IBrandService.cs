using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.BrandDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Abstractions
{
    public interface IBrandService
    {
        Task<bool> CreateBrandAsync(BrandPostDto brandPostDto);
        Task DeleteBrandAsync(Guid id);
        Task SoftDeleteBrandAsync(Guid id);
        Task RestoreBrandAsync(Guid id);
        Task UpdateBrandAsync(BrandPutDto brandPutDto);
        Task<ICollection<BrandGetDto>> GetAllBrand();
        Task<ICollection<BrandGetDto>> GetAllSoftDeletedBrand();
        Task<BrandGetDto> GetByIdBrandAsync(Guid id);
        Task<ICollection<SelectListItem>> SelectAllBrand();
    }
}
