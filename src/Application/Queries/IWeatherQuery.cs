using Geography.Application.Dtos;

namespace Geography.Application.Queries;

public interface IWeatherQuery
{
    Task<WeatherDto?> GetCurrentWeatherAsync(string city, CancellationToken ct = default);

    /// <summary>The same reading, found by position rather than by name — what the browser can
    /// answer without the reader knowing what their nearest reporting station is called.</summary>
    Task<WeatherDto?> GetCurrentWeatherAtAsync(double latitude, double longitude, CancellationToken ct = default);
}
