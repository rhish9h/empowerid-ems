using System;
using EmployeeManagementAPI.Contracts;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPITests.Repositories
{
    public class EmployeeRepositoryTests
    {
        private EmployeeRepository _repository;
        private DbContextOptions<EmployeeContext> _options;

        [SetUp]
        public void Setup()
        {
            // Configure in-memory database options
            _options = new DbContextOptionsBuilder<EmployeeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Clear database
            using (var context = new EmployeeContext(_options))
            {
                // Clear the Employees table
                context.Employees.RemoveRange(context.Employees);
                context.SaveChanges();
            }

            // Seed the in-memory database with test data
            using (var context = new EmployeeContext(_options))
            {
                context.AddRange(
                    new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), Department = "IT" },
                    new Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", DateOfBirth = new DateTime(1995, 5, 5), Department = "HR" },
                    new Employee { Id = 3, Name = "Alice Johnson", Email = "alice@example.com", DateOfBirth = new DateTime(1985, 12, 10), Department = "Finance" }
                );
                context.SaveChanges();
            }

            // Create EmployeeRepository instance with in-memory database context
            var contextWithOptions = new EmployeeContext(_options);
            _repository = new EmployeeRepository(contextWithOptions);
        }


        [Test]
        public async Task GetEmployeesAsync_ReturnsAllEmployees()
        {
            // Arrange: Setup expected employees
            var expectedEmployees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), Department = "IT" },
                new Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", DateOfBirth = new DateTime(1995, 5, 5), Department = "HR" },
                new Employee { Id = 3, Name = "Alice Johnson", Email = "alice@example.com", DateOfBirth = new DateTime(1985, 12, 10), Department = "Finance" }
            };

            // Act: Call the method under test
            var result = await _repository.GetEmployeesAsync();

            // Assert: Compare the result with expected employees
            Assert.That(result, Is.Not.Null); // Ensure result is not null
            Assert.That(result.Count(), Is.EqualTo(expectedEmployees.Count)); // Ensure the correct number of employees is returned
        }

        [Test]
        public async Task GetEmployeeByIdAsync_ReturnsCorrectEmployee()
        {
            // Arrange
            int idToFind = 1;
            var expectedEmployee = new Employee { Id = idToFind, Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), Department = "IT" };

            // Act
            var result = await _repository.GetEmployeeByIdAsync(idToFind);

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
            // Arrange: Create a new employee to add
            var newEmployee = new Employee { Name = "New Employee", Email = "new@example.com", DateOfBirth = new DateTime(1990, 1, 1), Department = "IT" };

            // Act: Call the method under test
            await _repository.AddEmployeeAsync(newEmployee);

            // Assert: Verify that the employee was added to the database
            using (var context = new EmployeeContext(_options))
            {
                var result = await context.Employees.FindAsync(newEmployee.Id);
                Assert.That(result, Is.Not.Null); // Ensure the employee is not null
                Assert.That(result.Id, Is.EqualTo(newEmployee.Id)); // Ensure correct employee is returned
                Assert.That(result.Name, Is.EqualTo(newEmployee.Name));
                Assert.That(result.Email, Is.EqualTo(newEmployee.Email));
                Assert.That(result.DateOfBirth, Is.EqualTo(newEmployee.DateOfBirth));
                Assert.That(result.Department, Is.EqualTo(newEmployee.Department));
            }
        }

        [Test]
        public async Task UpdateEmployeeAsync_UpdatesExistingEmployee()
        {
            // Arrange: Create an existing employee and an updated employee request
            int existingEmployeeId = 1;
            var existingEmployee = new Employee { Id = existingEmployeeId, Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), Department = "IT" };
            var updatedEmployeeRequest = new EmployeeRequest("Updated John Doe", "updated.john@example.com", new DateTime(1990, 1, 1), "HR");

            // Act: Call the method under test
            await _repository.UpdateEmployeeAsync(existingEmployee, updatedEmployeeRequest);

            // Assert: Verify that the employee was updated in the database
            using (var context = new EmployeeContext(_options))
            {
                var result = await context.Employees.FindAsync(existingEmployeeId);
                Assert.That(result, Is.Not.Null); // Ensure the employee is not null
                Assert.That(result.Id, Is.EqualTo(existingEmployeeId)); // Ensure correct employee is returned
                Assert.That(result.Name, Is.EqualTo(updatedEmployeeRequest.Name)); // Check if the name is updated
                Assert.That(result.Email, Is.EqualTo(updatedEmployeeRequest.Email)); // Check if the email is updated
                Assert.That(result.DateOfBirth, Is.EqualTo(updatedEmployeeRequest.DateOfBirth)); // Check if the date of birth is updated
                Assert.That(result.Department, Is.EqualTo(updatedEmployeeRequest.Department)); // Check if the department is updated
            }
        }

    }
}

