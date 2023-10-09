using asp_bpm_core7_BE.Dtos.PropertyDtos;
using asp_bpm_core7_BE.Models;

namespace asp_bpm_core7_BE.Services.PropertyService;

public interface IPropertyService
{
    Task<ServiceResponse<List<GetPropertyDto>>> GetAllPropertiesByOrgId(int orgId);
    Task<ServiceResponse<GetPropertyDto>> GetProperty(int propertyId, int orgId);
    Task<bool> PropertyExistsById(int propertyId);
    Task<bool> PropertyExistsByOrgId(int propertyId, int orgId);

    Task<GetPropertyDto> RegisterProperty(Property property);
    Task<ServiceResponse<int>> DeleteProperty(int propertyId, int orgId);

    Task<ServiceResponse<GetPropertyDto>> UpdateProperty(UpdatePropertyDto updatePropertyDto, int orgId);
}