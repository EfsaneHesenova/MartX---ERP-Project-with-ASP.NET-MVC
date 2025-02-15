using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.DocumentImageUrlDtos;
using MartX.BL.DTOs.ProductDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;
    IWebHostEnvironment _webHostEnvironment;

    public ProductService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IFileUploadService fileUploadService, IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task<bool> CreateProductAsync(ProductPostDto productPostDto)
    {
        Product product = _mapper.Map<Product>(productPostDto);
        product.ImageUrl = await _fileUploadService.UploadFileAsync(productPostDto.Image, _webHostEnvironment.WebRootPath, new[] { ".png", ".jpg", ".jpeg" });

        await _productWriteRepository.CreateAsync(product);

        int rows = await _productWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
        return true;
    }

    public async Task DeleteProductAsync(Guid id)
    {
        if (!await _productReadRepository.IsExist(id)) { throw new Exception("Product not found"); }
        Product product = await _productReadRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new Exception("Product not found");
        }
        _productWriteRepository.Delete(product);
        int rows = await _productWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<ProductGetDto>> GetAllProductAsync()
    {
        ICollection<Product> productGets = await _productReadRepository.GetAllAsync(true, "Brand", "Category");
        ICollection<ProductGetDto> products = _mapper.Map<ICollection<ProductGetDto>>(productGets);
        return products;
    }

    public async Task<ProductGetDto> GetByIdProductAsync(Guid id)
    {
        if (!await _productReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Product product = await _productReadRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new Exception("Something went wrong");
        }
        ProductGetDto dto = _mapper.Map<ProductGetDto>(product);
        return dto;
    }

    public async Task RestoreProductAsync(Guid id)
    {
        if (!await _productReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Product product = await _productReadRepository.GetOneByCondition(x => x.Id == id && x.IsDeleted);
        product.IsDeleted = false;
        _productWriteRepository.Update(product);
        int rows = await _productWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task SoftDeleteProductAsync(Guid id)
    {
        if (!await _productReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        Product product = await _productReadRepository.GetOneByCondition(x => x.Id == id && !x.IsDeleted);
        product.IsDeleted = true;
        _productWriteRepository.Update(product);
        int rows = await _productWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task UpdateProductAsync(ProductPutDto productPutDto)
    {
        if (!await _productReadRepository.IsExist(productPutDto.Id)) { throw new Exception("Something went wrong"); }
        Product product = _mapper.Map<Product>(productPutDto);
        product.ImageUrl = await _fileUploadService.UploadFileAsync(productPutDto.Image, _webHostEnvironment.WebRootPath, new[] { ".png", ".jpg", ".jpeg" });

        _productWriteRepository.Update(product);
        int rows = await _productWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }
}
