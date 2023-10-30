namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentStretch : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-stretch";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: stretch;"
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