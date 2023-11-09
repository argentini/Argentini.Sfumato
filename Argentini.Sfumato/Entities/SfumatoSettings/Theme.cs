// ReSharper disable CollectionNeverUpdated.Global
namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class Theme
{
    public MediaBreakpoints? MediaBreakpoint { get; set; } = new();
    public FontSizeUnits? FontSizeUnit { get; set; } = new();
    public Dictionary<string,string>? Color { get; set; } = new();
    public Dictionary<string,string>? Animation { get; set; } = new();
    public Dictionary<string,string>? AspectRatio { get; set; } = new();
    public Dictionary<string,string>? BackgroundImage { get; set; } = new();
    public Dictionary<string,string>? BackgroundPosition { get; set; } = new();
    public Dictionary<string,string>? BackgroundSize { get; set; } = new();
}
