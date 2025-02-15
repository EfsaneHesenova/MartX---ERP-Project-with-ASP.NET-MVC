using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.SupplierDtos;
using MartX.Core.Models;

namespace MartX.BL.Profiles.SupplierProfiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<SupplierGetDto, Supplier>().ReverseMap();
        CreateMap<SupplierPostDto, Supplier>().ReverseMap();
        CreateMap<SupplierPutDto, Supplier>().ReverseMap();
        CreateMap<SupplierPutDto, SupplierGetDto>().ReverseMap();

    }
}
