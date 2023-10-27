namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Cursor : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "cursor";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["alias"] = "cursor: alias;",
        ["all-scroll"] = "cursor: all-scroll;",
        ["auto"] = "cursor: auto;",
        ["cell"] = "cursor: cell;",
        ["context-menu"] = "cursor: context-menu;",
        ["col-resize"] = "cursor: col-resize;",
        ["copy"] = "cursor: copy;",
        ["crosshair"] = "cursor: crosshair;",
        ["default"] = "cursor: default;",
        ["e-resize"] = "cursor: e-resize;",
        ["ew-resize"] = "cursor: ew-resize;",
        ["grab"] = "cursor: grab;",
        ["grabbing"] = "cursor: grabbing;",
        ["help"] = "cursor: help;",
        ["move"] = "cursor: move;",
        ["n-resize"] = "cursor: n-resize;",
        ["ne-resize"] = "cursor: ne-resize;",
        ["nesw-resize"] = "cursor: nesw-resize;",
        ["ns-resize"] = "cursor: ns-resize;",
        ["nw-resize"] = "cursor: nw-resize;",
        ["nwse-resize"] = "cursor: nwse-resize;",
        ["no-drop"] = "cursor: no-drop;",
        ["none"] = "cursor: none;",
        ["not-allowed"] = "cursor: not-allowed;",
        ["pointer"] = "cursor: pointer;",
        ["progress"] = "cursor: progress;",
        ["row-resize"] = "cursor: row-resize;",
        ["s-resize"] = "cursor: s-resize;",
        ["se-resize"] = "cursor: se-resize;",
        ["sw-resize"] = "cursor: sw-resize;",
        ["text"] = "cursor: text;",
        ["vertical-text"] = "cursor: vertical-text;",
        ["w-resize"] = "cursor: w-resize;",
        ["wait"] = "cursor: wait;",
        ["zoom-in"] = "cursor: zoom-in;",
        ["zoom-out"] = "cursor: zoom-out;"
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
            return $"cursor: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}