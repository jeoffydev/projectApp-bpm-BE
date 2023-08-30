using asp_bpm_core7_BE.Dtos.OrganizationDtos;
using asp_bpm_core7_BE.Models;

namespace asp_bpm_core7_BE.Services.OrganizationService;

public interface IOrganizationRepository
{
    Task<ServiceResponse<List<GetOrgDto>>> GetAllOrganizations();
    Task<ServiceResponse<GetOrgDto>> GetOrganization(int id);
    Task<bool> OrganizationExistsById(int id);
    Task<GetOrgDto> RegisterOrganization(Organization organization);

    Task<ServiceResponse<int>> DeleteOrganization(int id);

    Task<ServiceResponse<GetOrgDto>> UpdateOrganization(UpdateOrgDto updateOrg);
}