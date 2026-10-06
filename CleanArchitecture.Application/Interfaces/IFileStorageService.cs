using CleanArchitecture.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(FileUpload file);
        void Delete(string? fileName);
    }
}
