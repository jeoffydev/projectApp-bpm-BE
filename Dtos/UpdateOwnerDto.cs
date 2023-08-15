using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos;

public class UpdateOwnerDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string FullName { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
    [Required(ErrorMessage = "Role is required")]
    public int RoleId { get; set; }
    [Required]

    public required bool Active { get; set; }
}