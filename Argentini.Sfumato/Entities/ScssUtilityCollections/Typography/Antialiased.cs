namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Antialiased : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "antialiased";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = """
               -webkit-font-smoothing: antialiased;
               -moz-osx-font-smoothing: grayscale;
               """,
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