namespace asp_bpm_core7_BE.Dtos.AdministratorDtos;

public class GetAdministratorDto
{
    public int Id { get; set; }

    public required string FullName { get; set; }

    public required string Email { get; set; }

    public string Mobile { get; set; } = String.Empty;
    public string Phone { get; set; } = String.Empty;

    public bool? Active { get; set; } = false;

    public string? SecretKey { get; set; } = String.Empty;

    public int AuthRoleId { get; set; }

    public string RoleName { get; set; } = String.Empty;

    public int OrganizationId { get; set; }

}