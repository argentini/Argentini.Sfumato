namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Overline : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overline";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-decoration-line: overline;",
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