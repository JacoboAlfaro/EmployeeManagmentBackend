using CleanArchitecture.Application.Interfaces.IServices;
using CleanArchitecture.Application.Models;
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
        public async Task<IActionResult> getAll([FromQuery] PaginationParams paginationParams)
        {
            var departments = await _departmentService.GetAllDepartmentsAsync(paginationParams);
            return Ok(new ApiResponse<PagedList<GetDepartmentResponse>>(true, 200, "Lista de departamentos", departments));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<GetDepartmentResponse>(true, 200, "Departamento encontrado", department));
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] CreateDepartmentRequest request)
        {
            await _departmentService.CreateDepartment(request);
            return Ok(new ApiResponse<object>(true, 200, "Departamento creado exitosamente", null));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] UpdateDepartmentRequest request)
        {
            await _departmentService.UpdateDepartment(id, request);
            return Ok(new ApiResponse<object>(true, 200, "Departamento actualizado exitosamente", null));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            await _departmentService.DeleteDepartment(id);
            return Ok(new ApiResponse<object>(true, 200, "Departamento eliminado exitosamente", null));
        }
    }
}
