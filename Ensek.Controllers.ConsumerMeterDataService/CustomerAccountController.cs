using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ensek.Controllers.ConsumerMeterDataService;

[ApiController]
public class CustomerAccountController : ServiceControllerBase<CustomerAccountController>
{
    private readonly ILogger<CustomerAccountController> _logger;
    
    public CustomerAccountController(ILogger<CustomerAccountController> logger) : base(logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Route("api/[controller]/customeraccount")]
    public async Task<IActionResult> GetCustomerAccountsAsync()
    {
        return Ok();
    }
}