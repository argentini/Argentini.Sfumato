namespace Argentini.Sfumato.ScssUtilityCollections.Backgrounds;

public class Bg : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "bg";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["repeat"] = "background-repeat: repeat;",
        ["no-repeat"] = "background-repeat: no-repeat;",
        ["repeat-x"] = "background-repeat: border-box;",
        ["repeat-y"] = "background-repeat: padding-box;",
        ["repeat-round"] = "background-repeat: round;",
        ["repeat-space"] = "background-repeat: space;",
        ["origin-border"] = "background-origin: border-box;",
        ["origin-padding"] = "background-origin: padding-box;",
        ["origin-content"] = "background-origin: content-box;",
        ["fixed"] = "background-attachment: fixed;",
        ["local"] = "background-attachment: local;",
        ["scroll"] = "background-attachment: scroll;",
        ["border"] = "background-clip: border-box;",
        ["padding"] = "background-clip: padding-box;",
        ["content"] = "background-clip: content-box;",
        ["text"] = "background-clip: text;",
        ["bottom"] = "background-position: bottom;",
        ["center"] = "background-position: center;",
        ["left"] = "background-position: left;",
        ["left-bottom"] = "background-position: left bottom;",
        ["left-top"] = "background-position: left top;",
        ["right"] = "background-position: right;",
        ["right-bottom"] = "background-position: right bottom;",
        ["right-top"] = "background-position: right top;",
        ["top"] = "background-position: top;",
        ["auto"] = "background-size: auto;",
        ["cover"] = "background-size: cover;",
        ["contain"] = "background-size: contain;",
        ["none"] = "background-image: none;",
        ["gradient-to-t"] = "background-image: linear-gradient(to top, var(--sf-gradient-stops));",
        ["gradient-to-tr"] = "background-image: linear-gradient(to top right, var(--sf-gradient-stops));",
        ["gradient-to-r"] = "background-image: linear-gradient(to right, var(--sf-gradient-stops));",
        ["gradient-to-br"] = "background-image: linear-gradient(to bottom right, var(--sf-gradient-stops));",
        ["gradient-to-b"] = "background-image: linear-gradient(to bottom, var(--sf-gradient-stops));",
        ["gradient-to-bl"] = "background-image: linear-gradient(to bottom left, var(--sf-gradient-stops));",
        ["gradient-to-l"] = "background-image: linear-gradient(to left, var(--sf-gradient-stops));",
        ["gradient-to-tl"] = "background-image: linear-gradient(to top left, var(--sf-gradient-stops));"
    };

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
        
        foreach (var corePrefix in appState.ColorOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "background-color: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "background-color: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "background-color: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "background-size: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues("url", cssSelector, "background-image: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "background-position: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}