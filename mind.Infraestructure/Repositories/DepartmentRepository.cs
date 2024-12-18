using System;
using mind.Core.Interfaces.IRepositories;
using mind.Core.Models.DbModels;

namespace mind.Infraestructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    public Task<IEnumerable<Department>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
