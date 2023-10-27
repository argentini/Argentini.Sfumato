namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Container : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "container";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = """
               width: 100%;

               @include sf-media($from: phab) {
                  max-width: $phab-breakpoint;
               }

               @include sf-media($from: tabp) {
                  max-width: $tabp-breakpoint;
               }

               @include sf-media($from: tabl) {
                  max-width: $tabl-breakpoint;
               }

               @include sf-media($from: note) {
                  max-width: $note-breakpoint;
               }

               @include sf-media($from: desk) {
                  max-width: $desk-breakpoint;
               }

               @include sf-media($from: elas) {
                  max-width: $elas-breakpoint;
               }
               """,
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        // Static utilities (e.g. bg-no-repeat)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion

        return string.Empty;
    }
}