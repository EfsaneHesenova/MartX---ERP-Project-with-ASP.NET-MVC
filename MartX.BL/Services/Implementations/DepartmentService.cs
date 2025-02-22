using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.CategoryDtos;
using MartX.BL.DTOs.DepartmentDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using MartX.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MartX.BL.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentReadRepository _departmentReadRepository;
    private readonly IDepartmentWriteRepository _departmentWriteRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;
    IWebHostEnvironment _webHostEnvironment;

    public DepartmentService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IFileUploadService fileUploadService, IDepartmentWriteRepository departmentWriteRepository, IDepartmentReadRepository departmentReadRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
        _departmentWriteRepository = departmentWriteRepository;
        _departmentReadRepository = departmentReadRepository;
    }

    public async Task<bool> CreateDepartmentAsync(DepartmentPostDto departmentPostDto)
    {
        Department department = _mapper.Map<Department>(departmentPostDto);
        department.CreatedAt = DateTime.Now;

        await _departmentWriteRepository.CreateAsync(department);

        int rows = await _departmentWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
        return true;
    }

    public async Task DeleteDepartmentAsync(Guid id)
    {
        if (!await _departmentReadRepository.IsExist(id)) { throw new Exception("Department not found"); }
        Department department = await _departmentReadRepository.GetByIdAsync(id);
        if (department is null)
        {
            throw new Exception("Department not found");
        }
        _departmentWriteRepository.Delete(department);
        int rows = await _departmentWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<DepartmentGetDto>> GetAllDepartment(int size = 10, int page = 0)
    {
        ICollection<Department> departments = await _departmentReadRepository.GetAllByCondition(p => !p.IsDeleted, page, size, true).ToListAsync();
        ICollection<DepartmentGetDto> departmentGets = _mapper.Map<ICollection<DepartmentGetDto>>(departments);
        return departmentGets;
    }

    public async Task<ICollection<DepartmentGetDto>> GetAllDepartmentAsync()
    {
        ICollection<Department> departmentGets = await _departmentReadRepository.GetAllByCondition(p => !p.IsDeleted, true).ToListAsync();
        ICollection<DepartmentGetDto> departments = _mapper.Map<ICollection<DepartmentGetDto>>(departmentGets);
        return departments;
    }

    public async Task<ICollection<DepartmentGetDto>> GetAllSoftDeletedDepartment()
    {
        ICollection<Department> departments = await _departmentReadRepository.GetAllByCondition(p => p.IsDeleted, true).ToListAsync();
        ICollection<DepartmentGetDto> departmentGets = _mapper.Map<ICollection<DepartmentGetDto>>(departments);
        return departmentGets;
    }

    public async Task<DepartmentGetDto> GetByIdDepartmentAsync(Guid id)
    {
        if (!await _departmentReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Department department = await _departmentReadRepository.GetByIdAsync(id);
        if (department is null)
        {
            throw new Exception("Something went wrong");
        }
        DepartmentGetDto dto = _mapper.Map<DepartmentGetDto>(department);
        return dto;
    }

    public async Task RestoreDepartmentAsync(Guid id)
    {
        if (!await _departmentReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Department department = await _departmentReadRepository.GetOneByCondition(x => x.Id == id && x.IsDeleted);
        department.IsDeleted = false;
        department.DeletedAt = null;
        _departmentWriteRepository.Update(department);
        int rows = await _departmentWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<SelectListItem>> SelectAllDepartment()
    {
        return await _departmentReadRepository.SelectAllDepartmentAsync();
    }

    public async Task SoftDeleteDepartmentAsync(Guid id)
    {
        if (!await _departmentReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Department department = await _departmentReadRepository.GetOneByCondition(x => x.Id == id && !x.IsDeleted);
        department.IsDeleted = true;
        department.DeletedAt = DateTime.Now;
        _departmentWriteRepository.Update(department);
        int rows = await _departmentWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task UpdateDepartmentAsync(DepartmentPutDto departmentPutDto)
    {
        if (!await _departmentReadRepository.IsExist(departmentPutDto.Id)) { throw new Exception("Something went wrong"); }
        Department oldDepartment = await _departmentReadRepository.GetByIdAsync(departmentPutDto.Id);
        Department department = _mapper.Map<Department>(departmentPutDto);
        department.DeletedAt = oldDepartment.DeletedAt;
        department.CreatedAt = oldDepartment.CreatedAt;
        department.UpdatedAt = DateTime.Now;
        _departmentWriteRepository.Update(department);
        int rows = await _departmentWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }
}
