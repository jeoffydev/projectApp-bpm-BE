using System.ComponentModel.DataAnnotations;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;

namespace asp_bpm_core7_BE.Dtos.OrganizationDtos;

public class GetOrgDto
{
    public int Id { get; set; }
    [Required]
    public required string CompanyName { get; set; }
    public string? BusinessDetails { get; set; }
    [Required]
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

    public List<GetAdministratorDto>? GetAdministratorDtos { get; set; }
}