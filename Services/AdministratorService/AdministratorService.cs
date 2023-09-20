using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Dtos;
using asp_bpm_core7_BE.Dtos.AdministratorDtos;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace asp_bpm_core7_BE.Services.AdministratorService;

public class AdministratorService : IAdministratorService
{
    public readonly Datacontext _context;
    private readonly IMapper _mapper;
    public readonly IConfiguration _configuration;

    public AdministratorService(Datacontext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<bool> AdministratorExists(string email)
    {
        if (await _context.Administrators.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
        {
            return true;
        }
        return false;
    }

    public async Task<bool> AdministratorExistsById(int id)
    {
        var checkOwner = await _context.Administrators.FirstOrDefaultAsync(s => s.Id == id);
        if (checkOwner is not null)
        {
            return true;
        }
        return false;
    }

    public async Task<ServiceResponse<VerifySecretKeyDto>> AdministratorFromEmailLoginVerification(string secretKey)
    {
        var response = new ServiceResponse<VerifySecretKeyDto>();
        var getBySecretKey = await _context.Administrators.FirstOrDefaultAsync(s => s.SecretKey == secretKey);

        if (getBySecretKey is not null)
        {

            getBySecretKey.SecretKey = "";
            await _context.SaveChangesAsync();
            response.Data = new VerifySecretKeyDto
            {
                Email = getBySecretKey.Email,
                Verified = true
            };
        }
        else
        {
            response.Success = false;
        }
        return response;
    }

    public async Task<ServiceResponse<int>> DeleteAdministrator(int id)
    {
        var response = new ServiceResponse<int>();
        if (await AdministratorExistsById(id))
        {
            await _context.Administrators.Where(u => u.Id == id).ExecuteDeleteAsync();
            response.Data = id;
        }
        else
        {
            response.Success = false;
            response.Message = "Administrator not found.";
        }

        return response;
    }

    public async Task<ServiceResponse<string>> GenerateAdministratorLoginVerification(string email)
    {
        var response = new ServiceResponse<string>();
        var getByEmail = await _context.Administrators.FirstOrDefaultAsync(s => s.Email == email);
        if (getByEmail is not null)
        {
            var secret = Helpers.RandomString(48);
            getByEmail.SecretKey = secret;
            await _context.SaveChangesAsync();
            response.Data = secret;
        }
        else
        {
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<GetAdministratorDto>> GetAdministrator(int id)
    {
        var response = new ServiceResponse<GetAdministratorDto>();
        try
        {


            var findAdmin = await _context.Administrators.Include(a => a.AuthRole).Include(o => o.Organization).FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception($"Administrator Id `{id}` is not found");
            var adminDto = new GetAdministratorDto
            {
                Id = findAdmin.Id,
                FullName = findAdmin.FullName,
                Email = findAdmin.Email,
                Active = (bool)findAdmin.Active!,
                AuthRoleId = findAdmin.AuthRoleId,
                RoleName = findAdmin?.AuthRole?.RoleName!,
                Mobile = findAdmin?.Mobile!,
                Phone = findAdmin?.Phone!,
                OrganizationId = (int)findAdmin?.OrganizationId!,
                OrganizationName = findAdmin?.Organization?.CompanyName!
            };
            response.Data = adminDto;
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }


        return response;
    }

    private IQueryable<Administrator> FilterUserOrderByClause(int? roleId)
    {
        var WhereRole = roleId is not null ? _context.Administrators.Include(a => a.AuthRole)
        .Where(
                    u => u.AuthRoleId == roleId
            ) : _context.Administrators.Include(a => a.AuthRole);

        return WhereRole.OrderBy((e) => e.FullName);
    }

    public async Task<ServiceResponse<List<GetAdministratorDto>>> GetAllAdministrators(int? roleId)
    {
        var ServiceResponse = new ServiceResponse<List<GetAdministratorDto>>();
        var dbAdmins = await FilterUserOrderByClause(roleId)
               .AsNoTracking().ToListAsync();

        var adminDtos = new List<GetAdministratorDto>();

        foreach (var dbAdmin in dbAdmins)
        {
            GetAdministratorDto adminAdded = new()
            {
                Id = dbAdmin.Id,
                Email = dbAdmin.Email,
                FullName = dbAdmin.FullName,
                Active = (bool)dbAdmin.Active!,
                AuthRoleId = dbAdmin.AuthRoleId,
                RoleName = dbAdmin?.AuthRole?.RoleName!,
                Mobile = dbAdmin?.Mobile!,
                Phone = dbAdmin?.Phone!,
                OrganizationId = (int)dbAdmin?.OrganizationId!

            };
            adminDtos.Add(adminAdded);
        }
        ServiceResponse.Data = adminDtos;
        return ServiceResponse;
    }

    public async Task<ServiceResponse<AuthAdminResponse>> LoginAdministrator(string email, string password)
    {
        var response = new ServiceResponse<AuthAdminResponse>();
        var admin = await _context.Administrators.Include(a => a.AuthRole)
        .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()) && u.Active.Equals(true) && u.AuthRoleId.Equals(Helpers.AdminRoleInt));

        var msg = "User not found or wrong password";
        if (admin is null)
        {
            response.Success = false;
            response.Message = msg;
        }
        else if (!Helpers.VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
        {
            response.Success = false;
            response.Message = $"{msg}. Please try again";
        }
        else
        {
            response.Data = new AuthAdminResponse
            {
                Id = admin.Id,
                Email = admin.Email,
                FullName = admin.FullName,
                TokenKey = CreateTokenAdministrator(admin),
                RoleId = admin.AuthRoleId,
                RoleName = admin?.AuthRole?.RoleName,
                Mobile = admin?.Mobile!,
                Phone = admin?.Phone!
            };
        }
        return response;
    }

    public async Task<GetAdministratorDto> RegisterAdministrator(Administrator admin, string password)
    {
        Helpers.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        admin.PasswordHash = passwordHash;
        admin.PasswordSalt = passwordSalt;
        admin.SecretKey = Helpers.RandomString(35);

        _context.Administrators.Add(admin);
        await _context.SaveChangesAsync();

        var resultDto = new GetAdministratorDto
        {
            Id = admin.Id,
            FullName = admin.FullName,
            Email = admin.Email,
            AuthRoleId = admin.AuthRoleId,
            Mobile = admin.Mobile,
            Phone = admin.Phone,
            OrganizationId = admin.OrganizationId,
            Active = admin.Active
        };
        return resultDto;
    }

    public async Task<ServiceResponse<GetAdministratorDto>> UpdateAdministrator(UpdateAdministratorDto admin, string password)
    {
        var response = new ServiceResponse<GetAdministratorDto>();
        try
        {
            var getById = await _context.Administrators.FirstOrDefaultAsync(s => s.Id == admin.Id) ?? throw new Exception($"Administrator Id `{admin.Id}` is not found");

            Helpers.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            getById.PasswordHash = passwordHash;
            getById.PasswordSalt = passwordSalt;

            getById.FullName = admin.FullName;
            getById.Active = admin.Active;
            getById.Mobile = admin.Mobile;
            getById.Phone = admin.Phone;

            var viewUpdate = new GetAdministratorDto
            {
                Id = getById.Id,
                Email = getById.Email,
                FullName = admin.FullName,
                Active = admin.Active,
                AuthRoleId = getById.AuthRoleId,
                OrganizationId = getById.OrganizationId,
                Mobile = admin.Mobile,
                Phone = admin.Phone
            };

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetAdministratorDto>(viewUpdate);
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }

        return response;
    }

    private string CreateTokenAdministrator(Administrator user)
    {

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user?.AuthRole?.RoleName!)
        };

        var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
        if (appSettingsToken is null)
        {
            throw new Exception("AppSettings Token is null");
        }
        SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(appSettingsToken));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<ServiceResponse<List<GetAdministratorDto>>> GetAllAdministratorsByOrgId(int orgId)
    {
        var response = new ServiceResponse<List<GetAdministratorDto>>();
        try
        {
            var findAdmins = await _context.Administrators.Include(a => a.AuthRole).Include(o => o.Organization).Where(s => s.OrganizationId == orgId).ToListAsync() ?? throw new Exception($"Org Id `{orgId}` is not found");

            var adminDtos = new List<GetAdministratorDto>();

            foreach (var dbAdmin in findAdmins)
            {
                GetAdministratorDto adminAdded = new()
                {
                    Id = dbAdmin.Id,
                    Email = dbAdmin.Email,
                    FullName = dbAdmin.FullName,
                    Active = (bool)dbAdmin.Active!,
                    AuthRoleId = dbAdmin.AuthRoleId,
                    RoleName = dbAdmin?.AuthRole?.RoleName!,
                    Mobile = dbAdmin?.Mobile!,
                    Phone = dbAdmin?.Phone!,
                    OrganizationId = (int)dbAdmin?.OrganizationId!,
                    OrganizationName = dbAdmin?.Organization?.CompanyName!

                };
                adminDtos.Add(adminAdded);
            }
            response.Data = adminDtos;

        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }

        return response;
    }

    public async Task<OrganizationUserResponse<GetAdministratorDto>> GetUserClaimDetails(int id)
    {
        var response = new OrganizationUserResponse<GetAdministratorDto>();
        try
        {
            var findAdmin = await _context.Administrators.Include(a => a.AuthRole).Include(o => o.Organization).FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception($"User Id `{id}` is not found");
            var userDto = new GetAdministratorDto
            {
                Id = findAdmin.Id,
                FullName = findAdmin.FullName,
                Email = findAdmin.Email,
                Active = (bool)findAdmin.Active!,
                AuthRoleId = findAdmin.AuthRoleId,
                RoleName = findAdmin?.AuthRole?.RoleName!,
                Mobile = findAdmin?.Mobile!,
                Phone = findAdmin?.Phone!,
                OrganizationId = (int)findAdmin?.OrganizationId!,
                OrganizationName = findAdmin?.Organization?.CompanyName!
            };
            response.Data = userDto;
        }
        catch (Exception err)
        {
            response.ErrorMsg = err.Message;
            response.Success = false;
        }
        return response;
    }

    public async Task<ServiceResponse<bool>> UpdateAdministratorByClaims(UpdateDetailsUserDto userDto, int id)
    {
        var response = new ServiceResponse<bool>();
        try
        {
            var getById = await _context.Administrators.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception($"User Id `{id}` is not found");
            getById.FullName = userDto.FullName;
            await _context.SaveChangesAsync();
            response.Data = true;
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> UpdateAdministratorPasswordByClaims(string password, int id)
    {
        var response = new ServiceResponse<bool>();
        try
        {
            var getById = await _context.Administrators.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception($"User Id `{id}` is not found");
            Helpers.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            getById.PasswordHash = passwordHash;
            getById.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();
            response.Data = true;
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }

        return response;
    }
}