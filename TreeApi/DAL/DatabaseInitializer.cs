using Microsoft.EntityFrameworkCore;

namespace TreeApp.DAL;

internal sealed class DatabaseInitializer(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TreeDbContext>();
            await dbContext.Database.MigrateAsync(cancellationToken);
            
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            await seeder.Seed();
        }

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
}