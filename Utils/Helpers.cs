using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using asp_bpm_core7_BE.Models;
using Microsoft.IdentityModel.Tokens;

namespace asp_bpm_core7_BE.Utils;

public class Helpers
{
    public const string OwnerRole = "Owner";
    public const string AdminRole = "Admin";
    public const string MemberRole = "Member";
    public const string OwnerAdminRole = "Owner,Admin";
    public const string AdminMemberRole = "Admin,Member";
    public const string ContractorRole = "Contractor";

    public const int OwnerRoleInt = 1;
    public const int AdminRoleInt = 2;
    public const int MemberRoleInt = 3;
    public const int ContractorRoleInt = 4;

    // Login Expiration
    public const int LoginExpiration = 1;

    // ORDER
    public const string ASC = "ASC";
    public const string DESC = "DESC";

    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }



    public static string RandomString(int length)
    {
        Random random = new();
        const string chars = "AbcdefghIJKlMnOpQrStUvWxYz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
 

}