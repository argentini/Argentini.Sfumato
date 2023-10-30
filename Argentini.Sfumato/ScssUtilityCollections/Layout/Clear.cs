namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Clear : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "clear";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["right"] = "clear: right;",
        ["left"] = "clear: left;",
        ["both"] = "clear: both;",
        ["none"] = "clear: none;",
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
        
        return string.Empty;
    }
}