using System.Security.Claims;

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
}

public class UserClaimDto
{
    public int UserId { get; set; }

}