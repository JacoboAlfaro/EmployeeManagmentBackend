using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces.IRepositories;

public interface IDepartmentRepository
{
    Task<PagedList<Department>> GetAllDepartmentsAsync(PaginationParams paginationParams);
    Task<Department?> GetDepartmentByIdAsync(int id);
    Task<Department?> GetDepartmentByName(string name);
    Task CreateDepartment(Department department);
    Task UpdateDepartment(Department department);
    Task DeleteDepartment(Department department);
    Task SaveChangesAsync();
}
