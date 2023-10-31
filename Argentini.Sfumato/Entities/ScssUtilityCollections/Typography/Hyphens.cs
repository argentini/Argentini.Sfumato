namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Hyphens : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "hyphens";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "hyphens: none;",
        ["manual"] = "hyphens: manual;",
        ["auto"] = "hyphens: auto;",
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