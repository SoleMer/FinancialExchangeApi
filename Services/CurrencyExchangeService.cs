namespace FinancialExchangeApi.Services;

using FinancialExchangeApi.Configuration;
using FinancialExchangeApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;

public class CurrencyExchangeService : ICurrencyExchangeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CurrencyExchangeService> _logger;
    private readonly ExchangeRateApiOptions _options;

    public CurrencyExchangeService(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache,
        ILogger<CurrencyExchangeService> logger, 
        IOptions<ExchangeRateApiOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _logger = logger;
        _options = options.Value;
    }
    
    public async Task<ExchangeRateApiResponse?> GetExchangeRatesAsync(string baseCurrency)
    {
        string cacheKey = $"rates_{baseCurrency}";
        if (_cache.TryGetValue(cacheKey, out ExchangeRateApiResponse? cachedRates))
        {
            _logger.LogInformation("Serving exchange rates for {Currency} from Memory Cache.", baseCurrency);
            return cachedRates;
        }
        _logger.LogInformation("Cache miss. Fetching exchange rates for {Currency} from external API...", baseCurrency);
        
        string apiKey = _options.ApiKey;
        string baseUrl = _options.BaseUrl;
        string url = $"{baseUrl}{apiKey}/latest/{baseCurrency}";

        var client = _httpClientFactory.CreateClient();
        
        try
        {
            var response = await client.GetFromJsonAsync<ExchangeRateApiResponse>(url);

            if (response is not null && response.Result == "success")
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(_options.CacheExpirationMinutes));

                _cache.Set(cacheKey, response, cacheOptions);
                return response;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching rates from external API.");
        }

        return null;
    }

    public async Task<ConversionResult?> ConvertCurrencyAsync(string from, string to, decimal amount)
    {
        var ratesResponse = await GetExchangeRatesAsync(from);

        if (ratesResponse is null || !ratesResponse.ConversionRates.ContainsKey(to))
        {
            return null;
        }

        decimal rate = ratesResponse.ConversionRates[to];
        decimal convertedAmount = amount * rate;
        bool wasCached = _cache.TryGetValue($"rates_{from}", out _);

        return new ConversionResult(
            From: from,
            To: to,
            Amount: amount,
            ConvertedAmount: Math.Round(convertedAmount, _options.DecimalPlaces),
            ExchangeRate: rate,
            ServedFromCache: wasCached,
            Timestamp: DateTime.UtcNow
        );
    }
}