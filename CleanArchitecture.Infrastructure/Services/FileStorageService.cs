using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Interfaces.IServices;
using CleanArchitecture.Application.Models;

namespace CleanArchitecture.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _uploadsFolder;

    public FileStorageService(string uploadsFolder)
    {
        _uploadsFolder = uploadsFolder;
    }

    public async Task<string> SaveAsync(FileUpload file)
    {
        if (!Directory.Exists(_uploadsFolder))
        {
            Directory.CreateDirectory(_uploadsFolder);
        }

        string uniqueFileName =
            $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        string filePath =
            Path.Combine(_uploadsFolder, uniqueFileName);

        using var fileStream =
            new FileStream(filePath, FileMode.Create);

        await file.Content.CopyToAsync(fileStream);

        return uniqueFileName;
    }

    public void Delete(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return;

        string filePath =
            Path.Combine(_uploadsFolder, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}