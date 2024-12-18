using mind.Core.Interfaces.IServices;
using mind.Core.Models.DbModels;

namespace mind.Core.Services;

public class EmployeeService : IEmployeeService
{
    public Task<Employee> CreateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteEmployee(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetAllEmployees()
    {
        throw new NotImplementedException();
    }

    public Task<Employee?> GetEmployeeById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }
}
