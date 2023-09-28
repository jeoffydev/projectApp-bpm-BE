using Microsoft.AspNetCore.Mvc;
using asp_bpm_core7_BE.Services.PropertyService;
using Microsoft.AspNetCore.Authorization;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Dtos.PropertyDtos;
using asp_bpm_core7_BE.Utils;
using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Services.AdministratorService;

namespace asp_bpm_core7_BE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyApiController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IAdministratorService _administratorService;
    public PropertyApiController(
        IPropertyService propertyService,
        IHttpContextAccessor httpContextAccessor,
        IAdministratorService administratorService
    )
    {
        _propertyService = propertyService;
        _httpContextAccessor = httpContextAccessor;
        _administratorService = administratorService;
    }

    [Authorize(Roles = Helpers.AdminRole)]
    [HttpGet("GetAllPropertiesByOrgIdClaims")]
    public async Task<ActionResult<ServiceResponse<List<GetPropertyDto>>>> GetAllPropertiesByOrgIdClaims()
    {
        var response = new ServiceResponse<List<GetPropertyDto>>();
        var getUser = UserClaims.UserClaimOrganization(_httpContextAccessor);
        if (getUser.UserId == 0)
        {
            response.Success = false;
            return BadRequest(response);
        }

        var getOrgAdmin = await _administratorService.GetUserClaimDetails(getUser.UserId);
        if (getOrgAdmin.Success)
        {
            return Ok(await _propertyService.GetAllPropertiesByOrgId((int)getOrgAdmin?.Data?.OrganizationId!));
        }
        else
        {
            response.Success = false;
            return BadRequest(response);
        }

    }

}