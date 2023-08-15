namespace asp_bpm_core7_BE.Dtos;

public class GetOwnerDto
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public bool Active { get; set; }
    public int AuthRoleId { get; set; }

    public string? RoleName { get; set; }

}