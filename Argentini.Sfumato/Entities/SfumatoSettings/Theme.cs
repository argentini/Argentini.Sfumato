namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class Theme
{
    public MediaBreakpoints MediaBreakpoints { get; set; } = new();
    public FontSizeUnits FontSizeUnits { get; set; } = new();
    public Dictionary<string,string> Colors { get; set; } = new();
    public Dictionary<string,string> Animation { get; set; } = new();
    public Dictionary<string,string> AspectRatios { get; set; } = new();
}
