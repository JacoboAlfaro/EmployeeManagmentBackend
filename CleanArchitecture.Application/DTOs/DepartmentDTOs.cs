namespace EmployeeApi.DTOs
{
    #region responses
    public record GetDepartmentResponse(int id, string name);
    #endregion

    #region requests
    public record CreateDepartmentRequest(string name);
    public record UpdateDepartmentRequest(int id, string name);
    #endregion

}
