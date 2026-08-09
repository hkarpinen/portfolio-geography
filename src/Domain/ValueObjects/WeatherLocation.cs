namespace Geography.Domain.ValueObjects;

public readonly record struct WeatherLocation(string City)
{
    public static WeatherLocation Create(string city) => new(city.Trim());

    public override string ToString() => City;
}
