namespace Argentini.Sfumato.ScssUtilityCollections.Accessibility;

public class NotSrOnly : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "not-sr-only";

    public override string GetStyles(CssSelector cssSelector)
    {
        return """
               position: static;
               width: auto;
               height: auto;
               padding: 0;
               margin: 0;
               overflow: visible;
               clip: auto;
               white-space: normal;
               """;
    }
}