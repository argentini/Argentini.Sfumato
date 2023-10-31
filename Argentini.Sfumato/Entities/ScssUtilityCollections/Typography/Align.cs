namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Align : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "align";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["baseline"] = "vertical-align: baseline;",
        ["top"] = "vertical-align: top;",
        ["middle"] = "vertical-align: middle;",
        ["bottom"] = "vertical-align: bottom;",
        ["text-top"] = "vertical-align: text-top;",
        ["text-bottom"] = "vertical-align: text-bottom;",
        ["sub"] = "vertical-align: sub;",
        ["super"] = "vertical-align: super;"
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length", cssSelector, "vertical-align: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}