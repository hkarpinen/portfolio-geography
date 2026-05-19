using System.Net.Http.Json;
using Geography.Application.Dtos;
using Geography.Application.Queries;
using Microsoft.Extensions.Options;

namespace Geography.Infrastructure.Http;

internal sealed class OpenWeatherMapClient(
    HttpClient http,
    IOptions<OpenWeatherMapOptions> options) : IWeatherQuery
{
    public async Task<WeatherDto?> GetCurrentWeatherAsync(string city, CancellationToken ct = default)
    {
        var opts = options.Value;

        // OWM q parameter uses comma as a separator: {city},{state},{country}
        // Each component must be individually encoded — the commas must stay literal.
        var parts = city.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(Uri.EscapeDataString)
                        .ToList();

        // Append country code if not already present so state disambiguation works.
        if (parts.Count > 0 && !parts.Last().Equals("US", StringComparison.OrdinalIgnoreCase))
            parts.Add("US");

        var q = string.Join(",", parts);
        var url = $"{opts.BaseUrl}/weather?q={q}&appid={opts.ApiKey}&units=metric";

        var response = await http.GetAsync(url, ct);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<OWMCurrentWeatherResponse>(ct);
        if (data is null) return null;

        return new WeatherDto(
            City: data.Name,
            Country: data.Sys.Country,
            TemperatureCelsius: data.Main.Temp,
            FeelsLikeCelsius: data.Main.FeelsLike,
            Humidity: data.Main.Humidity,
            Pressure: data.Main.Pressure,
            Description: data.Weather.FirstOrDefault()?.Description ?? string.Empty,
            IconCode: data.Weather.FirstOrDefault()?.Icon ?? string.Empty,
            WindSpeedMs: data.Wind.Speed,
            VisibilityMeters: data.Visibility,
            Latitude: data.Coord.Lat,
            Longitude: data.Coord.Lon);
    }
}
