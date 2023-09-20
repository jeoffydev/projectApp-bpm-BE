using asp_bpm_core7_BE.Data;
using asp_bpm_core7_BE.Services.AdministratorService;
using asp_bpm_core7_BE.Services.OrganizationService;
using asp_bpm_core7_BE.Services.OwnerService;
using asp_bpm_core7_BE.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger authentication

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the bearer scheme. Ex. "bearer {token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Add Automapper and the rest of tje services here
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddHttpContextAccessor();


// Add Authentication and Authorization nuget - dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)), //null-forgiving!
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
);
// CORS 
builder.Services.AddBuildingProjectManagementCors(builder.Configuration);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<Datacontext>(connString);

builder.Logging.AddAzureWebAppDiagnostics();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// to automatically migrate the EF changes
await app.Services.InitializeDbMigrationAsync();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
