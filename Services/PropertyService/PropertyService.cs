using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Dtos.PropertyDtos;
using asp_bpm_core7_BE.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace asp_bpm_core7_BE.Services.PropertyService;

public class PropertyService : IPropertyService
{
    public readonly Datacontext _context;
    private readonly IMapper _mapper;

    public PropertyService(Datacontext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetPropertyDto>>> GetAllPropertiesByOrgId(int orgId)
    {
        var ServiceResponse = new ServiceResponse<List<GetPropertyDto>>();
        var dbProps = await _context.Properties.AsNoTracking().ToListAsync();

        var propDtos = new List<GetPropertyDto>();

        foreach (var dbProp in dbProps)
        {
            GetPropertyDto propAdded = new()
            {
                Id = dbProp.Id,
                PropertyName = dbProp.PropertyName,
                PropertyDetails = dbProp.PropertyDetails,
                Address = dbProp.Address,
                PropertyColour = dbProp.PropertyColour,
                OrganizationId = dbProp.OrganizationId,
            };
            propDtos.Add(propAdded);
        }
        ServiceResponse.Data = propDtos;
        return ServiceResponse;
    }

    public Task<ServiceResponse<GetPropertyDto>> GetProperty(int propertyId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PropertyExistsById(int propertyId)
    {
        throw new NotImplementedException();
    }

    public Task<GetPropertyDto> RegisterProperty(Property property)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<GetPropertyDto>> UpdateProperty(UpdatePropertyDto updatePropertyDto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<int>> DeleteProperty(int propertyId)
    {
        throw new NotImplementedException();
    }
}