using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces.IRepositories;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllDepartmentsAsync();
}
