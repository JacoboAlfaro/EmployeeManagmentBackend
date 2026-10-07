using CleanArchitecture.Application.Models;
using EmployeeApi.Entities;

namespace CleanArchitecture.Application.Interfaces.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee>> GetAllAsync(PaginationParams paginationParams);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> GetByEmail(string email);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task SaveChangesAsync();
    }
}
