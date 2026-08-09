namespace Geography.Application.Queries;

public interface ITileProxy
{
    /// <summary>Null when the layer is unknown.</summary>
    Task<TileResult?> GetTileAsync(string layer, int z, int x, int y, CancellationToken ct = default);
}

public sealed record TileResult(byte[] Data, string ContentType);
