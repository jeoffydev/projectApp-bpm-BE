using System.Security.Claims;
using asp_bpm_core7_BE.Dtos;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Services.OwnerService;
using asp_bpm_core7_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_bpm_core7_BE.Controllers;

[Authorize(Roles = Helpers.OwnerAdminRole)]
[ApiController]
[Route("api/[controller]")]
public class OwnerApiController : ControllerBase
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerApiController(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;

    }

    [HttpGet("Owners/{roleId?}")]
    public async Task<ActionResult<ServiceResponse<GetOwnerDto>>> GetOwners(int? roleId)
    {
        return Ok(await _ownerRepository.GetAllOwners(roleId));
    }

    [HttpGet("Owner/{id}")]
    public async Task<ActionResult<ServiceResponse<GetOwnerDto>>> GetOwnersById(int id)
    {
        return Ok(await _ownerRepository.GetOwner(id));
    }

    [HttpGet("OwnerUser")]
    public async Task<ActionResult<ServiceResponse<GetOwnerDto>>> GetOwner()
    {
        var response = new ServiceResponse<GetOwnerDto>();
        if (User.IsInRole(Helpers.OwnerRole))
        {
            var getUserId = User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)!.Value;
            int userId = Int32.Parse(getUserId);
            return Ok(await _ownerRepository.GetOwner(userId));
        }
        return response;

    }

    [HttpPost("RegisterOwner")]
    public async Task<ActionResult<ServiceResponse<GetOwnerDto>>> RegisterOwner(RegisterOwnerDto registerOwnerDto)
    {

        var response = await _ownerRepository.RegisterOwner(
            new Owner
            {
                FullName = registerOwnerDto.FullName,
                Email = registerOwnerDto.Email,
                Active = registerOwnerDto.Active,
                AuthRoleId = registerOwnerDto.RoleId
            },
            registerOwnerDto.Password
        );

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("DeleteOwner/{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteOwner(int id)
    {
        var response = await _ownerRepository.DeleteOwner(id);
        return Ok(response);
    }

    [HttpPut("UpdateOwner")]
    public async Task<ActionResult<ServiceResponse<GetOwnerDto>>> UpdateOwner(UpdateOwnerDto updateOwnerDto)
    {
        var response = await _ownerRepository.UpdateOwner(updateOwnerDto, updateOwnerDto.Password);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("LoginOwner")]
    public async Task<ActionResult<ServiceResponse<AuthOwnerResponse>>> Login(LoginOwnerDto loginUserDto)
    {
        var response = await _ownerRepository.LoginOwner(
            loginUserDto.Email,
            loginUserDto.Password
        );
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("Roles")]
    public async Task<ActionResult<ServiceResponse<List<GetRoleDto>>>> GetRoles()
    {
        return Ok(await _ownerRepository.GetRoles());
    }


    [AllowAnonymous]
    [HttpPost("CheckLoginEmail")]
    public async Task<ActionResult<ServiceResponse<string>>> CheckLoginEmail(EmailVerificationDto emailDto)
    {
        var response = new ServiceResponse<string>();
        var verify = await _ownerRepository.OwnerExists(emailDto.Email);
        if (!verify)
        {
            response.Success = false;
            return BadRequest(response);
        }
        return Ok(await _ownerRepository.GenerateLoginVerification(emailDto.Email));
    }

    [AllowAnonymous]
    [HttpGet("FinalizedLogin/{secret}")]
    public async Task<ActionResult<ServiceResponse<VerifySecretKeyDto>>> FinalizedLogin(string secret)
    {
        return Ok(await _ownerRepository.FromEmailLoginVerification(secret));
    }


}