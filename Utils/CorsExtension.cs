namespace asp_bpm_core7_BE.Utils;

public static class CorsExtension
{
    private static string allowedOriginSetting = "AllowedOrigin";
    public static IServiceCollection AddBuildingProjectManagementCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(
            options =>
            {
                options.AddDefaultPolicy(corsBuilder =>
                {
                    var allowedOrigin = configuration[allowedOriginSetting]
                        ?? throw new InvalidOperationException("Allowed origin is not set!");
                    corsBuilder.WithOrigins(allowedOrigin)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-Pagination");
                });
            }
        );


        return services;
    }
}