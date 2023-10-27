namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Shrink : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "shrink";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "integer" or "number")
            return $"flex-shrink: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}