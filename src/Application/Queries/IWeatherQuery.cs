using Geography.Application.Dtos;

namespace Geography.Application.Queries;

public interface IWeatherQuery
{
    Task<WeatherDto?> GetCurrentWeatherAsync(string city, CancellationToken ct = default);
}
