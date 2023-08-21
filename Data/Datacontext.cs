using System.Reflection;
using asp_bpm_core7_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_bpm_core7_BE.Data;

public class Datacontext : DbContext
{

    public Datacontext(DbContextOptions<Datacontext> options) : base(options)
    {

    }

    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<AuthRole> AuthRoles => Set<AuthRole>();
    public DbSet<Organization> Organizations => Set<Organization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<AuthRole>()
        .HasData(
            new AuthRole
            {
                Id = 1,
                RoleName = "Owner",
                RoleDescription = "The owner with highest permission on this app"
            },
            new AuthRole
            {
                Id = 2,
                RoleName = "Admin",
                RoleDescription = "The Customer administrator and has high permission"
            },
            new AuthRole
            {
                Id = 3,
                RoleName = "Member",
                RoleDescription = "Limited access in the customers private pages"
            },
            new AuthRole
            {
                Id = 4,
                RoleName = "Contractor",
                RoleDescription = "Static and read only access"
            }
        );

        modelBuilder.Entity<Owner>()
       .HasData(
               GenerateUser()
       );

    }

    public static Owner GenerateUser()
    {
        var user = new Owner
        {
            Id = 1001,
            FullName = "Jeoffy Hipolito",
            Email = "jeoffy_hipolito@yahoo.com",
            Active = true,
            AuthRoleId = 1,
        };
        CreatePasswordHash("jeoffyOwner", out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        return user;
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}