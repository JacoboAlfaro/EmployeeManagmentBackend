using EmployeeApi.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Interfaces.IServices
{
    public interface IDepartmentService
    {
        Task<List<GetDepartmentResponse>> GetAllDepartmentsAsync();
    }
}
