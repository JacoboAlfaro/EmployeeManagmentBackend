using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Application.Interfaces.IRepositories;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Infrastructure.Extensions;


namespace CleanArchitecture.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeDBContext _context;
        public DepartmentRepository(EmployeeDBContext context)
        {
            _context = context;
        }
        public async Task<PagedList<Department>> GetAllDepartmentsAsync(PaginationParams paginationParams)
        {
            var query = _context.Departments.AsNoTracking();

            return await query.ToPagedListAsync(paginationParams);
        }


        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task<Department?> GetDepartmentByName(string name)
        {
            return await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
        }

        public async Task CreateDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public async Task UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            await Task.CompletedTask;
        }
        
        public async Task DeleteDepartment(Department department)
        {
            _context.Departments.Remove(department);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
