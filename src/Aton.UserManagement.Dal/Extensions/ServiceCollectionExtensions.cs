using Aton.UserManagement.Dal.Infrastructure;
using Aton.UserManagement.Dal.Repositories;
using Aton.UserManagement.Dal.Repositories.Interfaces;
using Aton.UserManagement.Dal.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aton.UserManagement.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }

    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot config)
    {
        //read config
        services.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));

        //configure postrges types
        Postgres.MapCompositeTypes();

        //add migrations
        Postgres.AddMigrations(services);

        return services;
    }
}