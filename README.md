# portfolio-geography

Stateless geography service. Handles all location and physical-world concerns: current weather today, with a natural home for geocoding, timezone lookup, distance calculation, or elevation data in the future.

Requires auth — all endpoints proxy external APIs that use a server-side API key.

## What it does

- **Weather** — current conditions for any city via OpenWeatherMap (temperature, feels-like, humidity, pressure, wind speed, description, icon code).

## Stack

- .NET 8 / ASP.NET Core Web API
- OpenWeatherMap API (external HTTP via typed `HttpClient`)
- Clean Architecture: Domain → Application → Infrastructure → Client
- No database, no RabbitMQ, no EF Core

## Running locally

```bash
# From repo root
dotnet run --project src/Client
```

Or via the full stack:

```bash
docker compose -f infra/compose.dev.yaml up geography
```

## Structure

```
src/
  Domain/          Value objects (WeatherLocation) — no aggregates
  Application/     Query interface (IWeatherQuery), DTOs
  Infrastructure/  OpenWeatherMapClient (typed HttpClient)
  Client/          ASP.NET Core controllers, FluentValidation validators, DI wiring
```

## API surface

| Controller | Routes | Auth | Purpose |
|---|---|---|---|
| `WeatherController` | `GET /api/geography/weather?city=Helsinki` | Required | Current weather via OpenWeatherMap |

## Environment variables

| Variable | Description |
|---|---|
| `Jwt__Secret` | JWT signing key (≥ 32 chars, shared with identity service) |
| `OpenWeatherMap__ApiKey` | OpenWeatherMap API key |
| `OpenWeatherMap__BaseUrl` | Base URL (default: `https://api.openweathermap.org/data/2.5`) |
