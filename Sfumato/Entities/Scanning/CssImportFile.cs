namespace Sfumato.Entities.Scanning;

public sealed class CssImportFile : IDisposable
{
    public required ObjectPool<StringBuilder>? Pool { get; set; }
    public required StringBuilder? CssContent { get; set; }
    public required FileInfo FileInfo { get; set; } = null!;
    public bool Touched { get; set; }
    
    public void Dispose()
    {
        if (CssContent is not null && Pool is not null)
            Pool.Return(CssContent);
    }
}