using System;
using mind.Core.Interfaces.IServices;
using mind.Core.Models.DbModels;

namespace mind.Core.Services;

public class DepartmentService : IDepartmentService
{
    public Task<IEnumerable<Department>> GetAllDepartments()
    {
        throw new NotImplementedException();
    }
}
