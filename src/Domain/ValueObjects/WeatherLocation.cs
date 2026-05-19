namespace Geography.Domain.ValueObjects;

/// <summary>
/// Represents a city name used to look up geographic or weather data.
/// </summary>
public readonly record struct WeatherLocation(string City)
{
    public static WeatherLocation Create(string city) => new(city.Trim());

    public override string ToString() => City;
}
