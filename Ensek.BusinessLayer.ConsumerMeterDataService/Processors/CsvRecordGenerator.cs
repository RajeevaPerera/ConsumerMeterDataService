using Ensek.BusinessLayer.ConsumerMeterDataService.Parsers;
using Ensek.Models.ConsumerMeterDataService.Internal;

namespace Ensek.BusinessLayer.ConsumerMeterDataService.Processors;

public class CsvRecordGenerator : ICsvRecordGenerator
{
    private readonly IMeterReadingParser _meterReadingParser;
    
    public CsvRecordGenerator(IMeterReadingParser meterReadingParser)
    {
        _meterReadingParser = meterReadingParser ?? throw new ArgumentNullException(nameof(meterReadingParser));
    }
    public async Task<Tuple<IEnumerable<MeterReading>, int>> GenerateMeterReadings(MemoryStream stream)
    {
        using var reader = new StreamReader(stream);
        
        var csvRawText = await reader.ReadToEndAsync();

        var (meterReadings, failedCount) = _meterReadingParser.CvsParser(csvRawText);
        
        return new Tuple<IEnumerable<MeterReading>, int>(meterReadings, failedCount);
    }
}