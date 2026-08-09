using System.Text.Json.Serialization;

namespace Geography.Infrastructure.Http;

internal sealed class OWMCurrentWeatherResponse
{
    [JsonPropertyName("name")]       public string Name { get; set; } = string.Empty;
    [JsonPropertyName("sys")]        public OWMSys Sys { get; set; } = new();
    [JsonPropertyName("main")]       public OWMMain Main { get; set; } = new();
    [JsonPropertyName("weather")]    public OWMWeather[] Weather { get; set; } = [];
    [JsonPropertyName("wind")]       public OWMWind Wind { get; set; } = new();
    [JsonPropertyName("visibility")] public int Visibility { get; set; }
    [JsonPropertyName("coord")]      public OWMCoord Coord { get; set; } = new();
    /// <summary>Seconds from UTC at the looked-up city, not the server.</summary>
    [JsonPropertyName("timezone")]   public int Timezone { get; set; }
}

internal sealed class OWMCoord
{
    [JsonPropertyName("lat")] public double Lat { get; set; }
    [JsonPropertyName("lon")] public double Lon { get; set; }
}

internal sealed class OWMSys
{
    [JsonPropertyName("country")] public string Country { get; set; } = string.Empty;
    /// <summary>Unix seconds, UTC.</summary>
    [JsonPropertyName("sunset")]  public long Sunset { get; set; }
}

internal sealed class OWMMain
{
    [JsonPropertyName("temp")]       public double Temp { get; set; }
    [JsonPropertyName("feels_like")] public double FeelsLike { get; set; }
    [JsonPropertyName("humidity")]   public int Humidity { get; set; }
    [JsonPropertyName("pressure")]   public int Pressure { get; set; }
    [JsonPropertyName("temp_min")]   public double TempMin { get; set; }
    [JsonPropertyName("temp_max")]   public double TempMax { get; set; }
}

internal sealed class OWMWeather
{
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
    [JsonPropertyName("icon")]        public string Icon { get; set; } = string.Empty;
}

internal sealed class OWMWind
{
    [JsonPropertyName("speed")] public double Speed { get; set; }
    /// <summary>Meteorological degrees, so 0 is north and it reads clockwise.</summary>
    [JsonPropertyName("deg")]   public int Deg { get; set; }
}
