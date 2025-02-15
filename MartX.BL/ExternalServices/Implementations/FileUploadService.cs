using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.ExternalServices.Abstractions;
using Microsoft.AspNetCore.Http;

namespace MartX.BL.ExternalServices.Implementations;

public class FileUploadService : IFileUploadService
{
    public async Task<string> UploadFileAsync(IFormFile file, string envPath, string[] allowedExtensions)
    {
        if (file is null) throw new ArgumentNullException(nameof(file));
        string contentPath = Path.Combine(envPath, "uploads");
        if (!File.Exists(contentPath))
        {
            Directory.CreateDirectory(contentPath);
        }
        var ext = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(ext))
        {
            throw new ArgumentException();
        }
        var fileName = $"{Guid.NewGuid()}{ext}";
        var path = Path.Combine(contentPath, fileName);
        using FileStream fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return fileName;
    }
}
