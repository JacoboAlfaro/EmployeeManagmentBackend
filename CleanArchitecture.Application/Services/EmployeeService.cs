using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Interfaces.IRepositories;
using CleanArchitecture.Application.Interfaces.IServices;
using CleanArchitecture.Application.Models;
using EmployeeApi.DTOs;
using EmployeeApi.Entities;

namespace CleanArchitecture.Application.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IFileStorageService _fileStorageService;

        public EmployeeService(IEmployeeRepository repository, IFileStorageService fileStorageService)
        {
            _repository = repository;
            _fileStorageService = fileStorageService;
        }
        public async Task<List<GetEmployeeResponse>> GetAllAsync(string baseUrl)
        {
            var employees = await _repository.GetAllAsync();

            return employees
                .Select(e => new GetEmployeeResponse(
                    e.EmployeeId,
                    e.FullName,
                    e.Email,
                    e.Salary,
                    !string.IsNullOrEmpty(e.Image) ? $"{baseUrl}/images/{e.Image}" : $"{baseUrl}/images/default.png",
                    e.Department.Name,
                    e.DepartmentId)
                )
                .ToList();
        }

        public async Task<GetEmployeeResponse?> GetByIdAsync(int id, string baseUrl)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null){
                throw new NotFoundException("Empleado no encontrado");
            }

            return new GetEmployeeResponse(
                employee.EmployeeId,
                employee.FullName,
                employee.Email,
                employee.Salary,
                !string.IsNullOrEmpty(employee.Image) ? $"{baseUrl}/images/{employee.Image}" : $"{baseUrl}/images/default.png",
                employee.Department.Name,
                employee.DepartmentId
            );
        }

        public async Task AddAsync(CreateEmployeeRequest request, FileUpload? image)
        {
            string? fileName = null;

            if (image != null)
            {
                fileName = await _fileStorageService.SaveAsync(image);
            }

            var existingEmployee = await _repository.GetByEmail(request.email);
            if (existingEmployee != null) throw new BusinessException("Ya existe un empleado con este correo");

            var employee = new Employee
            {
                FullName = request.fullName,
                Email = request.email,
                Salary = request.salary,
                Image = fileName,
                DepartmentId = request.departmentId
            };

            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateEmployeeRequest request, FileUpload? image)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null) throw new NotFoundException("Empleado no encontrado");

            employee.FullName = request.fullName;
            employee.Email = request.email;
            employee.Salary = request.salary;
            employee.DepartmentId = request.departmentId;

            if (image != null)
            {
                _fileStorageService.Delete(employee.Image);

                employee.Image = await _fileStorageService.SaveAsync(image);
            }

            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null) throw new NotFoundException("Empleado no encontrado");

            _fileStorageService.Delete(employee.Image);

            await _repository.DeleteAsync(employee);
            await _repository.SaveChangesAsync();
        }
    }
}
