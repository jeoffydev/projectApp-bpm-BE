namespace asp_bpm_core7_BE.Models;

public class AuthOwnerResponse
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string TokenKey { get; set; } = string.Empty;
    public string? RoleName { get; set; } = string.Empty;
    public int? RoleId { get; set; }
}