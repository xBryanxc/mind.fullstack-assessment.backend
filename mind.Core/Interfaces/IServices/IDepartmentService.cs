using mind.Core.Models.DbModels;

namespace mind.Core.Interfaces.IServices;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllDepartments();
}
