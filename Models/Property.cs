using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_bpm_core7_BE.Models;

public class Property
{
    public int Id { get; set; }
    [Required]
    public required string PropertyName { get; set; }
    public string PropertyDetails { get; set; } = String.Empty;

    [Required]
    public required string Address { get; set; }
    public string PropertyColour { get; set; } = String.Empty;

    [Required]
    public int OrganizationId { get; set; }

    [ForeignKey("OrganizationId")]
    public virtual Organization? Organization { get; set; }
}