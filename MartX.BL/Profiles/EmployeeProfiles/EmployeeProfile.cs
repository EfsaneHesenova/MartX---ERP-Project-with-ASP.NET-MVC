using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.EmployeeDtos;
using MartX.Core.Models;

namespace MartX.BL.Profiles.EmployeeProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeGetDto, Employee>().ReverseMap();
        CreateMap<EmployeePostDto, Employee>().ReverseMap();
        CreateMap<EmployeePutDto, Employee>().ReverseMap();
        CreateMap<EmployeePutDto, EmployeeGetDto>().ReverseMap();


    }
}
