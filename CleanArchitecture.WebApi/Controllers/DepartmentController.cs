using CleanArchitecture.Application.Interfaces.IServices;
using CleanArchitecture.WebApi.Models;
using EmployeeApi.DTOs;
using EmployeeApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(new ApiResponse<List<GetDepartmentResponse>>(true, 200, "Lista de empleados", departments));
        }
    }
}
