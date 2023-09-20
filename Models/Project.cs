using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_bpm_core7_BE.Models;

public class Project
{
    public int Id { get; set; }
    [Required]
    public required string ProjectName { get; set; }
    public string ProjectDetails { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;
    public string ProjectColour { get; set; } = String.Empty;

    // [Required]
    // public int OrganizationId { get; set; }

    // [ForeignKey("OrganizationId")]
    // public virtual Organization? Organization { get; set; }
}