
using Ensek.BusinessLayer.ConsumerMeterDataService.Processors;
using Ensek.DataLayer.ConsumerMeterDataService.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ensek.UnitTest.ConsumerMeterDataService.BusinessLayer;

public class FileProcessorTests
{
    private readonly ILogger<FileProcessor> _logger;
    private readonly IMeterReadingRepository _meterReadingRepository;
    private readonly ICsvRecordGenerator _csvRecordGenerator;
    
    private readonly FileProcessor _fileProcessor;
    private readonly IFormFile _formFile;
    
    public FileProcessorTests()
    {
        _formFile = TestHelper.CreateTestFormFile("test.csv", "AccountId,MeterReadingDateTime,MeterReadValue,\n2344,22/04/2019 09:24,1002,\n2233,22/04/2019 12:25,323,");
        _logger = Substitute.For<ILogger<FileProcessor>>();
        
        _meterReadingRepository = Substitute.For<IMeterReadingRepository>();
        
        _csvRecordGenerator = Substitute.For<ICsvRecordGenerator>();
        _csvRecordGenerator.GenerateMeterReadings(Arg.Any<MemoryStream>())
            .Returns(new Tuple<IEnumerable<Ensek.Models.ConsumerMeterDataService.Internal.MeterReading>, int>(new []
            {
                new Ensek.Models.ConsumerMeterDataService.Internal.MeterReading{AccountId = 123, MeterReadingDateTime=DateTime.Now,MeterReadValue=12345},
                new Ensek.Models.ConsumerMeterDataService.Internal.MeterReading{AccountId = 321, MeterReadingDateTime=DateTime.Now,MeterReadValue=12345}
            }, 1));
        
        
        _fileProcessor = new FileProcessor(_logger, _meterReadingRepository, _csvRecordGenerator);
    }

    [Fact]
    private void Constructor_RequiredParametersNull_ThrowsArgumentNullException()
    {
        GenericTests.TestsConstructor<FileProcessor>();
    }
    
    [Fact]
    private async Task ProcessMeterReadingsAsync_WhenCustomerAccountIsNull_MeterReadingResponseReturned()
    {
        _meterReadingRepository.FindCustomerAccountByAccountIdAsync(123)
            .Returns(Task.FromResult(new Ensek.Models.ConsumerMeterDataService.Entities.MeterReading{CustomerAccountId = 0}));
       var meterReadingResponse = await _fileProcessor.ProcessMeterReadingsAsync(_formFile);
       
       meterReadingResponse.TotalMeterReadings.Should().Be(2);
       meterReadingResponse.IsFileProcessed.Should().BeTrue();
    }
    
}