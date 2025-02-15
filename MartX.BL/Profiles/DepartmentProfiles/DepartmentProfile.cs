using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.DepartmentDtos;
using MartX.Core.Models;

namespace MartX.BL.Profiles.DepartmentProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentGetDto, Department>().ReverseMap();
            CreateMap<DepartmentPostDto, Department>().ReverseMap();
            CreateMap<DepartmentPutDto, Department>().ReverseMap();
            CreateMap<DepartmentPutDto, DepartmentGetDto>().ReverseMap();
        }
    }
}
