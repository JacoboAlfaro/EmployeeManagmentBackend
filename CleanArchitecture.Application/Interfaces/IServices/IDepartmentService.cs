using CleanArchitecture.Application.Models;
using EmployeeApi.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Interfaces.IServices
{
    public interface IDepartmentService
    {
        Task<PagedList<GetDepartmentResponse>> GetAllDepartmentsAsync(PaginationParams paginationParams);
        Task<GetDepartmentResponse> GetDepartmentByIdAsync(int id);
        Task CreateDepartment(CreateDepartmentRequest request);
        Task UpdateDepartment(int id, UpdateDepartmentRequest request);
        Task DeleteDepartment(int id);
    }
}
