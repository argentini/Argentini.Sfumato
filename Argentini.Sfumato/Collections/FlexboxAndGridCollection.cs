using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass FlexBasis { get; } = new()
    {
        SelectorPrefix = "basis",
        PropertyName = "flex-basis",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["px"] = "1px",
            ["auto"] = "auto",
            ["0.5"] = "0.125rem",
            ["1.5"] = "0.375rem",
            ["2.5"] = "0.625rem",
            ["3.5"] = "0.875rem",
            ["1/2"] = "50%",
            ["1/3"] = "33.333333%",
            ["2/3"] = "66.666667%",
            ["1/4"] = "25%",
            ["2/4"] = "50%",
            ["3/4"] = "75%",
            ["1/5"] = "20%",
            ["2/5"] = "40%",
            ["3/5"] = "60%",
            ["4/5"] = "80%",
            ["1/6"] = "16.666667%",
            ["2/6"] = "33.333333%",
            ["3/6"] = "50%",
            ["4/6"] = "66.666667%",
            ["5/6"] = "83.333333%",
            ["1/12"] = "8.333333%",
            ["2/12"] = "16.666667%",
            ["3/12"] = "25%",
            ["4/12"] = "33.333333%",
            ["5/12"] = "41.666667%",
            ["6/12"] = "50%",
            ["7/12"] = "58.333333%",
            ["8/12"] = "66.666667%",
            ["9/12"] = "75%",
            ["10/12"] = "83.333333%",
            ["11/12"] = "91.666667%",
            ["full"] = "100%"
        }
    };

    public ScssBaseClass FlexDirection { get; } = new()
    {
        PropertyName = "flex-direction",
        Options = new Dictionary<string, string>
        {
            ["flex-row"] = "row",
            ["flex-row-reverse"] = "row-reverse",
            ["flex-col"] = "column",
            ["flex-col-reverse"] = "column-reverse"
        }
    };
    
    public ScssBaseClass FlexWrap { get; } = new()
    {
        PropertyName = "flex-wrap",
        Options = new Dictionary<string, string>
        {
            ["flex-wrap"] = "wrap",
            ["flex-wrap-reverse"] = "wrap-reverse",
            ["flex-nowrap"] = "nowrap"
        }
    };
    
    public ScssBaseClass Flex { get; } = new()
    {
        SelectorPrefix = "flex",
        PropertyName = "flex",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["1"] = "1 1 0%",
            ["auto"] = "1 1 auto",
            ["initial"] = "0 1 auto",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass FlexGrowPresets { get; } = new()
    {
        PropertyName = "flex-grow",
        Options = new Dictionary<string, string>
        {
            ["grow"] = "1",
            ["grow-0"] = "0"
        }
    };
    
    public ScssBaseClass FlexGrow { get; } = new()
    {
        SelectorPrefix = "grow",
        PropertyName = "flex-grow",
        PrefixValueTypes = "integer,number",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass FlexShrinkPresets { get; } = new()
    {
        PropertyName = "flex-shrink",
        Options = new Dictionary<string, string>
        {
            ["shrink"] = "1",
            ["shrink-0"] = "0"
        }
    };
    
    public ScssBaseClass FlexShrink { get; } = new()
    {
        SelectorPrefix = "shrink",
        PropertyName = "flex-shrink",
        PrefixValueTypes = "integer,number",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass Order { get; } = new()
    {
        SelectorPrefix = "order",
        PropertyName = "order",
        PrefixValueTypes = "integer",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 24,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["first"] = int.MinValue.ToString(),
            ["last"] = int.MaxValue.ToString(),
            ["none"] = "0"
        }
    };
    
    public ScssBaseClass NegativeOrder { get; } = new()
    {
        SelectorPrefix = "-order",
        PropertyName = "order",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 24,
        AddNumberedOptionsIsNegative = true,
        Options = new Dictionary<string, string>()
    };
    
    public ScssBaseClass GridTemplateColumns { get; } = new()
    {
        SelectorPrefix = "grid-cols",
        PropertyName = "grid-template-columns",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 24,
        AddNumberedOptionsValueTemplate = "repeat({value}, minmax(0, 1fr))",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass GridColumnAuto { get; } = new()
    {
        PropertyName = "grid-column",
        Options = new Dictionary<string, string>
        {
            ["col-auto"] = "auto"
        }
    };
    
    public ScssBaseClass GridColumnSpan { get; } = new()
    {
        SelectorPrefix = "col-span",
        PropertyName = "grid-column",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 24,
        AddNumberedOptionsValueTemplate = "span {value} / span {value}",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["full"] = "1 / -1"
        }
    };
    
    public ScssBaseClass GridColumnStart { get; } = new()
    {
        SelectorPrefix = "col-start",
        PropertyName = "grid-column-start",
        PrefixValueTypes = "integer",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 25,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass GridColumnEnd { get; } = new()
    {
        SelectorPrefix = "col-end",
        PropertyName = "grid-column-end",
        PrefixValueTypes = "integer",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 25,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass GridTemplateRows { get; } = new()
    {
        SelectorPrefix = "grid-rows",
        PropertyName = "grid-template-rows",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 24,
        AddNumberedOptionsValueTemplate = "repeat({value}, minmax(0, 1fr))",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass GridRowAuto { get; } = new()
    {
        PropertyName = "grid-row",
        Options = new Dictionary<string, string>
        {
            ["row-auto"] = "auto"
        }
    };
    
    public ScssBaseClass GridRowSpan { get; } = new()
    {
        SelectorPrefix = "row-span",
        PropertyName = "grid-row",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 24,
        AddNumberedOptionsValueTemplate = "span {value} / span {value}",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["full"] = "1 / -1"
        }
    };
    
    public ScssBaseClass GridRowStart { get; } = new()
    {
        SelectorPrefix = "row-start",
        PropertyName = "grid-row-start",
        PrefixValueTypes = "integer",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 25,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass GridRowEnd { get; } = new()
    {
        SelectorPrefix = "row-end",
        PropertyName = "grid-row-end",
        PrefixValueTypes = "integer",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 25,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto"
        }
    };

    public ScssBaseClass GridAutoFlow { get; } = new()
    {
        PropertyName = "grid-auto-flow",
        Options = new Dictionary<string, string>
        {
            ["grid-flow-row"] = "row",
            ["grid-flow-col"] = "column",
            ["grid-flow-dense"] = "dense",
            ["grid-flow-row-dense"] = "row dense",
            ["grid-flow-col-dense"] = "column dense"
        }
    };
    
    public ScssBaseClass GridAutoColumns { get; } = new()
    {
        SelectorPrefix = "auto-cols",
        PropertyName = "grid-auto-columns",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto",
            ["min"] = "min-content",
            ["max"] = "max-content",
            ["fr"] = "minmax(0, 1fr)"
        }
    };
    
    public ScssBaseClass GridAutoRows { get; } = new()
    {
        SelectorPrefix = "auto-rows",
        PropertyName = "grid-auto-rows",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto",
            ["min"] = "min-content",
            ["max"] = "max-content",
            ["fr"] = "minmax(0, 1fr)"
        }
    };
    
    public ScssBaseClass Gap { get; } = new()
    {
        SelectorPrefix = "gap",
        PropertyName = "gap",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass GapX { get; } = new()
    {
        SelectorPrefix = "gap-x",
        PropertyName = "column-gap",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass GapY { get; } = new()
    {
        SelectorPrefix = "gap-y",
        PropertyName = "row-gap",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass JustifyContent { get; } = new()
    {
        SelectorPrefix = "justify",
        PropertyName = "justify-content",
        Options = new Dictionary<string, string>
        {
            ["normal"] = "normal",
            ["start"] = "flex-start",
            ["end"] = "flex-end",
            ["center"] = "center",
            ["between"] = "space-between",
            ["around"] = "space-around",
            ["evenly"] = "space-evenly",
            ["stretch"] = "stretch"
        }
    };
    
    public ScssBaseClass JustifyItems { get; } = new()
    {
        SelectorPrefix = "justify-items",
        PropertyName = "justify-items",
        Options = new Dictionary<string, string>
        {
            ["start"] = "start",
            ["end"] = "end",
            ["center"] = "center",
            ["stretch"] = "stretch"
        }
    };
    
    public ScssBaseClass JustifySelf { get; } = new()
    {
        SelectorPrefix = "justify-self",
        PropertyName = "justify-self",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["start"] = "start",
            ["end"] = "end",
            ["center"] = "center",
            ["stretch"] = "stretch"
        }
    };
    
    public ScssBaseClass AlignContent { get; } = new()
    {
        SelectorPrefix = "content",
        PropertyName = "align-content",
        Options = new Dictionary<string, string>
        {
            ["normal"] = "normal",
            ["center"] = "center",
            ["start"] = "flex-start",
            ["end"] = "flex-end",
            ["between"] = "space-between",
            ["around"] = "space-around",
            ["evenly"] = "space-evenly",
            ["baseline"] = "baseline",
            ["stretch"] = "stretch"
        }
    };
    
    public ScssBaseClass AlignItems { get; } = new()
    {
        SelectorPrefix = "items",
        PropertyName = "align-items",
        Options = new Dictionary<string, string>
        {
            ["start"] = "flex-start",
            ["end"] = "flex-end",
            ["center"] = "center",
            ["baseline"] = "baseline",
            ["stretch"] = "stretch"
        }
    };
    
    public ScssBaseClass AlignSelf { get; } = new()
    {
        SelectorPrefix = "self",
        PropertyName = "align-self",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["start"] = "flex-start",
            ["end"] = "flex-end",
            ["center"] = "center",
            ["baseline"] = "baseline",
            ["stretch"] = "stretch"
        }
    };
    
    public ScssBaseClass PlaceContent { get; } = new()
    {
        SelectorPrefix = "place-content",
        PropertyName = "place-content",
        Options = new Dictionary<string, string>
        {
            ["center"] = "center",
            ["start"] = "start",
            ["end"] = "end",
            ["between"] = "space-between",
            ["around"] = "space-around",
            ["evenly"] = "space-evenly",
            ["baseline"] = "baseline",
            ["stretch"] = "stretch"
        }
    };

    public ScssBaseClass PlaceItems { get; } = new()
    {
        SelectorPrefix = "place-items",
        PropertyName = "place-items",
        Options = new Dictionary<string, string>
        {
            ["start"] = "start",
            ["end"] = "end",
            ["center"] = "center",
            ["baseline"] = "baseline",
            ["stretch"] = "stretch"
        }
    };
    
    public ScssBaseClass PlaceSelf { get; } = new()
    {
        SelectorPrefix = "place-self",
        PropertyName = "place-self",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["start"] = "start",
            ["end"] = "end",
            ["center"] = "center",
            ["stretch"] = "stretch"
        }
    };
}