using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.DepartmentDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public async Task<ICollection<DepartmentGetDto>> GetAllDepartmentAsync()
    {
        ICollection<Department> departmentGets = await _departmentReadRepository.GetAllAsync(true);
        ICollection<DepartmentGetDto> departments = _mapper.Map<ICollection<DepartmentGetDto>>(departmentGets);
        return departments;
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
        Department department = _mapper.Map<Department>(departmentPutDto);
        _departmentWriteRepository.Update(department);
        int rows = await _departmentWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }
}
