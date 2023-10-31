namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class Items : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "items";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["start"] = "align-items: flex-start;",
        ["end"] = "align-items: flex-end;",
        ["center"] = "align-items: center;",
        ["baseline"] = "align-items: baseline;",
        ["stretch"] = "align-items: stretch;"
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