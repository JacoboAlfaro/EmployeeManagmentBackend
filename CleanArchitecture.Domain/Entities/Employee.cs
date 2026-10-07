using CleanArchitecture.Domain.Entities;

namespace EmployeeApi.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal Salary { get; set; }

    public string? Image { get; set; }

    public int DepartmentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Department Department { get; set; } = null!;
}
