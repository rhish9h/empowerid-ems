using System;
using Moq;
using EmployeeManagementAPI.Repositories;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Contracts;

namespace EmployeeManagementAPITests.Services
{
    [TestFixture]
    public class EmployeeServiceTests
	{
        private Mock<IEmployeeRepository> _mockRepository;
        private EmployeeService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_mockRepository.Object);
        }

        [Test]
        public async Task GetEmployeesAsync_ReturnsEmployeesFromRepository()
        {
            // Arrange
            var expectedEmployees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), Department = "IT" },
                new Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", DateOfBirth = new DateTime(1995, 5, 5), Department = "HR" },
                new Employee { Id = 3, Name = "Alice Johnson", Email = "alice@example.com", DateOfBirth = new DateTime(1985, 12, 10), Department = "Finance" }
            };

            _mockRepository.Setup(repo => repo.GetEmployeesAsync()).ReturnsAsync(expectedEmployees);

            // Act
            var result = await _service.GetEmployeesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedEmployees));
        }

        [Test]
        public async Task GetEmployeeByIdAsync_ReturnsCorrectEmployee()
        {
            // Arrange
            int idToFind = 1;
            var expectedEmployee = new Employee
            {
                Id = idToFind,
                Name = "John Doe",
                Email = "john@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Department = "IT"
            };

            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(idToFind)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _service.GetEmployeeByIdAsync(idToFind);

            // Assert
            Assert.That(result, Is.Not.Null); // Ensure result is not null
            Assert.That(result.Id, Is.EqualTo(expectedEmployee.Id)); // Ensure correct employee is returned
            Assert.That(result.Name, Is.EqualTo(expectedEmployee.Name));
            Assert.That(result.Email, Is.EqualTo(expectedEmployee.Email));
            Assert.That(result.DateOfBirth, Is.EqualTo(expectedEmployee.DateOfBirth));
            Assert.That(result.Department, Is.EqualTo(expectedEmployee.Department));
        }

        [Test]
        public async Task AddEmployeeAsync_AddsNewEmployee()
        {
            // Arrange
            var newEmployee = new Employee
            {
                Name = "New Employee",
                Email = "new@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Department = "IT"
            };
            var expectedEmployee = new Employee
            {
                Id = 1,
                Name = "New Employee",
                Email = "new@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Department = "IT"
            };

            _mockRepository.Setup(repo => repo.AddEmployeeAsync(newEmployee)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _service.AddEmployeeAsync(newEmployee);

            // Assert
            Assert.That(result, Is.Not.Null); // Ensure result is not null
            Assert.That(result.Id, Is.Not.EqualTo(0)); // Ensure employee ID is assigned
            Assert.That(result.Name, Is.EqualTo(newEmployee.Name));
            Assert.That(result.Email, Is.EqualTo(newEmployee.Email));
            Assert.That(result.DateOfBirth, Is.EqualTo(newEmployee.DateOfBirth));
            Assert.That(result.Department, Is.EqualTo(newEmployee.Department));
        }

        [Test]
        public async Task UpdateEmployeeAsync_UpdatesExistingEmployee()
        {
            // Arrange
            var existingEmployee = new Employee
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Department = "IT"
            };

            var employeeRequest = new EmployeeRequest("Updated John Doe", "updated.john@example.com", new DateTime(1990, 1, 1), "HR");

            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(existingEmployee.Id))
                           .ReturnsAsync(existingEmployee);

            // Act
            await _service.UpdateEmployeeAsync(existingEmployee, employeeRequest);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateEmployeeAsync(existingEmployee, employeeRequest), Times.Once);
        }

        [Test]
        public async Task DeleteEmployeeAsync_DeletesExistingEmployee()
        {
            // Arrange
            int idToDelete = 1;
            var existingEmployee = new Employee
            {
                Id = idToDelete,
                Name = "John Doe",
                Email = "john@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Department = "IT"
            };

            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(idToDelete))
                           .ReturnsAsync(existingEmployee);

            // Act
            await _service.DeleteEmployeeAsync(idToDelete);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteEmployeeAsync(idToDelete), Times.Once);
        }

        [Test]
        public async Task SearchEmployeesAsync_ReturnsFilteredEmployees()
        {
            // Arrange
            string nameToSearch = "John";
            string emailToSearch = "example.com";
            string departmentToSearch = "IT";

            // Define the expected result
            var expectedEmployees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "IT" },
                new Employee { Id = 2, Name = "John Smith", Email = "john.smith@example.com", Department = "IT" }
            };

            // Setup the mock repository to return the expected result
            _mockRepository.Setup(repo => repo.SearchEmployeesAsync(nameToSearch, emailToSearch, departmentToSearch))
                           .ReturnsAsync(expectedEmployees);

            // Act
            var result = await _service.SearchEmployeesAsync(nameToSearch, emailToSearch, departmentToSearch);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedEmployees));
        }
    }
}

