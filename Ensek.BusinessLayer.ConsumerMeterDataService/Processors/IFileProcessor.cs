using Ensek.Models.ConsumerMeterDataService.Internal;
using Microsoft.AspNetCore.Http;

namespace Ensek.BusinessLayer.ConsumerMeterDataService.Processors;

public interface IFileProcessor
{
    public Task<MeterReadingResponse> ProcessMeterReadingsAsync(IFormFile fileData, CancellationToken cancellationToken = default);
}