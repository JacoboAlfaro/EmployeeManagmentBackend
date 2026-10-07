using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.IRepositories;
using CleanArchitecture.Application.Interfaces.IServices;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain.Entities;
using EmployeeApi.DTOs;

namespace CleanArchitecture.Application.Services
{
    public class DepartmentService: IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<GetDepartmentResponse>> GetAllDepartmentsAsync(PaginationParams paginationParams)
        {
            var departments = await _repository.GetAllDepartmentsAsync(paginationParams);
            var departmentsResponse = departments.Items
                .Select(d => new GetDepartmentResponse(d.DepartmentId, d.Name)).ToList();

            return new PagedList<GetDepartmentResponse>(departmentsResponse, departments.TotalCount, departments.CurrentPage, departments.PageSize);
        }

        public async Task<GetDepartmentResponse> GetDepartmentByIdAsync(int id)
        {
            var department = await _repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                throw new NotFoundException("Departamento no encontrado");
            }

            return new GetDepartmentResponse(department.DepartmentId, department.Name);
        }

        public async Task CreateDepartment(CreateDepartmentRequest request)
        {
            //Validar que el nombre del departamento no exista
            var departmentExist = await _repository.GetDepartmentByName(request.name);
            if (departmentExist != null)
            {
                throw new BusinessException($"El departamento con nombre {request.name} ya existe");
            }

            // Crear el nuevo departamento para mandarlo a repository
            var department = new Department
            {
                Name = request.name
            };
            await _repository.CreateDepartment(department);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateDepartment(int id, UpdateDepartmentRequest request)
        {
            // Verificar que el departamento exista
            var department = await _repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                throw new NotFoundException("Departamento no encontrado");
            }

            // Validar que el nombre del departamento no exista
            var departmentExist = await _repository.GetDepartmentByName(request.name);
            if (departmentExist != null && departmentExist.DepartmentId != id)
            {
                throw new BusinessException($"El departamento con nombre {request.name} ya existe");
            }

            department.Name = request.name;
            await _repository.UpdateDepartment(department);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                throw new NotFoundException("Departamento no encontrado");
            }
            await _repository.DeleteDepartment(department);
            await _repository.SaveChangesAsync();
        }
    }
}
