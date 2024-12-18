using mind.Core.Models.DbModels;

namespace mind.Core.Interfaces.IRepositories;

public interface IEmployeeRepository
{
    Task<Employee> CreateAsync(Employee employee);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
}
