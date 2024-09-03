
namespace Ensek.Controllers.ConsumerMeterDataService.Constants;

public struct LogEventNames
{
    private const string ServiceName = "ConsumerMeterDataService";

    public struct Errors
    {
        
    }

    public struct Serviecs
    {
        public struct MeterReading
        {
            public const string InvalidRequest = $"{ServiceName}_UploadMeterReadingValidationFailure";
        }
    }
}