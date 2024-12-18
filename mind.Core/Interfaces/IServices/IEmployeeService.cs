using mind.Core.Models.DbModels;

namespace mind.Core.Interfaces.IServices;

public interface IEmployeeService
{
    Task<Employee> CreateEmployee(Employee employee);
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee?> GetEmployeeById(int id);
    Task<Employee> UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(int id);
}
