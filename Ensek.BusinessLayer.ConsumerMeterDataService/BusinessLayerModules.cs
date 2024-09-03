using System.Diagnostics.CodeAnalysis;
using Ensek.BusinessLayer.ConsumerMeterDataService.Parsers;
using Ensek.BusinessLayer.ConsumerMeterDataService.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Ensek.BusinessLayer.ConsumerMeterDataService;

[ExcludeFromCodeCoverage]
public static class BusinessLayerModules
{
    public static IServiceCollection AddBusinessLayerModules(this IServiceCollection services)
    {
        services.AddScoped<IFileProcessor, FileProcessor>();
        services.AddScoped<ICsvRecordGenerator, CsvRecordGenerator>();
        services.AddScoped<IMeterReadingParser, MeterReadingParser>();
        return services;
    }
}