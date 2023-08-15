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

    [HttpGet("Owners")]
    public async Task<ActionResult<ServiceResponse<GetOwnerDto>>> GetOwners()
    {
        return Ok(await _ownerRepository.GetAllOwners());
    }

    [HttpGet("Owner/{id}")]
    public async Task<ActionResult<ServiceResponse<GetOwnerDto>>> GetOwnersById(int id)
    {
        return Ok(await _ownerRepository.GetOwner(id));
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
}