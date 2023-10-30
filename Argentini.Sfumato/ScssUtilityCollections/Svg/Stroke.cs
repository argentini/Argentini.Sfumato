namespace Argentini.Sfumato.ScssUtilityCollections.Svg;

public class Stroke : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "stroke";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "stroke: none;",
        ["0"] = "stroke-width: 0;",
        ["1"] = "stroke-width: 1;",
        ["2"] = "stroke-width: 2;",
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.ColorOptions);
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "stroke: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "stroke: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues("integer,length,percentage,number", cssSelector, "stroke-width: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}