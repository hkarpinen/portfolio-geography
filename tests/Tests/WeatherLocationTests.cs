using Geography.Domain.ValueObjects;

namespace Tests;

public class WeatherLocationTests
{
    [Fact]
    public void Create_TrimsLeadingAndTrailingWhitespace()
    {
        var location = WeatherLocation.Create("  London  ");

        Assert.Equal("London", location.City);
    }

    [Fact]
    public void Create_PreservesInternalSpaces()
    {
        var location = WeatherLocation.Create("New York");

        Assert.Equal("New York", location.City);
    }

    [Fact]
    public void ToString_ReturnsCityName()
    {
        var location = WeatherLocation.Create("Paris");

        Assert.Equal("Paris", location.ToString());
    }

    [Fact]
    public void Equality_SameCityIsEqual()
    {
        var a = WeatherLocation.Create("Tokyo");
        var b = WeatherLocation.Create("Tokyo");

        Assert.Equal(a, b);
    }

    [Fact]
    public void Equality_DifferentCitiesAreNotEqual()
    {
        var a = WeatherLocation.Create("Berlin");
        var b = WeatherLocation.Create("Vienna");

        Assert.NotEqual(a, b);
    }

    [Fact]
    public void Equality_WhitespaceTrimmedCitiesAreEqual()
    {
        var a = WeatherLocation.Create("  Rome  ");
        var b = WeatherLocation.Create("Rome");

        Assert.Equal(a, b);
    }
}
