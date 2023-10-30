namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Lowercase : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "lowercase";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-transform: lowercase;",
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