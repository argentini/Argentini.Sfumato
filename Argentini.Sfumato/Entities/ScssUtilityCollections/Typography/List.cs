namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class List : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "list";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["inside"] = "list-style-position: inside;",
        ["outside"] = "list-style-position: outside;",
        
        ["none"] = "list-style-type: none;",
        ["disc"] = "list-style-type: disc;",
        ["decimal"] = "list-style-type: decimal;",
        
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
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "list-style-type: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}