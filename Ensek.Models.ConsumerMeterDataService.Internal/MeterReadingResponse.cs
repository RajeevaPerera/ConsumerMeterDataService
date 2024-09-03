namespace Ensek.Models.ConsumerMeterDataService.Internal;

public class MeterReadingResponse
{
    public bool IsFileProcessed { get; set; }
    
    public int TotalMeterReadings { get; set; }
    
    public int UpdatedMeterReadings { get; set; }
    
    public int InvalidMeterReadings { get; set; }
}