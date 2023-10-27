namespace Argentini.Sfumato.ScssUtilityCollections.Transforms;

public class Transition : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "transition";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = """
               transition-property: color, background-color, border-color, text-decoration-color, fill, stroke, opacity, box-shadow, transform, filter, backdrop-filter;
               transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
               transition-duration: 150ms;
               """,
        ["none"] = """
                   transition-property: none;
                   transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                   transition-duration: 150ms;
                   """,
        ["all"] = """
                  transition-property: all;
                  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                  transition-duration: 150ms;
                  """,
        ["colors"] = """
                     transition-property: color, background-color, border-color, text-decoration-color, fill, stroke;
                     transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                     transition-duration: 150ms;
                     """,
        ["opacity"] = """
                      transition-property: opacity;
                      transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                      transition-duration: 150ms;
                      """,
        ["shadow"] = """
                     transition-property: box-shadow;
                     transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                     transition-duration: 150ms;
                     """,
        ["transform"] = """
                        transition-property: transform;
                        transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                        transition-duration: 150ms;
                        """
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
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"transition: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}