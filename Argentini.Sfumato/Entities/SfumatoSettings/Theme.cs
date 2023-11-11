namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class Theme
{
    public MediaBreakpoints? MediaBreakpoint { get; set; } = new();
    public FontSizeUnits? FontSizeUnit { get; set; } = new();
    
    #region Value Options
    
    public Dictionary<string,string>? Color { get; set; } = new();
    public Dictionary<string,string>? BorderWidth { get; set; } = new();
    public Dictionary<string,string>? BorderRadius { get; set; } = new();
    public Dictionary<string,string>? FilterSize { get; set; } = new();
    
    #endregion
    
    #region Background
    
    public Dictionary<string,string>? BackgroundImage { get; set; } = new();
    public Dictionary<string,string>? BackgroundPosition { get; set; } = new();
    public Dictionary<string,string>? BackgroundSize { get; set; } = new();
    public Dictionary<string,string>? FromPosition { get; set; } = new();
    public Dictionary<string,string>? ToPosition { get; set; } = new();
    public Dictionary<string,string>? ViaPosition { get; set; } = new();

    #endregion

    #region Effects

    public Dictionary<string,string>? BoxShadow { get; set; } = new();

    #endregion

    #region Filters

    public Dictionary<string,string>? BackdropGrayscale { get; set; } = new();
    public Dictionary<string,string>? Grayscale { get; set; } = new();
    public Dictionary<string,string>? BackdropHueRotate { get; set; } = new();
    public Dictionary<string,string>? HueRotate { get; set; } = new();
    public Dictionary<string,string>? BackdropInvert { get; set; } = new();
    public Dictionary<string,string>? Invert { get; set; } = new();
    public Dictionary<string,string>? BackdropSepia { get; set; } = new();
    public Dictionary<string,string>? Sepia { get; set; } = new();

    #endregion
    
    #region Layout

    public Dictionary<string,string>? AspectRatio { get; set; } = new();

    #endregion
    
    #region Transforms

    public Dictionary<string,string>? Animate { get; set; } = new();
    
    #endregion
}
