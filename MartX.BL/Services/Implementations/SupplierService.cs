using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.ProductDtos;
using MartX.BL.DTOs.SupplierDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using MartX.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        supplier.CreatedAt = DateTime.Now;
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

    public async Task<ICollection<SupplierGetDto>> GetAllSoftDeletedSupplier()
    {
        ICollection<Supplier> suppliers = await _supplierReadRepository.GetAllByCondition(p => p.IsDeleted, true).ToListAsync();
        ICollection<SupplierGetDto> supplierGets = _mapper.Map<ICollection<SupplierGetDto>>(suppliers);
        return supplierGets;
    }

    public async Task<ICollection<SupplierGetDto>> GetAllSupplier(int size = 10, int page = 0)
    {
        ICollection<Supplier> suppliers = await _supplierReadRepository.GetAllByCondition(p => !p.IsDeleted, page, size, true).ToListAsync();
        ICollection<SupplierGetDto> supplierGets = _mapper.Map<ICollection<SupplierGetDto>>(suppliers);
        return supplierGets;
    }

    public async Task<ICollection<SupplierGetDto>> GetAllSupplierAsync()
    {
        ICollection<Supplier> supplierGets = await _supplierReadRepository.GetAllByCondition(p => !p.IsDeleted, true).ToListAsync();
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
        supplier.DeletedAt = null;
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
        supplier.DeletedAt = DateTime.Now;
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
        Supplier oldSupplier = await _supplierReadRepository.GetByIdAsync(supplierPutDto.Id);
        Supplier supplier = _mapper.Map<Supplier>(supplierPutDto);
        supplier.DeletedAt = oldSupplier.DeletedAt;
        supplier.CreatedAt = oldSupplier.CreatedAt;
        supplier.UpdatedAt = DateTime.Now;
        _supplierWriteRepository.Update(supplier);
        int rows = await _supplierWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }
}
