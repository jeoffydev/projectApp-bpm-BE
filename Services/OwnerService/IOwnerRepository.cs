using asp_bpm_core7_BE.Dtos;
using asp_bpm_core7_BE.Models;

namespace asp_bpm_core7_BE.Services.OwnerService;

public interface IOwnerRepository
{
    Task<ServiceResponse<List<GetOwnerDto>>> GetAllOwners(int? roleId);
    Task<ServiceResponse<GetOwnerDto>> GetOwner(int id);
    Task<bool> OwnerExists(string email);
    Task<bool> OwnerExistsById(int id);
    Task<GetOwnerDto> RegisterOwner(Owner owner, string password);

    Task<ServiceResponse<int>> DeleteOwner(int id);

    Task<ServiceResponse<GetOwnerDto>> UpdateOwner(UpdateOwnerDto owner, string password);

    Task<ServiceResponse<AuthOwnerResponse>> LoginOwner(string email, string password);

    Task<ServiceResponse<List<GetRoleDto>>> GetRoles();

    Task<ServiceResponse<string>> GenerateLoginVerification(string email);
    Task<ServiceResponse<VerifySecretKeyDto>> FromEmailLoginVerification(string secretKey);


}