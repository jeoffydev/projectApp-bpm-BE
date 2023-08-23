using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;
using asp_bpm_core7_BE.Dtos.OrganizationDtos;
using asp_bpm_core7_BE.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace asp_bpm_core7_BE.Services.OrganizationService;

public class OrganizationRepository : IOrganizationRepository
{
    public readonly Datacontext _context;
    private readonly IMapper _mapper;

    public OrganizationRepository(Datacontext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<int>> DeleteOrganization(int id)
    {
        var response = new ServiceResponse<int>();
        if (await OrganizationExistsById(id))
        {
            await _context.Organizations.Where(u => u.Id == id).ExecuteDeleteAsync();
            response.Data = id;
        }
        else
        {
            response.Success = false;
            response.Message = "Org not found.";
        }

        return response;
    }

    public async Task<ServiceResponse<List<GetOrgDto>>> GetAllOrganizations()
    {
        var ServiceResponse = new ServiceResponse<List<GetOrgDto>>();
        var dbOrgs = await _context.Organizations.AsNoTracking().ToListAsync();

        var orgDtos = new List<GetOrgDto>();

        foreach (var dbOrg in dbOrgs)
        {
            GetOrgDto orgAdded = new()
            {
                Id = dbOrg.Id,
                CompanyName = dbOrg.CompanyName,
                BusinessDetails = dbOrg.BusinessDetails,
                Address = dbOrg.Address,
                Active = dbOrg.Active,
                ContactEmail = dbOrg.ContactEmail,
                ContactPerson = dbOrg.ContactPerson,
                MobileNumber = dbOrg.MobileNumber,
                PhoneNumber = dbOrg.PhoneNumber,
                Website = dbOrg.Website,

            };
            orgDtos.Add(orgAdded);
        }
        ServiceResponse.Data = orgDtos;
        return ServiceResponse;
    }

    public async Task<ServiceResponse<GetOrgDto>> GetOrganization(int id)
    {
        var response = new ServiceResponse<GetOrgDto>();
        try
        {


            var findOrg = await _context.Organizations.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception($"Org Id `{id}` is not found");

            var findAdmins = await _context.Administrators.Include(r => r.AuthRole).Where(a => a.OrganizationId == id).AsNoTracking().ToListAsync();
            var admins = new List<GetAdministratorDto>() { };
            if (findAdmins is not null)
            {
                foreach (var admin in findAdmins)
                {
                    GetAdministratorDto adminAdded = new()
                    {
                        Id = admin.Id,
                        Email = admin.Email,
                        FullName = admin.FullName,
                        Active = admin.Active,
                        AuthRoleId = admin.AuthRoleId,
                        Mobile = admin.Mobile,
                        Phone = admin.Phone,
                        RoleName = admin?.AuthRole?.RoleName!,
                        OrganizationId = findOrg.Id
                    };
                    admins.Add(adminAdded);
                }
            }

            var orgDto = new GetOrgDto
            {
                Id = findOrg.Id,
                CompanyName = findOrg.CompanyName,
                BusinessDetails = findOrg.BusinessDetails,
                Address = findOrg.Address,
                Active = findOrg.Active,
                ContactEmail = findOrg.ContactEmail,
                ContactPerson = findOrg.ContactPerson,
                MobileNumber = findOrg.MobileNumber,
                PhoneNumber = findOrg.PhoneNumber,
                Website = findOrg.Website,
                GetAdministratorDtos = admins
            };
            response.Data = orgDto;
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }


        return response;
    }

    public async Task<bool> OrganizationExistsById(int id)
    {
        var checkOrg = await _context.Organizations.FirstOrDefaultAsync(s => s.Id == id);
        if (checkOrg is not null)
        {
            return true;
        }
        return false;
    }

    public async Task<ServiceResponse<GetOrgDto>> RegisterOrganization(Organization organization)
    {

        var response = new ServiceResponse<GetOrgDto>();

        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();

        var resultDto = new GetOrgDto
        {
            Id = organization.Id,
            CompanyName = organization.CompanyName,
            BusinessDetails = organization.BusinessDetails,
            Address = organization.Address,
            Active = organization.Active,
            ContactEmail = organization.ContactEmail,
            ContactPerson = organization.ContactPerson,
            MobileNumber = organization.MobileNumber,
            PhoneNumber = organization.PhoneNumber,
            Website = organization.Website,
        };

        response.Data = resultDto;

        return response;
    }

    public async Task<ServiceResponse<GetOrgDto>> UpdateOrganization(UpdateOrgDto updateOrg)
    {
        var response = new ServiceResponse<GetOrgDto>();
        try
        {
            var getById = await _context.Organizations.FirstOrDefaultAsync(s => s.Id == updateOrg.Id) ?? throw new Exception($"Org Id `{updateOrg.Id}` is not found");

            getById.CompanyName = updateOrg.CompanyName;
            getById.PhoneNumber = updateOrg.PhoneNumber;
            getById.MobileNumber = updateOrg.MobileNumber;
            getById.Address = updateOrg.Address;
            getById.BusinessDetails = updateOrg.BusinessDetails;
            getById.Active = updateOrg.Active;
            getById.ContactEmail = updateOrg.ContactEmail;
            getById.ContactPerson = updateOrg.ContactPerson;
            getById.Website = updateOrg.Website;

            await _context.SaveChangesAsync();

            var viewUpdate = new GetOrgDto
            {
                Id = getById.Id,
                CompanyName = updateOrg.CompanyName,
                BusinessDetails = updateOrg.BusinessDetails,
                Address = updateOrg.Address,
                Active = updateOrg.Active,
                ContactEmail = updateOrg.ContactEmail,
                ContactPerson = updateOrg.ContactPerson,
                MobileNumber = updateOrg.MobileNumber,
                PhoneNumber = updateOrg.PhoneNumber,
                Website = updateOrg.Website,
            };

            response.Data = _mapper.Map<GetOrgDto>(viewUpdate);
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }

        return response;
    }
}