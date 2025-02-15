using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.ProductDtos;
using MartX.Core.Models;

namespace MartX.BL.Profiles.ProductProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductGetDto, Product>().ReverseMap();
        CreateMap<ProductPostDto, Product>().ReverseMap();
        CreateMap<ProductPutDto, Product>().ReverseMap();
        CreateMap<ProductPutDto, ProductGetDto>().ReverseMap();

    }
}
