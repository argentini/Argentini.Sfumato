namespace Argentini.Sfumato.ScssUtilityCollections.Tables;

public class TableFixed : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-fixed";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "table-layout: fixed;",
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        return string.Empty;
    }
}