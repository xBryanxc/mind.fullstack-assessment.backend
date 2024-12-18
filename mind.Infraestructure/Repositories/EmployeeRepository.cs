using System;
using mind.Core.Interfaces.IRepositories;
using mind.Core.Models.DbModels;

namespace mind.Infraestructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public Task<Employee> CreateAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Employee?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> UpdateAsync(Employee employee)
    {
        throw new NotImplementedException();
    }
}
