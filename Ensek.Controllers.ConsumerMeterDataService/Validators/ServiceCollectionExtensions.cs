using Ensek.Models.ConsumerMeterDataService.External;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ensek.Controllers.ConsumerMeterDataService.Validators;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<FileUpload>, FileUploadValidator>();
        
        return services;
    }
}