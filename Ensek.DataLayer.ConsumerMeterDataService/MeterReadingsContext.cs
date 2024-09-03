using Ensek.Models.ConsumerMeterDataService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ensek.DataLayer.ConsumerMeterDataService;

public class MeterReadingsContext : DbContext
{
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    
    public DbSet<MeterReading> MeterReadings { get; set; }
    
    public MeterReadingsContext(DbContextOptions<MeterReadingsContext> options)
        : base(options) { }
}