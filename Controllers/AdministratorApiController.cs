using System.Security.Claims;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Services.AdministratorService;
using asp_bpm_core7_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace asp_bpm_core7_BE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministratorApiController : ControllerBase
{
    private readonly IAdministratorService _administrator;
    public AdministratorApiController(IAdministratorService administratorService)
    {
        _administrator = administratorService;
    }

    [HttpGet("Administrators/{roleId?}")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> GetAdministrators(int? roleId)
    {
        return Ok(await _administrator.GetAllAdministrators(roleId));
    }

    [HttpGet("Administrator/{id}")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> GetAdministratorById(int id)
    {
        return Ok(await _administrator.GetAdministrator(id));
    }

    [HttpGet("AdministratorUser")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> GetAdministrator()
    {
        var response = new ServiceResponse<GetAdministratorDto>();
        if (User.IsInRole(Helpers.AdminRole))
        {
            var getUserId = User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)!.Value;
            int userId = Int32.Parse(getUserId);
            return Ok(await _administrator.GetAdministrator(userId));
        }
        return response;

    }

    [HttpPost("RegisterAdministrator")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> RegisterAdministrator(RegisterAdminstratorDto registerAdmin)
    {

        var response = await _administrator.RegisterAdministrator(
            new Administrator
            {
                FullName = registerAdmin.FullName,
                Email = registerAdmin.Email,
                Active = registerAdmin.Active,
                AuthRoleId = registerAdmin.RoleId,
                Mobile = registerAdmin.Mobile,
                Phone = registerAdmin.Phone,
                OrganizationId = registerAdmin.OrganizationId
            },
            registerAdmin.Password
        );

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut("UpdateAdministrator")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> UpdateAdministrator(UpdateAdministratorDto updateAdmin)
    {
        var response = await _administrator.UpdateAdministrator(updateAdmin, updateAdmin.Password);
        return Ok(response);
    }

    [HttpDelete("DeleteAdministrator/{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteAdministrator(int id)
    {
        var response = await _administrator.DeleteAdministrator(id);
        return Ok(response);
    }
}