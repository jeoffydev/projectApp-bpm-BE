using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos.AdministratorDtos;

public class ForgotPasswordDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public required string Password { get; set; }

    [Required]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }

    [Required]
    public required string SecretKey { get; set; }
}