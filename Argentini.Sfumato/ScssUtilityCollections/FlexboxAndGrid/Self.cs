namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Self : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "self";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "align-self: auto;",
        ["start"] = "align-self: flex-start;",
        ["end"] = "align-self: flex-end;",
        ["center"] = "align-self: center;",
        ["baseline"] = "align-self: baseline;",
        ["stretch"] = "align-self: stretch;"
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