using asp_bpm_core7_BE.Dtos;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;
using asp_bpm_core7_BE.Models;

namespace asp_bpm_core7_BE.Services.AdministratorService;

public interface IAdministratorService
{
    Task<ServiceResponse<List<GetAdministratorDto>>> GetAllAdministrators(int? roleId);
    Task<ServiceResponse<GetAdministratorDto>> GetAdministrator(int id);
    Task<bool> AdministratorExists(string email);
    Task<bool> AdministratorExistsTrue(string email);
    Task<bool> AdministratorExistsById(int id);
    Task<GetAdministratorDto> RegisterAdministrator(Administrator admin, string password);

    Task<ServiceResponse<int>> DeleteAdministrator(int id);

    Task<ServiceResponse<GetAdministratorDto>> UpdateAdministrator(UpdateAdministratorDto admin, string password);

    Task<ServiceResponse<AuthAdminResponse>> LoginAdministrator(string email, string password);

    Task<ServiceResponse<string>> GenerateAdministratorLoginVerification(string email);
    Task<ServiceResponse<VerifySecretKeyDto>> AdministratorFromEmailLoginVerification(string secretKey);

    Task<ServiceResponse<List<GetAdministratorDto>>> GetAllAdministratorsByOrgId(int orgId);

    Task<OrganizationUserResponse<GetAdministratorDto>> GetUserClaimDetails(int id);

    Task<ServiceResponse<bool>> UpdateAdministratorByClaims(UpdateDetailsUserDto userDto, int id);
    Task<ServiceResponse<bool>> UpdateAdministratorPasswordByClaims(string password, int id);

    Task<ServiceResponse<ForgotPasswordVerifyDto>> AdministratorSecretKeyVerification(string secretKey);
    Task<ServiceResponse<bool>> RemoveSecretKey(string secretKey, int id);

}