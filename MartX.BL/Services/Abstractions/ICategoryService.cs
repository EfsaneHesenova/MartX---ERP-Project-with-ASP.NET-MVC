﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.CategoryDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(CategoryPostDto categoryPostDto);
        Task DeleteCategoryAsync(Guid id);
        Task SoftDeleteCategoryAsync(Guid id);
        Task RestoreCategoryAsync(Guid id);
        Task UpdateCategoryAsync(CategoryPutDto categoryPutDto);
        Task<ICollection<CategoryGetDto>> GetAllCategory(int size = 10, int page = 0);
        Task<ICollection<CategoryGetDto>> GetAllSoftDeletedCategory();
        Task<ICollection<CategoryGetDto>> GetAllCategoryAsync();
        Task<CategoryGetDto> GetByIdCategoryAsync(Guid id);
        Task<ICollection<SelectListItem>> SelectAllCategory();
    }
}
