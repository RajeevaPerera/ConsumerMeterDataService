using Microsoft.AspNetCore.Http;

namespace Ensek.Models.ConsumerMeterDataService.External;

public record FileUpload
{
    public IFormFile FileDetails { get; set; }
}