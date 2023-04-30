using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Aton.UserManagement.Bll.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBll(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddTransient<IUserManagementService, UserManagementService>();
        services.AddTransient<AuthorizationService>();
// TODO добавить новые сервисы
        return services;
    }
}