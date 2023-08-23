using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos.AdministratorDtos;

public class LoginAdministratorDto
{
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}