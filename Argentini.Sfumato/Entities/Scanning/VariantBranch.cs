// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato.Entities.Scanning;

public sealed class VariantBranch
{
    public ulong Fingerprint { get; set; }
    public int Depth { get; set; }
    public string WrapperCss { get; set; } = string.Empty;
    public HashSet<VariantBranch> Branches { get; set; } = [];
    public HashSet<CssClass> CssClasses { get; set; } = [];

    public override bool Equals(object? obj) => obj is VariantBranch other && Fingerprint == other.Fingerprint;

    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => Fingerprint.GetHashCode();    
}