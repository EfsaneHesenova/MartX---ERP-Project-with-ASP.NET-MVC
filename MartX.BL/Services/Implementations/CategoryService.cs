using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.CategoryDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using MartX.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MartX.BL.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _categoryReadRepository;
    private readonly ICategoryWriteRepository _categoryWriteRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;
    IWebHostEnvironment _webHostEnvironment;

    public CategoryService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IFileUploadService fileUploadService, ICategoryWriteRepository categoryWriteRepository, ICategoryReadRepository categoryReadRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
        _categoryWriteRepository = categoryWriteRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<bool> CreateCategoryAsync(CategoryPostDto categoryPostDto)
    {
        Category category = _mapper.Map<Category>(categoryPostDto);
        category.CreatedAt = DateTime.Now;

        await _categoryWriteRepository.CreateAsync(category);

        int rows = await _categoryWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
        return true;
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        if (!await _categoryReadRepository.IsExist(id)) { throw new Exception("Category not found"); }
        Category category = await _categoryReadRepository.GetByIdAsync(id);
        if (category is null)
        {
            throw new Exception("Category not found");
        }
        _categoryWriteRepository.Delete(category);
        int rows = await _categoryWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<CategoryGetDto>> GetAllCategory(int size = 10, int page = 0)
    {
        ICollection<Category> categories= await _categoryReadRepository.GetAllByCondition(p => !p.IsDeleted, page, size, true).ToListAsync();
        ICollection<CategoryGetDto> categoryGets = _mapper.Map<ICollection<CategoryGetDto>>(categories);
        return categoryGets;
    }

    public async Task<ICollection<CategoryGetDto>> GetAllCategoryAsync()
    {
        ICollection<Category> categoryGets = await _categoryReadRepository.GetAllByCondition(p => !p.IsDeleted, true).ToListAsync();
        ICollection<CategoryGetDto> categorys = _mapper.Map<ICollection<CategoryGetDto>>(categoryGets);
        return categorys;
    }

    public async Task<ICollection<CategoryGetDto>> GetAllSoftDeletedCategory()
    {
        ICollection<Category> categories = await _categoryReadRepository.GetAllByCondition(p => p.IsDeleted, true).ToListAsync();
        ICollection<CategoryGetDto> categoryGetDtos= _mapper.Map<ICollection<CategoryGetDto>>(categories);
        return categoryGetDtos;
    }

    public async Task<CategoryGetDto> GetByIdCategoryAsync(Guid id)
    {
        if (!await _categoryReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Category category = await _categoryReadRepository.GetByIdAsync(id);
        if (category is null)
        {
            throw new Exception("Something went wrong");
        }
        CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);
        return dto;
    }

    public async Task RestoreCategoryAsync(Guid id)
    {
        if (!await _categoryReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Category category = await _categoryReadRepository.GetOneByCondition(x => x.Id == id && x.IsDeleted);
        category.IsDeleted = false;
        category.DeletedAt = null;
        _categoryWriteRepository.Update(category);
        int rows = await _categoryWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<SelectListItem>> SelectAllCategory()
    {
        return await _categoryReadRepository.SelectAllCategoryAsync();
    }

    public async Task SoftDeleteCategoryAsync(Guid id)
    {
        if (!await _categoryReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Category category = await _categoryReadRepository.GetOneByCondition(x => x.Id == id && !x.IsDeleted);
        category.IsDeleted = true;
        category.DeletedAt = DateTime.Now;
        _categoryWriteRepository.Update(category);
        int rows = await _categoryWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task UpdateCategoryAsync(CategoryPutDto categoryPutDto)
    {

        if (!await _categoryReadRepository.IsExist(categoryPutDto.Id)) { throw new Exception("Something went wrong"); }
        Category oldCategory = await _categoryReadRepository.GetByIdAsync(categoryPutDto.Id);
        Category category = _mapper.Map<Category>(categoryPutDto);
        category.DeletedAt = oldCategory.DeletedAt;
        category.CreatedAt = oldCategory.CreatedAt;
        category.UpdatedAt = DateTime.Now;
        _categoryWriteRepository.Update(category);
        int rows = await _categoryWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }
}
