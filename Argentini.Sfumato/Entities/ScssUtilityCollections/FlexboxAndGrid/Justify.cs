namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class Justify : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "justify";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["normal"] = "justify-content: normal;",
        ["start"] = "justify-content: flex-start;",
        ["end"] = "justify-content: flex-end;",
        ["center"] = "justify-content: center;",
        ["between"] = "justify-content: space-between;",
        ["around"] = "justify-content: space-around;",
        ["evenly"] = "justify-content: space-evenly;",
        ["stretch"] = "justify-content: stretch;"
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