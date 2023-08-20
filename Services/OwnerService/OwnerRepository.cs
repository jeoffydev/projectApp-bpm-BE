
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Dtos;
using asp_bpm_core7_BE.Models;
using asp_bpm_core7_BE.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace asp_bpm_core7_BE.Services.OwnerService;

public class OwnerRepository : IOwnerRepository
{

    public readonly Datacontext _context;
    private readonly IMapper _mapper;
    public readonly IConfiguration _configuration;

    public OwnerRepository(Datacontext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    private IQueryable<Owner> FilterUserOrderByClause(int? roleId)
    {
        var WhereRole = roleId is not null ? _context.Owners.Include(a => a.AuthRole)
        .Where(
                    u => u.AuthRoleId == roleId
            ) : _context.Owners.Include(a => a.AuthRole);

        return WhereRole.OrderBy((e) => e.FullName);
    }
    public async Task<ServiceResponse<List<GetOwnerDto>>> GetAllOwners(int? roleId)
    {
        var ServiceResponse = new ServiceResponse<List<GetOwnerDto>>();
        var dbOwners = await FilterUserOrderByClause(roleId)
               .AsNoTracking().ToListAsync();

        var ownerDtos = new List<GetOwnerDto>();

        foreach (var dbOwner in dbOwners)
        {
            GetOwnerDto ownerAdded = new()
            {
                Id = dbOwner.Id,
                Email = dbOwner.Email,
                FullName = dbOwner.FullName,
                Active = (bool)dbOwner.Active!,
                AuthRoleId = dbOwner.AuthRoleId,
                RoleName = dbOwner?.AuthRole?.RoleName

            };
            ownerDtos.Add(ownerAdded);
        }
        ServiceResponse.Data = ownerDtos;
        return ServiceResponse;

    }



    public async Task<ServiceResponse<GetOwnerDto>> RegisterOwner(Owner owner, string password)
    {

        var response = new ServiceResponse<GetOwnerDto>();
        if (await OwnerExists(owner.Email))
        {
            response.Success = false;
            response.Message = "Owner already exists.";
            return response;
        }

        Helpers.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        owner.PasswordHash = passwordHash;
        owner.PasswordSalt = passwordSalt;
        owner.SecretKey = Helpers.RandomString(35);

        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();

        var resultDto = new GetOwnerDto
        {
            Id = owner.Id,
            FullName = owner.FullName,
            Email = owner.Email
        };

        response.Data = resultDto;

        return response;
    }

    public async Task<bool> OwnerExists(string email)
    {
        if (await _context.Owners.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
        {
            return true;
        }
        return false;
    }

    public async Task<bool> OwnerExistsById(int id)
    {
        var checkOwner = await _context.Owners.FirstOrDefaultAsync(s => s.Id == id);
        if (checkOwner is not null)
        {
            return true;
        }
        return false;
    }

    public async Task<ServiceResponse<int>> DeleteOwner(int id)
    {
        var response = new ServiceResponse<int>();
        if (await OwnerExistsById(id))
        {
            await _context.Owners.Where(u => u.Id == id).ExecuteDeleteAsync();
            response.Data = id;
        }
        else
        {
            response.Success = false;
            response.Message = "Owner not found.";
        }

        return response;
    }

    public async Task<ServiceResponse<GetOwnerDto>> GetOwner(int id)
    {
        var response = new ServiceResponse<GetOwnerDto>();
        try
        {


            var findOwner = await _context.Owners.Include(a => a.AuthRole).FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception($"Owner Id `{id}` is not found");
            var ownerDto = new GetOwnerDto
            {
                Id = findOwner.Id,
                FullName = findOwner.FullName,
                Email = findOwner.Email,
                Active = (bool)findOwner.Active!,
                AuthRoleId = findOwner.AuthRoleId,
                RoleName = findOwner?.AuthRole?.RoleName
            };
            response.Data = ownerDto;
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }


        return response;
    }

    public async Task<ServiceResponse<GetOwnerDto>> UpdateOwner(UpdateOwnerDto owner, string password)
    {
        var response = new ServiceResponse<GetOwnerDto>();
        try
        {
            var getById = await _context.Owners.FirstOrDefaultAsync(s => s.Id == owner.Id) ?? throw new Exception($"Owner Id `{owner.Id}` is not found");

            Helpers.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            getById.PasswordHash = passwordHash;
            getById.PasswordSalt = passwordSalt;

            getById.FullName = owner.FullName;
            getById.Active = owner.Active;
            getById.AuthRoleId = owner.RoleId;

            var viewUpdate = new GetOwnerDto
            {
                Email = getById.Email,
                FullName = owner.FullName,
                Active = owner.Active,
                AuthRoleId = owner.RoleId
            };

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetOwnerDto>(viewUpdate);
        }
        catch (Exception err)
        {
            response.Success = false;
            response.Message = err.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<AuthOwnerResponse>> LoginOwner(string email, string password)
    {
        var response = new ServiceResponse<AuthOwnerResponse>();
        var owner = await _context.Owners.Include(a => a.AuthRole)
        .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()) && u.Active.Equals(true) && u.AuthRoleId.Equals(Helpers.OwnerRoleInt));

        var msg = "User not found or wrong password";
        if (owner is null)
        {
            response.Success = false;
            response.Message = msg;
        }
        else if (!Helpers.VerifyPasswordHash(password, owner.PasswordHash, owner.PasswordSalt))
        {
            response.Success = false;
            response.Message = $"{msg}. Please try again";
        }
        else
        {
            response.Data = new AuthOwnerResponse
            {
                Id = owner.Id,
                Email = owner.Email,
                FullName = owner.FullName,
                TokenKey = CreateToken(owner),
                RoleId = owner.AuthRoleId,
                RoleName = owner?.AuthRole?.RoleName
            };
        }
        return response;
    }


    private string CreateToken(Owner user)
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

    public async Task<ServiceResponse<List<GetRoleDto>>> GetRoles()
    {
        var response = new ServiceResponse<List<GetRoleDto>>();
        var results = await _context.AuthRoles.ToListAsync();

        var roleDtos = new List<GetRoleDto>();

        foreach (var res in results)
        {
            GetRoleDto roleAdded = new()
            {
                Id = res.Id,
                RoleName = res.RoleName,
                RoleDescription = res.RoleDescription
            };

            roleDtos.Add(roleAdded);
        }
        response.Data = roleDtos;
        return response;
    }

    public async Task<ServiceResponse<string>> GenerateLoginVerification(string email)
    {
        var response = new ServiceResponse<string>();
        var getByEmail = await _context.Owners.FirstOrDefaultAsync(s => s.Email == email);
        if (getByEmail is not null)
        {
            var secret = Helpers.RandomString(45);
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

    public async Task<ServiceResponse<VerifySecretKeyDto>> FromEmailLoginVerification(string secretKey)
    {
        var response = new ServiceResponse<VerifySecretKeyDto>();
        var getBySecretKey = await _context.Owners.FirstOrDefaultAsync(s => s.SecretKey == secretKey);

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
}