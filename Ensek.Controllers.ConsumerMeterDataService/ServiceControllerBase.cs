using Ensek.BusinessLayer.ConsumerMeterDataService.Extensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ensek.Controllers.ConsumerMeterDataService;

public class ServiceControllerBase<T>(ILogger<T> logger) : ControllerBase
    where T : class
{
    private readonly ILogger<T> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    internal BadRequestObjectResult LogAndReturnBadRequest(string logEventName, IEnumerable<ValidationFailure> validationFailures)
    {
        _logger.LogAsValidationFailure(logEventName, validationFailures.ToList());
        
        return BadRequest(validationFailures);
    }
}