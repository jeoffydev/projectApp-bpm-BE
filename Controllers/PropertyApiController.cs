using Microsoft.AspNetCore.Mvc;
using asp_bpm_core7_BE.Services.PropertyService;
using Microsoft.AspNetCore.Authorization;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Dtos.PropertyDtos;
using asp_bpm_core7_BE.Utils;
using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Services.AdministratorService;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;

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
        var getUser = await UserClaims.GetUserClaimDetails(_httpContextAccessor, _administratorService);
        if (!getUser.Success)
        {
            response.Success = false;
            return BadRequest(response);
        }

        return Ok(await _propertyService.GetAllPropertiesByOrgId((int)getUser?.Data?.OrganizationId!));

    }


    [Authorize(Roles = Helpers.AdminRole)]
    [HttpPost("RegisterPropertyByClaims")]
    public async Task<ActionResult<ServiceResponse<GetPropertyDto>>> RegisterPropertyByClaims(RegisterPropertyDto registerPropertyDto)
    {
        var response = new ServiceResponse<GetPropertyDto>();
        var getUser = await UserClaims.GetUserClaimDetails(_httpContextAccessor, _administratorService);
        if (!getUser.Success)
        {
            response.Success = false;
            return BadRequest(response);
        }

        var orgId = (int)getUser?.Data?.OrganizationId!;

        var property = new Property
        {
            PropertyName = registerPropertyDto.PropertyName,
            PropertyDetails = registerPropertyDto.PropertyDetails,
            Address = registerPropertyDto.Address,
            PropertyColour = registerPropertyDto.PropertyColour,
            OrganizationId = orgId
        };

        var result = await _propertyService.RegisterProperty(property);
        response.Data = result;
        return Ok(response);

    }


    [Authorize(Roles = Helpers.AdminRole)]
    [HttpPut("UpdatePropertyByClaims")]
    public async Task<ActionResult<ServiceResponse<GetPropertyDto>>> UpdatePropertyByClaims(UpdatePropertyDto updatePropertyDto)
    {
        var response = new ServiceResponse<GetPropertyDto>();
        var getUser = await UserClaims.GetUserClaimDetails(_httpContextAccessor, _administratorService);
        if (!getUser.Success)
        {
            response.Success = false;
            return BadRequest(response);
        }

        var orgId = (int)getUser?.Data?.OrganizationId!;
        var result = await _propertyService.UpdateProperty(updatePropertyDto, orgId);
        if (!result.Success)
        {
            response.Success = false;
            return BadRequest(response);
        }

        response.Data = result.Data;
        return Ok(response);

    }

    [Authorize(Roles = Helpers.AdminRole)]
    [HttpDelete("DeletePropertyByClaims/{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeletePropertyByClaims(int id)
    {
        var response = new ServiceResponse<int>();
        var getUser = await UserClaims.GetUserClaimDetails(_httpContextAccessor, _administratorService);
        if (!getUser.Success)
        {
            response.Success = false;
            return BadRequest(response);
        }

        var orgId = (int)getUser?.Data?.OrganizationId!;
        var result = await _propertyService.DeleteProperty(id, orgId);
        if (!result.Success)
        {
            response.Success = false;
            return BadRequest(response);
        }

        response.Data = result.Data;

        return Ok(response);
    }




}