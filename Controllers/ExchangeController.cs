namespace FinancialExchangeApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using FinancialExchangeApi.Services;

[ApiController]
[Route("api")]
public class ExchangeController : ControllerBase
{
    private readonly ICurrencyExchangeService _service;

    public ExchangeController(ICurrencyExchangeService service)
    {
        _service = service;
    }

    [HttpGet("rates/{baseCurrency}")]
    public async Task<IActionResult> GetRates(string baseCurrency)
    {
        var rates = await _service.GetExchangeRatesAsync(baseCurrency.ToUpper());
        return rates is not null ? Ok(rates) : NotFound();
    }

    [HttpGet("convert/{from}/{to}/{amount:decimal}")]
    public async Task<IActionResult> Convert(string from, string to, decimal amount)
    {
        if (amount <= 0) return BadRequest("Amount must be greater than zero.");
        var result = await _service.ConvertCurrencyAsync(from.ToUpper(), to.ToUpper(), amount);
        return result is not null ? Ok(result) : NotFound("Conversion failed or currency not found.");
    }
    
}