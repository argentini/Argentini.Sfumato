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
        
        if (ProcessArbitraryValues("integer,number", cssSelector, "flex-grow: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}