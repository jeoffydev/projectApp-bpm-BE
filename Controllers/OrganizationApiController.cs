using asp_bpm_core7_BE.Dtos.OrganizationDtos;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Services.OrganizationService;
using asp_bpm_core7_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_bpm_core7_BE.Controllers;

[Authorize(Roles = Helpers.OwnerRole)]
[ApiController]
[Route("api/[controller]")]
public class OrganizationApiController : ControllerBase
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationApiController(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    [HttpGet("GetAllOrganizations")]
    public async Task<ActionResult<ServiceResponse<List<GetOrgDto>>>> GetAllOrganizations()
    {
        return Ok(await _organizationRepository.GetAllOrganizations());
    }


    [HttpGet("Organization/{id}")]
    public async Task<ActionResult<ServiceResponse<GetOrgDto>>> GetOrgById(int id)
    {
        return Ok(await _organizationRepository.GetOrganization(id));
    }

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

    [HttpPut("UpdateOrganization")]
    public async Task<ActionResult<ServiceResponse<GetOrgDto>>> UpdateOrganization(UpdateOrgDto updateOrgDto)
    {
        var response = await _organizationRepository.UpdateOrganization(updateOrgDto);
        return Ok(response);
    }

    [HttpDelete("DeleteOrganization/{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteOrganization(int id)
    {
        var response = await _organizationRepository.DeleteOrganization(id);
        return Ok(response);
    }
}