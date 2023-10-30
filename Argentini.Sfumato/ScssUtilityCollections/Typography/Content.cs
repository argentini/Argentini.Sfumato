namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Content : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "content: none;",
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
        
        if (ProcessArbitraryValues("string", cssSelector, "content: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}