namespace Geography.Infrastructure.Http;

public sealed class OpenWeatherMapOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://api.openweathermap.org/data/2.5";
}
