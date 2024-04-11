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

            _mockService.Setup(repo => repo.GetEmployeesAsync()).ReturnsAsync(expectedEmployees);

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result.Result;

            Assert.That(okResult.Value, Is.InstanceOf<IEnumerable<Employee>>());
            var employees = (IEnumerable<Employee>)okResult.Value;

            Assert.That(employees, Is.EqualTo(expectedEmployees));
        }
    }
}

