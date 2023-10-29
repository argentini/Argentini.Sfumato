namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Snap : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "snap";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["start"] = "scroll-snap-align: start;",
        ["end"] = "scroll-snap-align: end;",
        ["center"] = "scroll-snap-align: center;",
        ["align-none"] = "scroll-snap-align: none;",
        ["normal"] = "scroll-snap-stop: normal;",
        ["always"] = "scroll-snap-stop: always;",
        ["none"] = "scroll-snap-type: none;",
        ["x"] = "scroll-snap-type: x var(--sf-scroll-snap-strictness);",
        ["y"] = "scroll-snap-type: y var(--sf-scroll-snap-strictness);",
        ["both"] = "scroll-snap-type: both var(--sf-scroll-snap-strictness);",
        ["mandatory"] = "--sf-scroll-snap-strictness: mandatory;",
        ["proximity"] = "--sf-scroll-snap-strictness: proximity;"
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