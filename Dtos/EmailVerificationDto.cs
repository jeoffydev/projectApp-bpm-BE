using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos;

public class EmailVerificationDto
{
    [Required]
    public required string Email { get; set; }
}