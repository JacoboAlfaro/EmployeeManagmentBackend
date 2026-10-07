using CleanArchitecture.Application.Interfaces.IServices;
using CleanArchitecture.Application.Models;
using CleanArchitecture.WebApi.Models;
using EmployeeApi.DTOs;
using EmployeeApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> getAll([FromQuery] PaginationParams paginationParams)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var employees = await _employeeService.GetAllAsync(baseUrl, paginationParams);
            return Ok(new ApiResponse<PagedList<GetEmployeeResponse>>(true, 200, "Lista de empleados", employees));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var employee = await _employeeService.GetByIdAsync(id, baseUrl);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<GetEmployeeResponse>(true, 200, "Empleado encontrado", employee));
        }

        [HttpPost]
        public async Task<IActionResult> create([FromForm] CreateEmployeeRequest request, IFormFile? image)
        {
            FileUpload? fileUpload = null;

            if (image != null && image.Length > 0)
            {
                fileUpload = new FileUpload(
                    image.OpenReadStream(),
                    image.FileName,
                    image.ContentType
                );
            }

            await _employeeService.AddAsync(request, fileUpload);
            return Ok(new ApiResponse<object>(true, 200, "Employee created successfully", null));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromForm] UpdateEmployeeRequest request, IFormFile? image)
        {
            FileUpload? fileUpload = null;

            if (image != null && image.Length > 0)
            {
                fileUpload = new FileUpload(
                    image.OpenReadStream(),
                    image.FileName,
                    image.ContentType
                );
            }

            await _employeeService.UpdateAsync(id, request, fileUpload);
            return Ok(new ApiResponse<object>(true, 200, "Employee updated successfully", null));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return Ok(new ApiResponse<object>(true, 200, "Employee deleted successfully", null));
        }
    }
}
