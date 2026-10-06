using CleanArchitecture.Application.Interfaces.IRepositories;
using CleanArchitecture.Application.Interfaces.IServices;
using EmployeeApi.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Services
{
    public class DepartmentService: IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetDepartmentResponse>> GetAllDepartmentsAsync()
        {
            var departments = await _repository.GetAllDepartmentsAsync();
            return departments.Select(d => new GetDepartmentResponse(d.DepartmentId, d.Name)).ToList();
        }
    }
}
