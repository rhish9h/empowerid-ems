using System;
using EmployeeManagementAPI.Contracts;
using EmployeeManagementAPI.Controllers;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagementAPITests.Controllers
{
    [TestFixture]
    public class EmployeeContollerTests
    {
        private EmployeeController _controller;
        private Mock<IEmployeeService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_mockService.Object);
        }

        [Test]
        public async Task GetEmployees_ReturnsOkObjectResult()
        {
            // Arrange
            var expectedEmployees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "IT" },
                new Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Department = "HR" }
            };

            _mockService.Setup(serv => serv.GetEmployeesAsync()).ReturnsAsync(expectedEmployees);

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result.Result;

            Assert.That(okResult.Value, Is.InstanceOf<IEnumerable<Employee>>());
            var employees = (IEnumerable<Employee>)okResult.Value;

            Assert.That(employees, Is.EqualTo(expectedEmployees));
        }

        [Test]
        public async Task GetEmployee_WithValidId_ReturnsOkObjectResult()
        {
            // Arrange
            int validId = 1;
            var expectedEmployee = new Employee
            {
                Id = validId,
                Name = "John Doe",
                Email = "john@example.com",
                Department = "IT"
            };

            _mockService.Setup(serv => serv.GetEmployeeByIdAsync(validId)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _controller.GetEmployee(validId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result.Result;

            Assert.That(okResult.Value, Is.InstanceOf<Employee>());
            var employee = (Employee)okResult.Value;

            Assert.That(employee.Id, Is.EqualTo(validId));
            Assert.That(employee.Name, Is.EqualTo(expectedEmployee.Name));
            Assert.That(employee.Email, Is.EqualTo(expectedEmployee.Email));
            Assert.That(employee.Department, Is.EqualTo(expectedEmployee.Department));
        }

        [Test]
        public async Task GetEmployee_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int invalidId = 100; // Assuming an ID that doesn't exist
            Employee? nullEmployee = null;
            _mockService.Setup(serv => serv.GetEmployeeByIdAsync(invalidId)).ReturnsAsync(nullEmployee);

            // Act
            var result = await _controller.GetEmployee(invalidId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task SearchEmployees_WithValidSearchParams_ReturnsOkObjectResult()
        {
            // Arrange
            string name = "John";
            string email = "john@example.com";
            string department = "IT";

            var expectedEmployees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "IT" },
                new Employee { Id = 4, Name = "John Smith", Email = "john@example.com", Department = "IT" }
            };

            _mockService.Setup(serv => serv.SearchEmployeesAsync(name, email, department)).ReturnsAsync(expectedEmployees);

            // Act
            var result = await _controller.SearchEmployees(name, email, department);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result.Result;

            Assert.That(okResult.Value, Is.InstanceOf<List<Employee>>());
            var employees = (List<Employee>)okResult.Value;

            Assert.That(employees.Count, Is.EqualTo(expectedEmployees.Count));
        }

        [Test]
        public async Task PostEmployee_WithValidEmployeeRequest_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest("John Doe", "john@example.com", new DateTime(1990, 1, 1), "IT");

            var employee = new Employee
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Department = "IT"
            };

            _mockService.Setup(serv => serv.AddEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync(employee);

            // Act
            var result = await _controller.PostEmployee(employeeRequest);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdAtActionResult = (CreatedAtActionResult)result.Result;

            Assert.That(createdAtActionResult.Value, Is.EqualTo(employee));

            Assert.That(createdAtActionResult.ActionName, Is.EqualTo(nameof(EmployeeController.GetEmployee)));
        }

        [Test]
        public async Task PutEmployee_WithValidIdAndEmployeeRequest_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            var employeeRequest = new EmployeeRequest("Updated John Doe", "updated_john@example.com", new DateTime(1990, 1, 1), "Updated IT");

            var existingEmployee = new Employee
            {
                Id = id,
                Name = "John Doe",
                Email = "john@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Department = "IT"
            };

            _mockService.Setup(serv => serv.GetEmployeeByIdAsync(id)).ReturnsAsync(existingEmployee);

            // Act
            var result = await _controller.PutEmployee(id, employeeRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockService.Verify(serv => serv.UpdateEmployeeAsync(existingEmployee, employeeRequest), Times.Once);
        }

        [Test]
        public async Task DeleteEmployee_WithValidId_ReturnsNoContent()
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

            _mockService.Setup(serv => serv.GetEmployeeByIdAsync(idToDelete)).ReturnsAsync(existingEmployee);

            // Act
            var result = await _controller.DeleteEmployee(idToDelete);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockService.Verify(serv => serv.DeleteEmployeeAsync(idToDelete), Times.Once);
        }
    }
}

