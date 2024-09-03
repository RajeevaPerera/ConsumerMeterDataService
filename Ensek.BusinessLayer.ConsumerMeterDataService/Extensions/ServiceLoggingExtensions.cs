using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Ensek.BusinessLayer.ConsumerMeterDataService.Extensions;

public static class ServiceLoggingExtensions
{
    private static string EventNameCannotBeNullOrEmpty => "Name of event cannot be any type of empty/null string.";
    
    
    public static void LogAsValidationFailure(this ILogger logger, string eventName, IList<ValidationFailure> validationFailures)
    {
        if (string.IsNullOrWhiteSpace(eventName))
        {
            throw new ArgumentNullException(EventNameCannotBeNullOrEmpty, nameof(eventName));
        }

        if (validationFailures is null || validationFailures.Count == 0)
        {
            throw new ArgumentNullException(EventNameCannotBeNullOrEmpty, nameof(eventName));
        }

        var extraData = new Dictionary<string, object> { { "", ExtractErrorMessages(validationFailures) } };
        
        logger.Log(LogLevel.Warning, eventName, extraData);
    }

    private static string ExtractErrorMessages(IEnumerable<ValidationFailure> validationFailures) =>
        $" - { string.Join($"{Environment.NewLine} -", validationFailures.Select(failure => failure.ErrorMessage).ToList()) }";

}