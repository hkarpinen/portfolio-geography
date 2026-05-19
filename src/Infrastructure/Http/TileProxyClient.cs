using Geography.Application.Queries;
using Microsoft.Extensions.Options;

namespace Geography.Infrastructure.Http;

internal sealed class TileProxyClient(
    HttpClient http,
    IOptions<OpenWeatherMapOptions> options) : ITileProxy
{
    private static readonly Dictionary<string, string> OwmLayers = new(StringComparer.OrdinalIgnoreCase)
    {
        ["clouds"]        = "clouds_new",
        ["precipitation"] = "precipitation_new",
        ["pressure"]      = "pressure_new",
        ["wind"]          = "wind_new",
        ["temp"]          = "temp_new",
    };

    public async Task<TileResult?> GetTileAsync(string layer, int z, int x, int y, CancellationToken ct = default)
    {
        string url;

        if (layer.Equals("cycling", StringComparison.OrdinalIgnoreCase))
        {
            // CyclOSM — no API key required
            url = $"https://a.tile-cyclosm.openstreetmap.fr/cyclosm/{z}/{x}/{y}.png";
        }
        else if (OwmLayers.TryGetValue(layer, out var owmLayer))
        {
            var apiKey = options.Value.ApiKey;
            url = $"https://tile.openweathermap.org/map/{owmLayer}/{z}/{x}/{y}.png?appid={apiKey}";
        }
        else
        {
            return null;
        }

        var response = await http.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode) return null;

        var data = await response.Content.ReadAsByteArrayAsync(ct);
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "image/png";
        return new TileResult(data, contentType);
    }
}
