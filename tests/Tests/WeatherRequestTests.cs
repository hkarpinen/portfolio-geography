using Geography.Application.Dtos;

namespace Tests;

public class WeatherRequestTests
{
    [Fact]
    public void HasCoordinates_NeedsBoth()
    {
        Assert.False(new GetWeatherRequest(null, 46.7, null).HasCoordinates);
        Assert.False(new GetWeatherRequest(null, null, -117.2).HasCoordinates);
        Assert.True(new GetWeatherRequest(null, 46.7, -117.2).HasCoordinates);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(90, 180)]
    [InlineData(-90, -180)]
    [InlineData(46.73, -117.17)]
    public void CoordinatesAreOnEarth_AcceptsRealPlaces(double lat, double lon) =>
        Assert.True(new GetWeatherRequest(null, lat, lon).CoordinatesAreOnEarth);

    [Theory]
    [InlineData(91, 0)]
    [InlineData(-91, 0)]
    [InlineData(0, 181)]
    [InlineData(0, -181)]
    public void CoordinatesAreOnEarth_RejectsTheRest(double lat, double lon) =>
        Assert.False(new GetWeatherRequest(null, lat, lon).CoordinatesAreOnEarth);
}
