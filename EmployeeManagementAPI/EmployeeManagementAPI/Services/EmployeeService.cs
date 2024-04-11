using System;

using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;
using EmployeeManagementAPI.Contracts;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _repository.GetEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _repository.GetEmployeeByIdAsync(id);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            return await _repository.AddEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee,
            EmployeeRequest employeeRequest)
        {
            await _repository.UpdateEmployeeAsync(employee, employeeRequest);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repository.DeleteEmployeeAsync(id);
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(string name,
            string email, string department)
        {
            return await _repository.SearchEmployeesAsync(name, email, department);
        }
    }
}

