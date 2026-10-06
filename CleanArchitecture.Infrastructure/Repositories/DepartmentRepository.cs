using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Application.Interfaces.IRepositories;


namespace CleanArchitecture.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeDBContext _context;
        public DepartmentRepository(EmployeeDBContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }
    }
}
