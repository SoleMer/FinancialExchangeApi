namespace FinancialExchangeApi.Services;

using FinancialExchangeApi.Models;

public interface ICurrencyExchangeService
{
    Task<ExchangeRateApiResponse?> GetExchangeRatesAsync(string baseCurrency);
    Task<ConversionResult?> ConvertCurrencyAsync(string from, string to, decimal amount);
}