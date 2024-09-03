using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ensek.DataLayer.ConsumerMeterDataService;

public class DatabaseDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MeterReadingsContext>
{
    public MeterReadingsContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MeterReadingsContext>();
        builder.UseSqlite();
        return new MeterReadingsContext(builder.Options);
    }
}