using FinancialExchangeApi.Services;
using FinancialExchangeApi.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<ExchangeRateApiOptions>(
    builder.Configuration.GetSection(ExchangeRateApiOptions.SectionName));
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<ICurrencyExchangeService, CurrencyExchangeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) 
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
