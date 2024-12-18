using System.ComponentModel.DataAnnotations;

namespace mind.Core.Models.DTOs;

public class EmployeeDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime HireDate { get; set; }

    [Required]
    [Range(1, 1, ErrorMessage = "Department ID must be greater than 0")]
    public int DepartmentId { get; set; }

    [StringLength(15)]
    public string? Phone { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }
} 