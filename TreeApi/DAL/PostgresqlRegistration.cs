using Microsoft.EntityFrameworkCore;

namespace TreeApp.DAL;

internal static class PostgresqlRegistration
{
    private const string SectionName = "database";

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        
        services.Configure<PostgresOptions>(section);
        var options = configuration.GetOptions<PostgresOptions>(SectionName);
        
        services.AddDbContext<TreeDbContext>(x => x.UseNpgsql(options.ConnectionString, sqlServerOptions =>
        {
            sqlServerOptions.CommandTimeout(1);
        }));
        services.AddHostedService<DatabaseInitializer>();

        return services;
    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}