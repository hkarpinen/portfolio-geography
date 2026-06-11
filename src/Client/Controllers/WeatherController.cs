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
        var result = await weatherQuery.GetCurrentWeatherAsync(request.City, ct);
        return result is null
            ? Problem(detail: $"City '{request.City}' not found.", statusCode: StatusCodes.Status404NotFound)
            : Ok(result);
    }
}
