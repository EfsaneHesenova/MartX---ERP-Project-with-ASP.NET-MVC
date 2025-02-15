using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.SupplierDtos;
using MartX.Core.Models;

namespace MartX.BL.Profiles.AccountProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<SupplierGetDto, Supplier>().ReverseMap();
        }
    }
}
