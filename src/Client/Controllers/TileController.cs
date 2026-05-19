using Geography.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[ApiController]
[Route("api/geography/tiles")]
[AllowAnonymous]
public sealed class TileController(ITileProxy tileProxy) : ControllerBase
{
    // GET /api/geography/tiles/{layer}/{z}/{x}/{y}.png
    [HttpGet("{layer}/{z:int}/{x:int}/{y:int}.png")]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> GetTile(
        string layer, int z, int x, int y, CancellationToken ct)
    {
        if (z < 0 || z > 20 || x < 0 || y < 0)
            return BadRequest(new { error = "Invalid tile coordinates." });

        var result = await tileProxy.GetTileAsync(layer, z, x, y, ct);
        return result is null
            ? NotFound(new { error = $"Unknown layer '{layer}'." })
            : File(result.Data, result.ContentType);
    }
}
