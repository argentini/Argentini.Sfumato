namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Grow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grow";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "integer" or "number")
            return $"flex-grow: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}