using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ensek.Models.ConsumerMeterDataService.Entities;

public class CustomerAccount
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string LastName { get; set; }
    
    [JsonIgnore]
    public List<MeterReading> MeterReadings { get; set; }
}
