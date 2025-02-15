using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.DocumentImageUrlDtos;
using MartX.Core.Models;

namespace MartX.BL.Profiles.DocumentImageUrlProfiles
{
    public class DocumentImageUrlProfile : Profile
    {
        public DocumentImageUrlProfile()
        {
            CreateMap<DocumentImageUrlGetDto,DocumentImageUrl>().ReverseMap();
            CreateMap<DocumentImageUrlPostDto, DocumentImageUrl>().ReverseMap();
            CreateMap<DocumentImageUrlPutDto, DocumentImageUrl>().ReverseMap();

        }
    }
}
