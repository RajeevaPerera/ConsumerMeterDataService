using Ensek.Models.ConsumerMeterDataService.Internal;

namespace Ensek.BusinessLayer.ConsumerMeterDataService.Parsers;

public interface IMeterReadingParser
{
    public (IList<MeterReading> meterReadings, int failedToReadCount) CvsParser(string csvString);
}