using mind.Core.Models.DbModels;

namespace mind.Core.Interfaces.IRepositories;

public interface IDepartmentRepository
{
     Task<IEnumerable<Department>> GetAllAsync();
}
