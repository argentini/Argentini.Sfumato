namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentEvenly : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-evenly";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: space-evenly;",
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