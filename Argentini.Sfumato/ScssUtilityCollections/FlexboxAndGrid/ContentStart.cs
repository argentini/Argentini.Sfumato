namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentStart : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-start";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: flex-start;",
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
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