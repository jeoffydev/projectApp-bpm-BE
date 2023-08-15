using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_bpm_core7_BE.Models;

public class Owner
{
    public int Id { get; set; }
    [Required]
    public required string FullName { get; set; }
    [Required]
    public required string Email { get; set; }

    public byte[] PasswordHash { get; set; } = new byte[0];
    public byte[] PasswordSalt { get; set; } = new byte[0];

    public bool? Active { get; set; } = false;

    public string? SecretKey { get; set; } = String.Empty;
    [Required]
    public int AuthRoleId { get; set; }

    [ForeignKey("AuthRoleId")]
    public virtual AuthRole? AuthRole { get; set; }
}