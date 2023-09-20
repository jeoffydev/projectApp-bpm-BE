using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos.AdministratorDtos;

public class UpdatePasswordUserDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string Password { get; set; }

    [Required]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}