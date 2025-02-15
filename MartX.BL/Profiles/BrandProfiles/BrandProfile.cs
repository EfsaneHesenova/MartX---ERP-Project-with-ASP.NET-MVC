using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.Core.Models;

namespace MartX.BL.Profiles.BrandProfiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandGetDto, Brand>().ReverseMap();
            CreateMap<BrandPostDto, Brand>().ReverseMap();
            CreateMap<BrandPutDto, Brand>().ReverseMap();
            CreateMap<BrandPutDto, BrandGetDto>().ReverseMap();
        }
    }
}
