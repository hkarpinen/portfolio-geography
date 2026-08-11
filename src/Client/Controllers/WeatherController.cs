using Geography.Application.Dtos;
using Geography.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[ApiController]
[Route("api/geography/weather")]
[AllowAnonymous]
public sealed class WeatherController(IWeatherQuery weatherQuery) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetWeatherRequest request, CancellationToken ct)
    {
        if (request.HasCoordinates)
        {
            if (!request.CoordinatesAreOnEarth)
                return Problem(detail: "That is not a place on Earth.", statusCode: StatusCodes.Status400BadRequest);

            var atPoint = await weatherQuery.GetCurrentWeatherAtAsync(request.Lat!.Value, request.Lon!.Value, ct);
            return atPoint is null
                ? Problem(detail: "No reading for that spot.", statusCode: StatusCodes.Status404NotFound)
                : Ok(atPoint);
        }

        if (string.IsNullOrWhiteSpace(request.City))
            return Problem(detail: "Give a city, or a lat and lon.", statusCode: StatusCodes.Status400BadRequest);

        var result = await weatherQuery.GetCurrentWeatherAsync(request.City, ct);
        return result is null
            ? Problem(detail: $"City '{request.City}' not found.", statusCode: StatusCodes.Status404NotFound)
            : Ok(result);
    }
}
