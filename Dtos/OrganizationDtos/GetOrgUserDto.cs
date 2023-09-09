using asp_bpm_core7_BE.Dtos.AdministratorDtos;
using asp_bpm_core7_BE.Models;

namespace asp_bpm_core7_BE.Dtos.OrganizationDtos;

public class GetOrgUserDto
{
    public ServiceResponse<GetOrgDto>? OrganizationDetails { get; set; }
    public OrganizationUserResponse<GetAdministratorDto>? UserDetails { get; set; }


}