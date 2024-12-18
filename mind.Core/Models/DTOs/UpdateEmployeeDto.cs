using System.ComponentModel.DataAnnotations;

namespace mind.Core.Models.DTOs;

public class UpdateEmployeeDto
{
    [Required]
    public int Id { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    public DateTime HireDate { get; set; } = DateTime.MinValue;

    public int DepartmentId { get; set; } = 0;

    [StringLength(15)]
    public string? Phone { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }
} 