namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Decoration : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "decoration";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["solid"] = "text-decoration-style: solid;",
        ["double"] = "text-decoration-style: double;",
        ["dotted"] = "text-decoration-style: dotted;",
        ["dashed"] = "text-decoration-style: dashed;",
        ["wavy"] = "text-decoration-style: wavy;",
        ["auto"] = "text-decoration-thickness: auto;",
        ["from-font"] = "text-decoration-thickness: from-font;",
        ["0"] = "text-decoration-thickness: 0px;",
        ["1"] = "text-decoration-thickness: 1px;",
        ["2"] = $"text-decoration-thickness: {2.PxToRem()};",
        ["4"] = $"text-decoration-thickness: {4.PxToRem()};",
        ["8"] = $"text-decoration-thickness: {8.PxToRem()};"
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

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "text-decoration-color: {value};", out Result))
            return Result;
        
        #endregion

        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "text-decoration-color: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "text-decoration-color: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues("length", cssSelector, "text-decoration-thickness: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}