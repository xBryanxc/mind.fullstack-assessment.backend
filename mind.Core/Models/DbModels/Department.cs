using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mind.Core.Models.DbModels;

[Table("departments")]
public class Department
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Employee>? Employees { get; set; }
}

