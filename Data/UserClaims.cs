using System.Security.Claims;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;
using asp_bpm_core7_BE.Dtos.PropertyDtos;
using asp_bpm_core7_BE.Services.AdministratorService;

namespace asp_bpm_core7_BE.Data;

public static class UserClaims
{
    public static UserClaimDto UserClaimOrganization(IHttpContextAccessor httpContextAccessor)
    {
        var getUserId = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)!.Value;
        if (getUserId is not null)
        {
            return new UserClaimDto()
            {
                UserId = Int32.Parse(getUserId)
            };
        }

        return new UserClaimDto()
        {
            UserId = 0
        };
    }
    public static async Task<ClaimsIdentityDto<GetAdministratorDto>> GetUserClaimDetails(IHttpContextAccessor httpContextAccessor, IAdministratorService _administratorService)
    {
        var response = new ClaimsIdentityDto<GetAdministratorDto>();
        var getUser = UserClaimOrganization(httpContextAccessor);
        if (getUser.UserId == 0)
        {
            response.Success = false;
            return response;
        }
        var getOrgAdmin = await _administratorService.GetUserClaimDetails(getUser.UserId);
        if (!getOrgAdmin.Success)
        {
            response.Success = false;
            return response;
        }
        else
        {
            response.Data = getOrgAdmin.Data;
            return response;
        }


    }
}




public class UserClaimDto
{
    public int UserId { get; set; }

}