namespace Geography.Application.Queries;

public interface ITileProxy
{
    /// <summary>
    /// Fetches a map tile from the upstream provider and returns the raw bytes
    /// with its content-type, or null if the layer is unknown.
    /// </summary>
    Task<TileResult?> GetTileAsync(string layer, int z, int x, int y, CancellationToken ct = default);
}

public sealed record TileResult(byte[] Data, string ContentType);
