namespace FinancialExchangeApi.Models;

public record ConversionResult(
    string From,
    string To,
    decimal Amount,
    decimal ConvertedAmount,
    decimal ExchangeRate,
    bool ServedFromCache,
    DateTime Timestamp
);