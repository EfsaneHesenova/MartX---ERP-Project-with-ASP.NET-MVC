using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.DocumentImageUrlDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MartX.BL.Services.Abstractions;

public interface IDocumentImageUrlService
{
    Task<bool> CreateDocumentImageUrlAsync(DocumentImageUrlPostDto documentImageUrlPostDto);
    Task DeleteDocumentImageUrlAsync(Guid id);
    Task SoftDeleteDocumentImageUrlAsync(Guid id);
    Task RestoreDocumentImageUrlAsync(Guid id);
    Task UpdateDocumentImageUrlAsync(DocumentImageUrlPutDto documentImageUrlPutDto);
    Task<ICollection<DocumentImageUrlGetDto>> GetAllDocumentImageUrlAsync();
    Task<DocumentImageUrlGetDto> GetByIdDocumentImageUrlAsync(Guid id);
}
