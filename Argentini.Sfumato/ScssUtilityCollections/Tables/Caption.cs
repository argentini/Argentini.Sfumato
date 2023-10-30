namespace Argentini.Sfumato.ScssUtilityCollections.Tables;

public class Caption : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "caption";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["top"] = "caption-side: top;",
        ["bottom"] = "caption-side: bottom;",
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