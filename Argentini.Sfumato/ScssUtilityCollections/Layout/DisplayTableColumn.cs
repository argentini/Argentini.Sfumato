namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayTableColumn : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-column";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: table-column;"
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