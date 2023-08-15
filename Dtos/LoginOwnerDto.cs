using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos;

public class LoginOwnerDto
{
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}