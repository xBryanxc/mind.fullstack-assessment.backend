using mind.Core.Interfaces.IRepositories;
using mind.Core.Interfaces.IServices;
using mind.Core.Models.DbModels;
using mind.Core.Models.DTOs;

namespace mind.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> CreateEmployee(EmployeeDto employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        var newEmployee = new Employee
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            HireDate = employee.HireDate,
            DepartmentId = employee.DepartmentId,
            Phone = employee.Phone,
            Address = employee.Address
        };

        return await _employeeRepository.CreateAsync(newEmployee);
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid employee ID", nameof(id));
        }

        return await _employeeRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await _employeeRepository.GetAllAsync();
    }

    public async Task<Employee?> GetEmployeeById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid employee ID", nameof(id));
        }

        return await _employeeRepository.GetByIdAsync(id);
    }

    public async Task<Employee> UpdateEmployee(UpdateEmployeeDto employee)
    {
       if (employee.Id <= 0)
       {
           throw new ArgumentException("Invalid employee ID", nameof(employee.Id));
       }

       var employeeToUpdate = new Employee
       {
           Id = employee.Id,
           FirstName = employee.FirstName,
           LastName = employee.LastName,
           HireDate = employee.HireDate,
           DepartmentId = employee.DepartmentId,
           Phone = employee.Phone,
           Address = employee.Address
       };
       
       return await _employeeRepository.UpdateAsync(employeeToUpdate);

    }
}
