using System;
using Moq;
using EmployeeManagementAPI.Repositories;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Models;

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
    }
}

