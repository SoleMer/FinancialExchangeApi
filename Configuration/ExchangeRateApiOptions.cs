namespace FinancialExchangeApi.Configuration;

public class ExchangeRateApiOptions
{
    public const string SectionName = "ExchangeRateApi";
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public int CacheExpirationMinutes { get; set; } = 30;
    public int DecimalPlaces { get; set; } = 2;
}