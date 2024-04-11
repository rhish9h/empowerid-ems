using EmployeeManagementAPI.Contracts;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _service.GetEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _service.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // GET: api/employees/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(string name = null,
            string email = null, string department = null)
        {
            var employees = await _service.SearchEmployeesAsync(name, email, department);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeRequest employeeRequest)
        {
            // Map the EmployeeRequest to the Employee model
            var employee = new Employee
            {
                Name = employeeRequest.Name,
                Email = employeeRequest.Email,
                DateOfBirth = employeeRequest.DateOfBirth,
                Department = employeeRequest.Department
            };
            var newEmployee = await _service.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeRequest employeeRequest)
        {
            // Retrieve the employee from the database
            var existingEmployee = await _service.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            await _service.UpdateEmployeeAsync(existingEmployee, employeeRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _service.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
