using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mind.Core.Extensions;

namespace mind.Core.Models.DbModels;

[Table("employees")]
public class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Column("last_name")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Column("hire_date")]
    public DateTime HireDate { get; set; }

    [Required]
    [Column("department_id")]
    public int DepartmentId { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Column("address")]
    [StringLength(200)]
    public string? Address { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual Department? Department { get; set; }

    [NotMapped]
    public string TimeWorked
    {
        get
        {
           return HireDate.GetTimeWorked();
        }
    }
}

