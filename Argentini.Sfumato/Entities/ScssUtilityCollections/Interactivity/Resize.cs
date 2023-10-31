namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Interactivity;

public class Resize : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "resize";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "resize: both;",
        ["none"] = "resize: none;",
        ["y"] = "resize: vertical;",
        ["x"] = "resize: horizontal;"
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