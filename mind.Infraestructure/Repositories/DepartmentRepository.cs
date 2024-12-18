using Microsoft.EntityFrameworkCore;
using mind.Core.Interfaces.IRepositories;
using mind.Core.Models.DbModels;
using mind.Infraestructure.Data;

namespace mind.Infraestructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
     protected readonly ApplicationDbContext _dbContext;

     public DepartmentRepository(ApplicationDbContext dbContext)
     {
         _dbContext = dbContext;
     }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
       return await _dbContext.Departments
            .AsNoTracking()
            .ToListAsync();
    }
}
