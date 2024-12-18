using mind.Core.Models.DbModels;
using mind.Core.Models.DTOs;

namespace mind.Core.Interfaces.IServices;

public interface IEmployeeService
{
    Task<Employee> CreateEmployee(EmployeeDto employee);
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee?> GetEmployeeById(int id);
    Task<Employee> UpdateEmployee(UpdateEmployeeDto employee);
    Task<bool> DeleteEmployee(int id);
}
