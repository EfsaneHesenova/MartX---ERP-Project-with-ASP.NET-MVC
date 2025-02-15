using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.DocumentImageUrlDtos;
using MartX.BL.DTOs.EmployeeDtos;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Implementations;

public class DocumentImageUrlService : IDocumentImageUrlService
{
    private readonly IDocumentImageUrlReadRepository _documentImageUrlReadRepository;
    private readonly IDocumentImageUrlWriteRepository _documentImageUrlWriteRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;
    IWebHostEnvironment _webHostEnvironment;

    public DocumentImageUrlService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IFileUploadService fileUploadService, IDocumentImageUrlWriteRepository documentImageUrlWriteRepository, IDocumentImageUrlReadRepository documentImageUrlReadRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
        _documentImageUrlWriteRepository = documentImageUrlWriteRepository;
        _documentImageUrlReadRepository = documentImageUrlReadRepository;
    }

    public async Task<bool> CreateDocumentImageUrlAsync(DocumentImageUrlPostDto documentImageUrlPostDto)
    {
        DocumentImageUrl documentImageUrl = _mapper.Map<DocumentImageUrl>(documentImageUrlPostDto);
        documentImageUrl.ImageUrl = await _fileUploadService.UploadFileAsync(documentImageUrlPostDto.Image, _webHostEnvironment.WebRootPath, new[] { ".png", ".jpg", ".jpeg" });

        await _documentImageUrlWriteRepository.CreateAsync(documentImageUrl);

        int rows = await _documentImageUrlWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
        return true;
    }

    public async Task DeleteDocumentImageUrlAsync(Guid id)
    {
        if (!await _documentImageUrlReadRepository.IsExist(id)) { throw new Exception("DocumentImageUrl not found"); }
        DocumentImageUrl documentImageUrl = await _documentImageUrlReadRepository.GetByIdAsync(id);
        if (documentImageUrl is null)
        {
            throw new Exception("DocumentImageUrl not found");
        }
        _documentImageUrlWriteRepository.Delete(documentImageUrl);
        int rows = await _documentImageUrlWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<ICollection<DocumentImageUrlGetDto>> GetAllDocumentImageUrlAsync()
    {
        ICollection<DocumentImageUrl> documentImageUrlGets = await _documentImageUrlReadRepository.GetAllAsync(true, "Employee");
        ICollection<DocumentImageUrlGetDto> documentImageUrls = _mapper.Map<ICollection<DocumentImageUrlGetDto>>(documentImageUrlGets);
        return documentImageUrls;
    }

    public async Task<DocumentImageUrlGetDto> GetByIdDocumentImageUrlAsync(Guid id)
    {
        if (!await _documentImageUrlReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        DocumentImageUrl documentImageUrl = await _documentImageUrlReadRepository.GetByIdAsync(id);
        if (documentImageUrl is null)
        {
            throw new Exception("Something went wrong");
        }
        DocumentImageUrlGetDto dto = _mapper.Map<DocumentImageUrlGetDto>(documentImageUrl);
        return dto;
    }

    public async Task RestoreDocumentImageUrlAsync(Guid id)
    {
        if (!await _documentImageUrlReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        DocumentImageUrl documentImageUrl = await _documentImageUrlReadRepository.GetOneByCondition(x => x.Id == id && x.IsDeleted);
        documentImageUrl.IsDeleted = false;
        _documentImageUrlWriteRepository.Update(documentImageUrl);
        int rows = await _documentImageUrlWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

   

    public async Task SoftDeleteDocumentImageUrlAsync(Guid id)
    {
        if (!await _documentImageUrlReadRepository.IsExist(id)) { throw new Exception("Something went wrong"); }
        DocumentImageUrl documentImageUrl = await _documentImageUrlReadRepository.GetOneByCondition(x => x.Id == id && !x.IsDeleted);
        documentImageUrl.IsDeleted = true;
        _documentImageUrlWriteRepository.Update(documentImageUrl);
        int rows = await _documentImageUrlWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task UpdateDocumentImageUrlAsync(DocumentImageUrlPutDto documentImageUrlPutDto)
    {
        if (!await _documentImageUrlReadRepository.IsExist(documentImageUrlPutDto.Id)) { throw new Exception("Something went wrong"); }
        DocumentImageUrl documentImageUrl = _mapper.Map<DocumentImageUrl>(documentImageUrlPutDto);
        documentImageUrl.ImageUrl = await _fileUploadService.UploadFileAsync(documentImageUrlPutDto.Image, _webHostEnvironment.WebRootPath, new[] { ".png", ".jpg", ".jpeg" });
        _documentImageUrlWriteRepository.Update(documentImageUrl);
        int rows = await _documentImageUrlWriteRepository.SaveChangesAsync();
        if (rows == 0)
        {
            throw new Exception("Something went wrong");
        }
    }
}
