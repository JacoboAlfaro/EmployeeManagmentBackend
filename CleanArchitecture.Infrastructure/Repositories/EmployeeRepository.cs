using CleanArchitecture.Application.Interfaces.IRepositories;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Extensions;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly EmployeeDBContext _context;

        public EmployeeRepository(EmployeeDBContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Employee>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.Employees
                .AsNoTracking()
                .Include(d => d.Department);

            return await query.ToPagedListAsync(paginationParams);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee?> GetByEmail(string email)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            var result = Task.CompletedTask;
            return result;
        }

        public Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);

            var result = Task.CompletedTask;
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
