namespace CleanArchitecture.WebApi.Models
{
    public record CreateEmployeeForm(
    string fullName,
    string email,
    decimal salary,
    IFormFile? image,
    int departmentId
);
}
