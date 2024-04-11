using System;
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
            Employee ?nullEmployee = null;
            _mockService.Setup(serv => serv.GetEmployeeByIdAsync(invalidId)).ReturnsAsync(nullEmployee);

            // Act
            var result = await _controller.GetEmployee(invalidId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }
    }
}

