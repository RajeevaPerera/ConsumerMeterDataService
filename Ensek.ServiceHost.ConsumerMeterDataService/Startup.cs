using Ensek.BusinessLayer.ConsumerMeterDataService;
using Ensek.Controllers.ConsumerMeterDataService;
using Ensek.DataLayer.ConsumerMeterDataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Ensek.ServiceHost.ConsumerMeterDataService;

public class Startup
{
    protected readonly IConfiguration _configuration;
    
    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllerModules();
        services.AddBusinessLayerModules();
        services.AddDataLayerModules();
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("Ensek.Controllers.ConsumerMeterDataService", new OpenApiInfo { Title = "Ensek.Controllers.ConsumerMeterDataService", Version = "v1" });
        });
        
        services.AddDbContext<MeterReadingsContext>(options =>
        {
            var dbPath = $"{Environment.CurrentDirectory}{System.IO.Path.DirectorySeparatorChar}ConsumerMeterData.db";
            options.UseSqlite($"Data Source={dbPath}");
            //options.UseSqlite($"Data Source=ConsumerMeterData.db");
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ensek.Controllers.ConsumerMeterDataService");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        app.UseStaticFiles();
        
    }
}