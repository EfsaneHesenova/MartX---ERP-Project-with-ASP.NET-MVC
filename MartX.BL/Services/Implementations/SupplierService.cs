using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.SupplierDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Implementations;

public class SupplierService : ISupplierService
{
    private readonly ISupplierReadRepository _supplierReadRepository;
    private readonly ISupplierWriteRepository _supplierWriteRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;
    IWebHostEnvironment _webHostEnvironment;

    public SupplierService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IFileUploadService fileUploadService, ISupplierWriteRepository supplierWriteRepository, ISupplierReadRepository supplierReadRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
        _supplierWriteRepository = supplierWriteRepository;
        _supplierReadRepository = supplierReadRepository;
    }

    public async Task<bool> CreateSupplierAsync(SupplierPostDto supplierPostDto)
    {
        Supplier supplier = _mapper.Map<Supplier>(supplierPostDto);
        await _supplierWriteRepository.CreateAsync(supplier);

        int rows = await _supplierWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
        return true;
    }

    public async Task DeleteSupplierAsync(Guid id)
    {
        if (!await _supplierReadRepository.IsExist(id)) { throw new Exception("Supplier not found"); }
        Supplier supplier = await _supplierReadRepository.GetByIdAsync(id);
        if (supplier is null)
        {
            throw new Exception("Supplier not found");
        }
        _supplierWriteRepository.Delete(supplier);
        int rows = await _supplierWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<SupplierGetDto>> GetAllSupplierAsync()
    {
        ICollection<Supplier> supplierGets = await _supplierReadRepository.GetAllAsync(true);
        ICollection<SupplierGetDto> suppliers = _mapper.Map<ICollection<SupplierGetDto>>(supplierGets);
        return suppliers;
    }

    public async Task<SupplierGetDto> GetByIdSupplierAsync(Guid id)
    {
        if (!await _supplierReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Supplier supplier = await _supplierReadRepository.GetByIdAsync(id);
        if (supplier is null)
        {
            throw new Exception("Something went wrong");
        }
        SupplierGetDto dto = _mapper.Map<SupplierGetDto>(supplier);
        return dto;
    }

    public async Task RestoreSupplierAsync(Guid id)
    {
        if (!await _supplierReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Supplier supplier = await _supplierReadRepository.GetOneByCondition(x => x.Id == id && x.IsDeleted);
        supplier.IsDeleted = false;
        _supplierWriteRepository.Update(supplier);
        int rows = await _supplierWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<SelectListItem>> SelectAllSupplier()
    {
        return await _supplierReadRepository.SelectAllSupplierAsync();
    }

    public async Task SoftDeleteSupplierAsync(Guid id)
    {
        if (!await _supplierReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Supplier supplier = await _supplierReadRepository.GetOneByCondition(x => x.Id == id && !x.IsDeleted);
        supplier.IsDeleted = true;
        _supplierWriteRepository.Update(supplier);
        int rows = await _supplierWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task UpdateSupplierAsync(SupplierPutDto supplierPutDto)
    {
        if (!await _supplierReadRepository.IsExist(supplierPutDto.Id)) { throw new Exception("Something went wrong"); }
        Supplier supplier = _mapper.Map<Supplier>(supplierPutDto);
        _supplierWriteRepository.Update(supplier);
        int rows = await _supplierWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }
}
