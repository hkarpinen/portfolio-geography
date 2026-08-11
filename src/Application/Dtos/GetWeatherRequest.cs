namespace Geography.Application.Dtos;

/// <summary>
/// Two ways to say where: a name, or a position the browser handed over. Exactly one is
/// required — a request carrying neither has not asked a question.
/// </summary>
public sealed record GetWeatherRequest(string? City, double? Lat, double? Lon)
{
    public bool HasCoordinates => Lat is not null && Lon is not null;

    public bool CoordinatesAreOnEarth =>
        Lat is >= -90 and <= 90 && Lon is >= -180 and <= 180;
}
