namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class ListImage : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "list-image";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "list-style-image: none;",
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
        
        if (ProcessArbitraryValues("url", cssSelector, "list-style-image: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}