namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class JustifyItems : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "justify-items";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["start"] = "justify-items: start;",
        ["end"] = "justify-items: end;",
        ["center"] = "justify-items: center;",
        ["stretch"] = "justify-items: stretch;"
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