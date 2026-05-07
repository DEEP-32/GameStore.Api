using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtension {
    public static void MigrateDb(this WebApplication app) {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.
            GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
    
    public static void SeedGenreDb(this WebApplicationBuilder builder) {
        var connectionString = builder.Configuration.GetConnectionString("GameStoreDb");
        builder.Services.AddSqlite<GameStoreContext>(
            connectionString,
            optionsAction: options => options.UseSeeding((context, _) => {
                if (!context.Set<Genre>().Any()) {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Action" },
                        new Genre { Name = "Adventure" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Strategy" },
                        new Genre { Name = "Platformers" }
                    );
            
                    context.SaveChanges();
                }
            })
        );
    }
}