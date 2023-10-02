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

    public async Task<GetPropertyDto> RegisterProperty(Property property)
    {
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        var resultDto = new GetPropertyDto
        {
            Id = property.Id,
            PropertyName = property.PropertyName,
            PropertyDetails = property.PropertyDetails,
            Address = property.Address,
            PropertyColour = property.PropertyColour,
            OrganizationId = property.OrganizationId
        };

        return resultDto;
    }

    public async Task<ServiceResponse<GetPropertyDto>> UpdateProperty(UpdatePropertyDto updatePropertyDto, int orgId)
    {
        var response = new ServiceResponse<GetPropertyDto>();
        try
        {
            var getById = await _context.Properties.FirstOrDefaultAsync(s => s.Id == updatePropertyDto.Id && s.OrganizationId == orgId) ?? throw new Exception($"Property Id `{updatePropertyDto.Id}` is not found");

            getById.PropertyName = updatePropertyDto.PropertyName;
            getById.Address = updatePropertyDto.Address;
            getById.PropertyDetails = updatePropertyDto.PropertyDetails;
            getById.PropertyColour = updatePropertyDto.PropertyColour;
            await _context.SaveChangesAsync();

            var resultDto = new GetPropertyDto
            {
                Id = orgId,
                PropertyName = updatePropertyDto.PropertyName,
                PropertyDetails = updatePropertyDto.PropertyDetails,
                Address = updatePropertyDto.Address,
                PropertyColour = updatePropertyDto.PropertyColour,
                OrganizationId = getById.OrganizationId
            };

            response.Data = _mapper.Map<GetPropertyDto>(resultDto);

        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }

        return response;

    }

    public Task<ServiceResponse<int>> DeleteProperty(int propertyId)
    {
        throw new NotImplementedException();
    }
}