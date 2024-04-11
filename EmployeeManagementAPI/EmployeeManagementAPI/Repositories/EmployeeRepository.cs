using System;

using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Contracts;

namespace EmployeeManagementAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateEmployeeAsync(Employee existingEmployee,
            EmployeeRequest employeeRequest)
        {
            // Update the employee properties with values from the request body
            existingEmployee.Name = employeeRequest.Name;
            existingEmployee.Email = employeeRequest.Email;
            existingEmployee.DateOfBirth = employeeRequest.DateOfBirth;
            existingEmployee.Department = employeeRequest.Department;
            _context.Entry(existingEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(string name,
            string email, string department)
        {
            IQueryable<Employee> query = _context.Employees;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(e => e.Name.Contains(name,
                    StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(e => e.Email.Contains(email,
                    StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(department))
            {
                query = query.Where(e => e.Department.Contains(department,
                    StringComparison.OrdinalIgnoreCase));
            }

            return await query.ToListAsync();
        }
    }
}

