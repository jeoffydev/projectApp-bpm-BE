using System.Security.Claims;
using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;
using asp_bpm_core7_BE.Dtos.OrganizationDtos;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Services.AdministratorService;
using asp_bpm_core7_BE.Services.OrganizationService;
using asp_bpm_core7_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_bpm_core7_BE.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OrganizationApiController : ControllerBase
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IAdministratorService _administrator;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrganizationApiController(IOrganizationRepository organizationRepository, IAdministratorService administratorService, IHttpContextAccessor httpContextAccessor)
    {
        _organizationRepository = organizationRepository;
        _administrator = administratorService;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpGet("GetAllOrganizations")]
    public async Task<ActionResult<ServiceResponse<List<GetOrgDto>>>> GetAllOrganizations()
    {
        return Ok(await _organizationRepository.GetAllOrganizations());
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpGet("Organization/{id}")]
    public async Task<ActionResult<ServiceResponse<GetOrgDto>>> GetOrgById(int id)
    {
        return Ok(await _organizationRepository.GetOrganization(id));
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpPost("RegisterOrganization")]
    public async Task<ActionResult<ServiceResponse<GetOrgDto>>> RegisterOrganization(RegisterOrgDto regOrg)
    {
        var response = new ServiceResponse<GetOrgDto>();

        var result = await _organizationRepository.RegisterOrganization(
            new Organization
            {
                CompanyName = regOrg.CompanyName,
                BusinessDetails = regOrg.BusinessDetails,
                Address = regOrg.Address,
                Active = regOrg.Active,
                ContactEmail = regOrg.ContactEmail,
                ContactPerson = regOrg.ContactPerson,
                MobileNumber = regOrg.MobileNumber,
                PhoneNumber = regOrg.PhoneNumber,
                Website = regOrg.Website,
                Administrators = new() { }
            }
        );

        response.Data = result;


        return Ok(response);
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpPut("UpdateOrganization")]
    public async Task<ActionResult<ServiceResponse<GetOrgDto>>> UpdateOrganization(UpdateOrgDto updateOrgDto)
    {
        var response = await _organizationRepository.UpdateOrganization(updateOrgDto);
        return Ok(response);
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpDelete("DeleteOrganization/{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteOrganization(int id)
    {
        var response = await _organizationRepository.DeleteOrganization(id);
        return Ok(response);
    }

    [Authorize(Roles = Helpers.AdminRole)]
    [HttpGet("GetUserOrganization")]
    public async Task<ActionResult<ServiceResponse<GetOrgUserDto>>> GetUserOrganization()
    {
        var response = new ServiceResponse<GetOrgUserDto>();
        var getUserId = UserClaims.UserClaimOrganization(_httpContextAccessor);
        if (getUserId.UserId is 0)
        {
            response.Success = false;
            return BadRequest(response);
        }


        OrganizationUserResponse<GetAdministratorDto> getUser = await _administrator.GetUserClaimDetails(getUserId.UserId);
        if (getUser.Success && getUser?.Data?.OrganizationId is not null)
        {
            var orgDetails = await _organizationRepository.GetOrganization(getUser.Data.OrganizationId);
            var createUserOrgDetails = new GetOrgUserDto()
            {
                OrganizationDetails = orgDetails,
                UserDetails = getUser
            };
            response.Data = createUserOrgDetails;
            return Ok(response);
        }
        else
        {
            response.Success = false;
            return BadRequest(response);
        }

    }
}