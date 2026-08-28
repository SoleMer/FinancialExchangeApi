# 🔄 Java / Spring Boot to .NET Cheat Sheet & Architectural Equivalences

This document serves as a quick architectural mapping between **Java (Spring Boot)** and **.NET 10 (ASP.NET Core)**. It demonstrates how core enterprise backend concepts, annotations, and design patterns translate between both ecosystems.

---

## 🛠️ Project Configuration & Build Tools

| Feature / Tool | Java / Spring Boot | .NET (ASP.NET Core) | Description |
| :--- | :--- | :--- | :--- |
| **Build File** | `pom.xml` / `build.gradle` | `.csproj` (XML) | Defines dependencies, target runtime, and metadata. |
| **Package Manager** | Maven Central / Gradle | NuGet | Package repository for external libraries. |
| **App Settings** | `application.properties` / `application.yml` | `appsettings.json` / `appsettings.Development.json` | Stores configuration variables, connections, and log levels. |
| **Build Artifacts** | `target/` or `build/` | `bin/` and `obj/` | Directories containing intermediate and final compiled binaries. |

---

## 🏗️ Core Architecture & Dependency Injection (IoC)

In .NET, service registration is configured inside `Program.cs` on the `WebApplicationBuilder`.

| Concept | Java / Spring Boot | .NET (ASP.NET Core) | Notes |
| :--- | :--- | :--- | :--- |
| **Singleton Scope** | `@Scope("singleton")` / Default `@Bean` | `builder.Services.AddSingleton<IService, Service>()` | Single instance shared across the entire app lifecycle. |
| **Scoped (Request) Scope** | `@Scope("request")` / Default `@Service` | `builder.Services.AddScoped<IService, Service>()` | New instance created per HTTP request lifecycle. |
| **Transient Scope** | `@Scope("prototype")` | `builder.Services.AddTransient<IService, Service>()` | New instance created every time it is injected. |
| **HTTP Client Factory** | `RestTemplate` / `WebClient` | `builder.Services.AddHttpClient()` | Factory pattern for resilient HTTP external service consumption. |

---

## 🚀 Web & API Frameworks

| Feature | Java / Spring Boot | .NET (ASP.NET Core) | Example / Syntax |
| :--- | :--- | :--- | :--- |
| **REST Controller** | `@RestController` | `[ApiController]` or **Minimal APIs** | Modern .NET favors lightweight Minimal APIs (`app.MapGet()`). |
| **GET Endpoint** | `@GetMapping("/api/resource")` | `app.MapGet("/api/resource", ...)` | Defines an HTTP GET endpoint route. |
| **POST Endpoint** | `@PostMapping("/api/resource")` | `app.MapPost("/api/resource", ...)` | Defines an HTTP POST endpoint route. |
| **Path Variable** | `@PathVariable("id") String id` | `/api/resource/{id}` (Route parameter) | Automatically bound by route name. |
| **Query Parameter** | `@RequestParam("query") String q` | Endpoint lambda parameter | Automatically bound from query string. |
| **JSON Serialization** | Jackson (`@JsonProperty`) | `System.Text.Json` (`[JsonPropertyName]`) | Native, high-performance JSON parsing. |

---

## ⚡ Caching & Performance Optimization

| Feature | Java / Spring Boot | .NET (ASP.NET Core) | Notes |
| :--- | :--- | :--- | :--- |
| **Enable In-Memory Caching** | `@EnableCaching` | `builder.Services.AddMemoryCache()` | Enables internal memory store for cached key-value pairs. |
| **Inject Cache Service** | `CacheManager` / `@Cacheable` | `IMemoryCache` (Injected via DI) | Programmatic control over cache retrieval, creation, and expiration. |
| **Distributed Cache** | Spring Data Redis (`@Cacheable`) | `builder.Services.AddStackExchangeRedisCache()` | Standardized interface for external cache stores like Redis. |

---

## 🔒 Async, Background Jobs & Concurrency

| Feature | Java / Spring Boot | .NET (ASP.NET Core) | Notes |
| :--- | :--- | :--- | :--- |
| **Async Execution** | `@Async` / `CompletableFuture<T>` | `async` / `await` (`Task<T>`) | Native language-level asynchronous programming syntax. |
| **Background Workers** | `@Scheduled` / Spring Batch | `IHostedService` / `BackgroundService` | Long-running asynchronous tasks executed in the background. |
| **Data Models** | Lombok `@Data` / Java `record` | C# `record` | Immutable data structures with value-based equality. |

---

## 🧪 Testing & DevOps Pipeline

| Feature | Java / Spring Boot | .NET (ASP.NET Core) |
| :--- | :--- | :--- |
| **Unit Testing Framework** | JUnit 5 / TestNG | xUnit / NUnit |
| **Mocking Library** | Mockito | Moq / NSubstitute |
| **Assertion Library** | AssertJ / Hamcrest | FluentAssertions / Built-in `Assert` |
| **CI/CD Pipeline** | GitHub Actions (`setup-java`) | GitHub Actions (`setup-dotnet`) |