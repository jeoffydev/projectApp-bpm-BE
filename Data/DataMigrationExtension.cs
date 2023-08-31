
using Microsoft.EntityFrameworkCore;

namespace asp_bpm_core7_BE.Data;

public static class DataMigrationExtension
{
    public static async Task InitializeDbMigrationAsync(this IServiceProvider serviceProvider)
    {
        // To automatically migrate the EF changes every dotnet run
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<Datacontext>();
        await dbContext.Database.MigrateAsync();

        var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                                    .CreateLogger("Database Migration");
        logger.LogInformation(5, "Database is ready!");
    }


}