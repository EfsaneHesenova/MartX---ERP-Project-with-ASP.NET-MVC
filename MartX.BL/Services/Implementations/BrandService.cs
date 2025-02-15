using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using MartX.BL.DTOs.DocumentImageUrlDtos;

namespace MartX.BL.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandReadRepository _brandReadRepository;
        private readonly IBrandWriteRepository _brandWriteRepository;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;
        IWebHostEnvironment _webHostEnvironment;

        public BrandService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IFileUploadService fileUploadService, IBrandWriteRepository brandWriteRepository, IBrandReadRepository brandReadRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
        }

        public async Task<bool> CreateBrandAsync(BrandPostDto brandPostDto)
        {
            Brand brand = _mapper.Map<Brand>(brandPostDto);
            brand.ImageUrl = await _fileUploadService.UploadFileAsync(brandPostDto.Image, _webHostEnvironment.WebRootPath, new[] { ".png", ".jpg", ".jpeg" });

            await _brandWriteRepository.CreateAsync(brand);

            int rows = await _brandWriteRepository.SaveChangesAsync();
            if (rows == 0)
            {
                throw new Exception("Something went wrong");
            }
            return true;
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            if (!await _brandReadRepository.IsExist(id)) { throw new Exception("Brand not found"); }
            Brand brand = await _brandReadRepository.GetByIdAsync(id);
            if (brand is null)
            {
                throw new Exception("Brand not found");
            }
            _brandWriteRepository.Delete(brand);
            int rows = await _brandWriteRepository.SaveChangesAsync();
            if (rows == 0)
            {
                throw new Exception("Something went wrong");
            }
        }

        public async Task<ICollection<BrandGetDto>> GetAllBrand()
        {
            ICollection<Brand> brandGets= await _brandReadRepository.GetAllByCondition(p => !p.IsDeleted,true, "Supplier").ToListAsync();
            ICollection<BrandGetDto> brands = _mapper.Map<ICollection<BrandGetDto>>(brandGets);
            return brands;
        }

        public async Task<ICollection<BrandGetDto>> GetAllSoftDeletedBrand()
        {
            ICollection<Brand> brandGets = await _brandReadRepository.GetAllByCondition(p => p.IsDeleted, true, "Supplier").ToListAsync();
            ICollection<BrandGetDto> brands = _mapper.Map<ICollection<BrandGetDto>>(brandGets);
            return brands;
        }

        public async Task<BrandGetDto> GetByIdBrandAsync(Guid id)
        {
            if (!await _brandReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
            Brand brand = await _brandReadRepository.GetByIdAsync(id);
            if (brand is null)
            {
                throw new Exception("Something went wrong");
            }
            BrandGetDto dto = _mapper.Map<BrandGetDto>(brand);
            return dto;
        }

        public async Task RestoreBrandAsync(Guid id)
        {
            if (!await _brandReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
            Brand brand = await _brandReadRepository.GetOneByCondition(x => x.Id == id && x.IsDeleted);
            brand.IsDeleted = false;
            _brandWriteRepository.Update(brand);
            int rows = await _brandWriteRepository.SaveChangesAsync();
            if (rows == 0)
            {
                throw new Exception("Something went wrong");
            }
        }

        public async Task<ICollection<SelectListItem>> SelectAllBrand()
        {
            return await _brandReadRepository.SelectAllBrandAsync();
        }

        public async Task SoftDeleteBrandAsync(Guid id)
        {
            if (!await _brandReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
            Brand brand = await _brandReadRepository.GetOneByCondition(x => x.Id == id && !x.IsDeleted);
            brand.IsDeleted = true;
            _brandWriteRepository.Update(brand);
            int rows = await _brandWriteRepository.SaveChangesAsync();
            if (rows == 0)
            {
                throw new Exception("Something went wrong");
            }
        }

        public async Task UpdateBrandAsync(BrandPutDto brandPutDto)
        {
            if (!await _brandReadRepository.IsExist(brandPutDto.Id)) { throw new Exception("Something went wrong"); }
            Brand brand = _mapper.Map<Brand>(brandPutDto);
            brand.ImageUrl = await _fileUploadService.UploadFileAsync(brandPutDto.Image, _webHostEnvironment.WebRootPath, new[] { ".png", ".jpg", ".jpeg" });
            _brandWriteRepository.Update(brand);
            int rows = await _brandWriteRepository.SaveChangesAsync();
            if (rows == 0)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}

