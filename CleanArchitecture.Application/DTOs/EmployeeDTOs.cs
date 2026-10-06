namespace EmployeeApi.DTOs
{
    #region requests
    public record CreateEmployeeRequest(string fullName, string email, decimal salary, int departmentId);

    public record UpdateEmployeeRequest(int id,string fullName, string email, decimal salary, int departmentId);

    #endregion

    #region responses
    public record GetEmployeeResponse(int id, string fullName, string email, decimal salary,
        string imageUrl, string departmentName, int departmentId);
    #endregion
}
