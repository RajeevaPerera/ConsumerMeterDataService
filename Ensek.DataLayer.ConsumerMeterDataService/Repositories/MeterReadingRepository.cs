using Ensek.Models.ConsumerMeterDataService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ensek.DataLayer.ConsumerMeterDataService.Repositories;

public class MeterReadingRepository : IMeterReadingRepository
{
    private readonly MeterReadingsContext _meterReadingsContext;
    
    public MeterReadingRepository(MeterReadingsContext meterReadingsContext)
    {
        _meterReadingsContext = meterReadingsContext;
    }
    public async Task<MeterReading> FindCustomerAccountByAccountIdAsync(int accountId)
    {
        var meterReading =  await _meterReadingsContext.FindAsync<MeterReading>(accountId);
        return meterReading ?? new MeterReading { CustomerAccountId = 0 };
    }

    public async Task UpdateAndSaveChangesAsync(MeterReading meterReading)
    {
        _meterReadingsContext.Entry(meterReading).State = EntityState.Modified;
        _meterReadingsContext.MeterReadings.Update(meterReading);
        await _meterReadingsContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _meterReadingsContext.SaveChangesAsync();
    }
}

public interface IMeterReadingRepository
{
    public Task<MeterReading> FindCustomerAccountByAccountIdAsync(int accountId);
    
    public Task UpdateAndSaveChangesAsync(MeterReading meterReading);

    public Task SaveChangesAsync();
}