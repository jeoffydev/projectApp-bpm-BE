using asp_bpm_core7_BE.Dtos;
using asp_bpm_core7_BE.Models;

namespace asp_bpm_core7_BE.Services.OwnerService;

public interface IOwnerRepository
{
    Task<ServiceResponse<List<GetOwnerDto>>> GetAllOwners();
    Task<ServiceResponse<GetOwnerDto>> GetOwner(int id);
    Task<bool> OwnerExists(string email);
    Task<bool> OwnerExistsById(int id);
    Task<ServiceResponse<GetOwnerDto>> RegisterOwner(Owner owner, string password);

    Task<ServiceResponse<int>> DeleteOwner(int id);

    Task<ServiceResponse<GetOwnerDto>> UpdateOwner(UpdateOwnerDto owner, string password);

    Task<ServiceResponse<AuthOwnerResponse>> LoginOwner(string email, string password);

}