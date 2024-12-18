using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using mind.Core.Models.DbModels;

namespace mind.Infraestructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
	}

    #region DbSet Section
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    #endregion
}

