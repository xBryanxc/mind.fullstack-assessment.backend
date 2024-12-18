using Microsoft.EntityFrameworkCore;
using mind.Core.Interfaces.IRepositories;
using mind.Core.Models.DbModels;
using mind.Infraestructure.Data;

namespace mind.Infraestructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
     protected readonly ApplicationDbContext _dbContext;

     public EmployeeRepository(ApplicationDbContext dbContext)
     {
        _dbContext = dbContext;
     }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> DeleteAsync(int id)
    {
       var employee = await _dbContext.Employees.FindAsync(id);
       if (employee == null)
       {
        return false;
       }
       _dbContext.Employees.Remove(employee);
       await _dbContext.SaveChangesAsync();
       return true;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
         return await _dbContext.Employees
                      .Include(e => e.Department)
                      .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
         return await _dbContext.Employees
                      .Include(e => e.Department)
                      .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        var employeeToUpdate = await _dbContext.Employees.FindAsync(employee.Id);
        if (employeeToUpdate == null)
        {
           throw new Exception("Employee not found");
        }
        employeeToUpdate.FirstName = employee.FirstName ?? employeeToUpdate.FirstName;
        employeeToUpdate.LastName = employee.LastName ?? employeeToUpdate.LastName;
        employeeToUpdate.HireDate = employee.HireDate != DateTime.MinValue ? employee.HireDate : employeeToUpdate.HireDate;
        employeeToUpdate.DepartmentId = employee.DepartmentId != 0 ? employee.DepartmentId : employeeToUpdate.DepartmentId;
        employeeToUpdate.Phone = employee.Phone ?? employeeToUpdate.Phone;
        employeeToUpdate.Address = employee.Address ?? employeeToUpdate.Address;

         await _dbContext.SaveChangesAsync();
        return employeeToUpdate;

    }
}
