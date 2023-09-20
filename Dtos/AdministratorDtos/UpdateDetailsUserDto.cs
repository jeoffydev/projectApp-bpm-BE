using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos.AdministratorDtos;

public class UpdateDetailsUserDto
{
    [Required]
    public required string FullName { get; set; }
}