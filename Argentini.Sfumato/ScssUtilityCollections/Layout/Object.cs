namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Object : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "object";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["contain"] = "object-fit: contain;",
        ["cover"] = "object-fit: cover;",
        ["fill"] = "object-fit: fill;",
        ["none"] = "object-fit: none;",
        ["scale-down"] = "object-fit: scale-down;",
        ["bottom"] = "object-position: bottom;",
        ["center"] = "object-position: center;",
        ["left"] = "object-position: left;",
        ["left-bottom"] = "object-position: left bottom;",
        ["left-top"] = "object-position: left top;",
        ["right"] = "object-position: right;",
        ["right-bottom"] = "object-position: right bottom;",
        ["right-top"] = "object-position: right top;",
        ["top"] = "object-position: top;"
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