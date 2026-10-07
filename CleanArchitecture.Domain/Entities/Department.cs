using EmployeeApi.Entities;

namespace CleanArchitecture.Domain.Entities
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
