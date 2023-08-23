using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_bpm_core7_BE.Models;

public class Organization
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

    [NotMapped]
    public required List<Administrator> Administrators { get; set; }

}