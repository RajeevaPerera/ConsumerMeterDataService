using Ensek.DataLayer.ConsumerMeterDataService.Repositories;
using Ensek.Models.ConsumerMeterDataService.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ensek.BusinessLayer.ConsumerMeterDataService.Processors;

public class FileProcessor : IFileProcessor
{
    private readonly ILogger<FileProcessor> _logger;
    private readonly IMeterReadingRepository _meterReadingRepository;
    private readonly ICsvRecordGenerator _csvRecordGenerator;
    
    public FileProcessor(ILogger<FileProcessor> logger, IMeterReadingRepository meterReadingRepository, ICsvRecordGenerator csvRecordGenerator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _meterReadingRepository = meterReadingRepository ?? throw new ArgumentNullException(nameof(meterReadingRepository));
        _csvRecordGenerator = csvRecordGenerator ?? throw new ArgumentNullException(nameof(csvRecordGenerator));
    }
    public async Task<MeterReadingResponse> ProcessMeterReadingsAsync(IFormFile fileData, CancellationToken cancellationToken = default)
    {
        // builder to generate this object
        var meterReadingResponse = new MeterReadingResponse();
        
        _logger.LogInformation($"Processing MeterReadings for {fileData.FileName}");

        try
        {
            var fileDetails = new FileDetails
            {
                Id = new Guid(),
                FileName = fileData.FileName
            };

            using var stream = new MemoryStream();
            fileData.CopyTo(stream);
            fileDetails.FileData = stream.ToArray();

            if (fileDetails.FileData.Length == 0) return meterReadingResponse;

            var (meterReadings, failedCount) =
                await _csvRecordGenerator.GenerateMeterReadings(new MemoryStream(fileDetails.FileData));

            //Update current failed count
            meterReadingResponse.InvalidMeterReadings = failedCount;

            foreach (var meterReading in meterReadings)
            {
                var customerAccount =
                    await _meterReadingRepository.FindCustomerAccountByAccountIdAsync(meterReading.AccountId);

                if (customerAccount?.CustomerAccountId != null)
                {
                    //When meter reading is newer than existing reading 
                    if (meterReading.MeterReadingDateTime.CompareTo(customerAccount.DateTime) >= 0)
                    {
                        customerAccount.DateTime = meterReading.MeterReadingDateTime;
                        //Check reading is older than existing
                        if (Math.Abs(meterReading.MeterReadValue) < customerAccount.MeterReadValue)
                        {
                            _logger.LogInformation(
                                $"[{meterReading.AccountId}] - Provided meter reading is older than current meter reading");
                            meterReadingResponse.InvalidMeterReadings++;
                        }
                        else
                        {
                            _logger.LogInformation($"[{meterReading.AccountId}] - Meter reading Updated successfully");
                            customerAccount.MeterReadValue = Math.Abs(meterReading.MeterReadValue);
                            await _meterReadingRepository.UpdateAndSaveChangesAsync(customerAccount);
                            meterReadingResponse.UpdatedMeterReadings++;
                        }
                    }
                    else
                    {
                        _logger.LogInformation(
                            $"[{meterReading.AccountId}] - Date {meterReading.MeterReadingDateTime} is older than last updated date: {customerAccount.DateTime}");
                        meterReadingResponse.InvalidMeterReadings++;
                    }
                }
                else
                {
                    //This may be a new customer may not exist in the db

                }

                meterReadingResponse.TotalMeterReadings++;
                await _meterReadingRepository.SaveChangesAsync();
            }

            _logger.LogInformation($"Processing MeterReadings for {fileData.FileName} completed");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, $"An error occured while processing MeterReadings file: {fileData.FileName}");
            throw;
        }
        
        meterReadingResponse.IsFileProcessed = true;
        return meterReadingResponse;
    }
}