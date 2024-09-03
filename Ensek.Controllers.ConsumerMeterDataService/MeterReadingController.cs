using Ensek.BusinessLayer.ConsumerMeterDataService.Processors;
using Ensek.Controllers.ConsumerMeterDataService.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ensek.Models.ConsumerMeterDataService.External;
using FluentValidation;

namespace Ensek.Controllers.ConsumerMeterDataService;

[ApiController]
public class MeterReadingController : ServiceControllerBase<MeterReadingController>
{
    private readonly ILogger<MeterReadingController> _logger;
    private readonly IValidator<FileUpload> _fileUploadValidator;
    private readonly IFileProcessor _fileProcessor;

    public MeterReadingController(
        ILogger<MeterReadingController> logger, 
        IValidator<FileUpload> fileUploadValidator, IFileProcessor fileProcessor) : base(logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileUploadValidator = fileUploadValidator ?? throw new ArgumentNullException(nameof(fileUploadValidator));
        _fileProcessor = fileProcessor ?? throw new ArgumentNullException(nameof(fileProcessor));
    }

    [HttpPost]
    [Route("api/[controller]/meter-reading-uploads")]
    [DisableRequestSizeLimit]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<IActionResult> UploadMeterReadingAsync([FromForm] FileUpload fileUpload)
    {
        var validationResult = await _fileUploadValidator.ValidateAsync(fileUpload);

        if (!validationResult.IsValid)
        {
            return LogAndReturnBadRequest(LogEventNames.Serviecs.MeterReading.InvalidRequest, validationResult.Errors);
        }

        try
        {
            var meterUploadResults = await _fileProcessor.ProcessMeterReadingsAsync(fileUpload.FileDetails);
            return Ok(meterUploadResults);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
}