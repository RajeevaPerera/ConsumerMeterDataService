using Ensek.DataLayer.ConsumerMeterDataService.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ensek.DataLayer.ConsumerMeterDataService;

public static class DataLayerModules
{
    public static IServiceCollection AddDataLayerModules(this IServiceCollection services)
    {
        services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();
        services.AddScoped<MeterReadingsContext, MeterReadingsContext>();
        return services;
    }
}