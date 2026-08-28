# 💱 Financial Exchange & Currency Caching API (.NET 10 / ASP.NET Core)

A high-performance, resilient RESTful Web API built with **.NET 10** and **ASP.NET Core Minimal APIs**. This project integrates with external financial rate providers and implements an **In-Memory Caching strategy** to optimize response times, reduce external API consumption, and handle currency conversions.

> 💡 **Architectural Note:** Developed as a practical demonstration of rapid technical adaptation, mapping enterprise **Java / Spring Boot** architecture patterns (Dependency Injection, Resilience, Caching) directly to modern **.NET**.

---

## 🚀 Key Features

* **Real-time Currency Exchange Rates:** Fetches live foreign exchange rates for base currencies (USD, EUR, ARS, etc.).
* **Currency Conversion Engine:** Performs real-time conversions between currency pairs with validation.
* **In-Memory Caching (`IMemoryCache`):** Caches exchange rate responses for 30 minutes to reduce external HTTP requests and maximize throughput.
* **Resilient HTTP Client:** Uses `IHttpClientFactory` for efficient socket management and external API calls.
* **OpenAPI Documentation:** Built-in Interactive API documentation powered by OpenAPI.

---

## 🛠️ Tech Stack & Architecture

* **Framework:** .NET 10 / ASP.NET Core (Minimal APIs)
* **Language:** C# 12 / 13
* **External API:** [ExchangeRate-API](https://www.exchangerate-api.com/)
* **Caching:** Native .NET `IMemoryCache`
* **Configuration:** `appsettings.json` with Environment Overrides

---

## 📋 API Endpoints

### 1. Get Exchange Rates
Returns live exchange rates for a given base currency. Subsequent requests within the expiration window are served directly from cache.

* **HTTP Method:** `GET`
* **Route:** `/api/rates/{baseCurrency}`
* **Example:** `/api/rates/USD`

**Response (`200 OK`):**
```json
{
  "result": "success",
  "base_code": "USD",
  "conversion_rates": {
    "ARS": 1350.50,
    "EUR": 0.92,
    "BRL": 5.45
  }
}