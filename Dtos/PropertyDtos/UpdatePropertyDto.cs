using System.ComponentModel.DataAnnotations;

namespace asp_bpm_core7_BE.Dtos.PropertyDtos;

public class UpdatePropertyDto
{
    public int Id { get; set; }
    [Required]
    public required string PropertyName { get; set; }
    public string PropertyDetails { get; set; } = String.Empty;

    [Required]
    public required string Address { get; set; }
    public string PropertyColour { get; set; } = String.Empty;
}