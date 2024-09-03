using System.Diagnostics.CodeAnalysis;
using Ensek.Controllers.ConsumerMeterDataService.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Ensek.Controllers.ConsumerMeterDataService;

[ExcludeFromCodeCoverage]
public static class ControllerModules
{
    public static IServiceCollection AddControllerModules(this IServiceCollection services)
    {
        services.AddValidators();
        return services;
    }
}