namespace Geography.Application.Dtos;

public sealed record WeatherDto(
    string City,
    string Country,
    double TemperatureCelsius,
    double FeelsLikeCelsius,
    int Humidity,
    int Pressure,
    string Description,
    string IconCode,
    double WindSpeedMs,
    int VisibilityMeters,
    double Latitude,
    double Longitude);
