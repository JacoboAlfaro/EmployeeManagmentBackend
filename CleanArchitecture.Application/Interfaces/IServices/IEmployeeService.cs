using CleanArchitecture.Application.Models;
using EmployeeApi.DTOs;
using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<PagedList<GetEmployeeResponse>> GetAllAsync(string baseUrl, PaginationParams paginationParams);
        Task<GetEmployeeResponse?> GetByIdAsync(int id, string baseUrl);
        Task AddAsync(CreateEmployeeRequest request, FileUpload? image);
        Task UpdateAsync(int id, UpdateEmployeeRequest request, FileUpload? image);
        Task DeleteAsync(int id);
    }
}
