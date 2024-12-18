using System;
using mind.Core.Interfaces.IRepositories;
using mind.Core.Interfaces.IServices;
using mind.Core.Models.DbModels;

namespace mind.Core.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<IEnumerable<Department>> GetAllDepartments()
    {
       var departments = await _departmentRepository.GetAllAsync();
         return departments;
    }
}
