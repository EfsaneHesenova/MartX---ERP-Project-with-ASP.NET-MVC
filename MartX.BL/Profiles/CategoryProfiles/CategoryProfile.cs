using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.CategoryDtos;
using MartX.Core.Models;

namespace MartX.BL.Profiles.CategoryProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryGetDto, Category>().ReverseMap();
            CreateMap<CategoryPostDto, Category>().ReverseMap();
            CreateMap<CategoryPutDto, Category>().ReverseMap();
            CreateMap<CategoryPutDto, CategoryGetDto>().ReverseMap();
        }
    }
}
