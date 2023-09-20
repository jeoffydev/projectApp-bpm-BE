using System.Security.Claims;
using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Dtos;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Services.AdministratorService;
using asp_bpm_core7_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_bpm_core7_BE.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AdministratorApiController : ControllerBase
{
    private readonly IAdministratorService _administrator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AdministratorApiController(IAdministratorService administratorService, IHttpContextAccessor httpContextAccessor)
    {
        _administrator = administratorService;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpGet("Administrators/{roleId?}")]
    public async Task<ActionResult<ServiceResponse<List<GetAdministratorDto>>>> GetAdministrators(int? roleId)
    {
        return Ok(await _administrator.GetAllAdministrators(roleId));
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpGet("Administrator/{id}")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> GetAdministratorById(int id)
    {
        return Ok(await _administrator.GetAdministrator(id));
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpGet("AdministratorUser")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> GetAdministrator()
    {
        var response = new ServiceResponse<GetAdministratorDto>();
        if (User.IsInRole(Helpers.AdminRole))
        {
            var getUser = UserClaims.UserClaimOrganization(_httpContextAccessor);
            return Ok(await _administrator.GetAdministrator(getUser.UserId));
        }
        return response;

    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpPost("RegisterAdministrator")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> RegisterAdministrator(RegisterAdminstratorDto registerAdmin)
    {
        var response = new ServiceResponse<GetAdministratorDto>();

        if (await _administrator.AdministratorExists(registerAdmin.Email))
        {
            response.Success = false;
            response.Message = "Administrator already exists.";
            return BadRequest(response);
        }

        var result = await _administrator.RegisterAdministrator(
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
        response.Data = result;

        return Ok(response);
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpPut("UpdateAdministrator")]
    public async Task<ActionResult<ServiceResponse<GetAdministratorDto>>> UpdateAdministrator(UpdateAdministratorDto updateAdmin)
    {
        var response = await _administrator.UpdateAdministrator(updateAdmin, updateAdmin.Password);
        return Ok(response);
    }


    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpDelete("DeleteAdministrator/{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteAdministrator(int id)
    {
        var response = await _administrator.DeleteAdministrator(id);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("LoginAdministrator")]
    public async Task<ActionResult<ServiceResponse<AuthAdminResponse>>> LoginAdministrator(LoginAdministratorDto loginUserDto)
    {
        var response = await _administrator.LoginAdministrator(
            loginUserDto.Email,
            loginUserDto.Password
        );
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("CheckLoginEmail")]
    public async Task<ActionResult<ServiceResponse<string>>> CheckLoginEmail(EmailVerificationDto emailDto)
    {
        var response = new ServiceResponse<string>();
        var verify = await _administrator.AdministratorExists(emailDto.Email);
        if (!verify)
        {
            response.Success = false;
            return BadRequest(response);
        }
        return Ok(await _administrator.GenerateAdministratorLoginVerification(emailDto.Email));
    }

    [AllowAnonymous]
    [HttpGet("FinalizedLogin/{secret}")]
    public async Task<ActionResult<ServiceResponse<VerifySecretKeyDto>>> FinaAdministratorLogin(string secret)
    {
        return Ok(await _administrator.AdministratorFromEmailLoginVerification(secret));
    }

    [Authorize(Roles = Helpers.OwnerRole)]
    [HttpGet("AdministratorsByOrgId/{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetAdministratorDto>>>> GetAdministratorsByOrgId(int id)
    {
        return Ok(await _administrator.GetAllAdministratorsByOrgId(id));
    }

    [Authorize(Roles = Helpers.AdminRole)]
    [HttpPut("UpdateUserDetailsByClaims")]
    public async Task<ActionResult<ServiceResponse<List<GetAdministratorDto>>>> UpdateUserDetailsByClaims(UpdateDetailsUserDto userDto)
    {
        var response = new ServiceResponse<bool>();
        var getUser = UserClaims.UserClaimOrganization(_httpContextAccessor);
        if (getUser.UserId == 0)
        {
            response.Success = false;
            return BadRequest(response);
        }

        UpdateDetailsUserDto updateUser = new UpdateDetailsUserDto()
        {
            FullName = userDto.FullName
        };
        var update = await _administrator.UpdateAdministratorByClaims(updateUser, getUser.UserId);
        return Ok(update);
    }
}