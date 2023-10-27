namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Tracking : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "tracking";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["tighter"] = "letter-spacing: -0.05em;",
        ["tight"] = "letter-spacing: -0.025em;",
        ["normal"] = "letter-spacing: 0em;",
        ["wide"] = "letter-spacing: 0.025em;",
        ["wider"] = "letter-spacing: 0.05em;",
        ["widest"] = "letter-spacing: 0.1em;"
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "length")
            return $"letter-spacing: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}