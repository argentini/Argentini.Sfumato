namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class BoxDecoration : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "box-decoration";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["clone"] = "box-decoration-break: clone;",
        ["slice"] = "box-decoration-break: slice;",
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