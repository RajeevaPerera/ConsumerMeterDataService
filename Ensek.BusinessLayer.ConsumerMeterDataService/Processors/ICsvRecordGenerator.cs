using Ensek.Models.ConsumerMeterDataService.Internal;

namespace Ensek.BusinessLayer.ConsumerMeterDataService.Processors;

public interface ICsvRecordGenerator
{
    public  Task<Tuple<IEnumerable<MeterReading>, int>> GenerateMeterReadings(MemoryStream stream);
}