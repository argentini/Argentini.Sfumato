namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class Border : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["solid"] = "border-style: solid;",
        ["dashed"] = "border-style: dashed;",
        ["dotted"] = "border-style: dotted;",
        ["double"] = "border-style: double;",
        ["hidden"] = "border-style: hidden;",
        ["none"] = "border-style: none;"
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
        
        await AddToIndexAsync(appState.ColorOptions);
        
        await AddToIndexAsync(appState.BorderWidthOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;

        #endregion

        #region Modifier Utilities

        if (ProcessColorModifierOptions(cssSelector, "border-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "border-color: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "border-width: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "border-color: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "border-width: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "border-style: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}