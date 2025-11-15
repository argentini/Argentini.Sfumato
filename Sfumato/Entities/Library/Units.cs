namespace Sfumato.Entities.Library;

public static class LibraryUnits
{
    public static HashSet<string> CssLengthUnits { get; } = new(StringComparer.Ordinal)
    {
        // Order here matters as truncating values like 'em' also work on values ending with 'rem'

        "rem", "vmin", "vmax",
        "cqw", "cqh", "cqi", "cqb", "cqmin", "cqmax", 
        "cm", "in", "mm", "pc", "pt", "px",
        "ch", "em", "ex", "vw", "vh", "Q", "%",
    };
    
    public static HashSet<string> CssAngleUnits { get; } = new(StringComparer.Ordinal)
    {
        // Order here matters as truncating values like 'rad' also work on values ending with 'grad'

        "grad", "turn", "deg", "rad"
    };

    public static HashSet<string> CssDurationUnits { get; } = new(StringComparer.Ordinal)
    {
        // Order here matters as truncating values like 's' also work on values ending with 'ms'
		
        "ms", "s"
    };

    public static HashSet<string> CssFrequencyUnits { get; } = new(StringComparer.Ordinal)
    {
        // Order here matters as truncating values like 'Hz' also work on values ending with 'kHz'
		
        "kHz", "Hz"
    };

    public static HashSet<string> CssResolutionUnits { get; } = new(StringComparer.Ordinal)
    {
        // Order here matters as truncating values like 'x' also work on values ending with 'dppx'
		
        "dpcm", "dppx", "dpi", "x"
    };
}