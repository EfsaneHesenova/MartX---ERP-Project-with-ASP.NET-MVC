using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.EmployeeDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using MartX.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MartX.BL.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeReadRepository _employeeReadRepository;
    private readonly IEmployeeWriteRepository _employeeWriteRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;
    IWebHostEnvironment _webHostEnvironment;

    public EmployeeService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IFileUploadService fileUploadService, IEmployeeWriteRepository employeeWriteRepository, IEmployeeReadRepository employeeReadRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
        _employeeWriteRepository = employeeWriteRepository;
        _employeeReadRepository = employeeReadRepository;
    }

    public async Task<bool> CreateEmployeeAsync(EmployeePostDto employeePostDto)
    {
        Employee employee = _mapper.Map<Employee>(employeePostDto);
        employee.CreatedAt = DateTime.Now;

        employee.ImageUrl = await _fileUploadService.UploadFileAsync(employeePostDto.Image, _webHostEnvironment.WebRootPath, new[] { ".png", ".jpg", ".jpeg" });

        await _employeeWriteRepository.CreateAsync(employee);

        int rows = await _employeeWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
        return true;
    }

    public async Task DeleteEmployeeAsync(Guid id)
    {
        if (!await _employeeReadRepository.IsExist(id)) { throw new Exception("Employee not found"); }
        Employee employee = await _employeeReadRepository.GetByIdAsync(id);
        if (employee is null)
        {
            throw new Exception("Employee not found");
        }
        _employeeWriteRepository.Delete(employee);
        int rows = await _employeeWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<EmployeeGetDto>> GetAllEmployeeAsync()
    {
        ICollection<Employee> employeeGets = await _employeeReadRepository.GetAllByCondition(x => !x.IsDeleted, true, "Department").ToListAsync();
        ICollection<EmployeeGetDto> employees = _mapper.Map<ICollection<EmployeeGetDto>>(employeeGets);
        return employees;
    }

    public async Task<ICollection<EmployeeGetDto>> GetAllSoftDeletedEmployee()
    {
        ICollection<Employee> employees = await _employeeReadRepository.GetAllByCondition(p => p.IsDeleted, true, "Department").ToListAsync();
        ICollection<EmployeeGetDto> employeeGets = _mapper.Map<ICollection<EmployeeGetDto>>(employees);
        return employeeGets;
    }

    public async Task<EmployeeGetDto> GetByIdEmployeeAsync(Guid id)
    {
        if (!await _employeeReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Employee employee = await _employeeReadRepository.GetByIdAsync(id);
        if (employee is null)
        {
            throw new Exception("Something went wrong");
        }
        EmployeeGetDto dto = _mapper.Map<EmployeeGetDto>(employee);
        return dto;
    }

    public async Task RestoreEmployeeAsync(Guid id)
    {
        if (!await _employeeReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Employee employee = await _employeeReadRepository.GetOneByCondition(x => x.Id == id && x.IsDeleted);
        employee.IsDeleted = false;
        employee.DeletedAt = null;
        _employeeWriteRepository.Update(employee);
        int rows = await _employeeWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<SelectListItem>> SelectAllEmployee()
    {
        return await _employeeReadRepository.SelectAllEmployeeAsync();
    }

    public async Task SoftDeleteEmployeeAsync(Guid id)
    {
        if (!await _employeeReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Employee employee = await _employeeReadRepository.GetOneByCondition(x => x.Id == id && !x.IsDeleted);
        employee.IsDeleted = true;
        employee.DeletedAt = DateTime.Now;
        _employeeWriteRepository.Update(employee);
        int rows = await _employeeWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task UpdateEmployeeAsync(EmployeePutDto employeePutDto)
    {
        if (!await _employeeReadRepository.IsExist(employeePutDto.Id)) { throw new Exception("Something went wrong"); }
        Employee oldEmployee = await _employeeReadRepository.GetByIdAsync(employeePutDto.Id);
        Employee employee = _mapper.Map<Employee>(employeePutDto);
        employee.DeletedAt = oldEmployee.DeletedAt;
        employee.CreatedAt = oldEmployee.CreatedAt;
        employee.UpdatedAt = DateTime.Now;
        employee.ImageUrl = await _fileUploadService.UploadFileAsync(employeePutDto.Image, _webHostEnvironment.WebRootPath, new[] { ".png", ".jpg", ".jpeg" });
        _employeeWriteRepository.Update(employee);
        int rows = await _employeeWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }
}
