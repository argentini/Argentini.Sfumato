namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayTableCell : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-cell";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: table-cell;"
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