using System;
namespace EmployeeManagementAPI.Contracts
{
    public record EmployeeRequest(
        string Name,
        string Email,
        DateTime DateOfBirth,
        string Department
    );
}
