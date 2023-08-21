using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos.OrganizationDtos;

public class UpdateOrgDto
{
    public int Id { get; set; }
    [Required]
    [StringLength(200)]
    public required string CompanyName { get; set; }
    [StringLength(200)]
    public string? BusinessDetails { get; set; }
    [Required]
    [StringLength(200)]
    public required string Address { get; set; }

    public string? PhoneNumber { get; set; }
    [Required]
    public required string MobileNumber { get; set; }
    [Required]
    public required string ContactPerson { get; set; }
    [Required]
    public required string ContactEmail { get; set; }
    public string? Website { get; set; }
    [Required]
    public required bool Active { get; set; }
}