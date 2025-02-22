using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Abstractions;

public interface IProductService
{
    Task<bool> CreateProductAsync(ProductPostDto productPostDto);
    Task DeleteProductAsync(Guid id);
    Task SoftDeleteProductAsync(Guid id);
    Task RestoreProductAsync(Guid id);
    Task UpdateProductAsync(ProductPutDto productPutDto);
    Task<ICollection<ProductGetDto>> GetAllSoftDeletedProduct();
    Task<ICollection<ProductGetDto>> GetAllProduct(int size = 10, int page = 0);
    Task<ICollection<ProductGetDto>> GetAllProductAsync();
    Task<ProductGetDto> GetByIdProductAsync(Guid id);
}
