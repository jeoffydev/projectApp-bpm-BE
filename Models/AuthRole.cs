using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Models;

public class AuthRole
{
    public int Id { get; set; }
    [Required]
    public required string RoleName { get; set; }
    public string? RoleDescription { get; set; }
}