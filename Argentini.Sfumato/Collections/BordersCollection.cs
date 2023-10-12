using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    #region Rounded
    
    private static Dictionary<string, string> RoundedOptions => new()
    {
        ["-"] = string.Empty,
        [""] = "0.25rem",
        ["none"] = "0px",
        ["sm"] = "0.125rem",
        ["md"] = "0.375rem",
        ["lg"] = "0.5rem",
        ["xl"] = "0.75rem",
        ["2xl"] = "1rem",
        ["3xl"] = "1.5rem",
        ["full"] = "9999px"
    };
    
    public ScssBaseClass Rounded { get; } = new()
    {
        SelectorPrefix = "rounded",
        PropertyName = "border-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedS { get; } = new()
    {
        SelectorPrefix = "rounded-s",
        PropertyName = "border-start-start-radius,border-end-start-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedE { get; } = new()
    {
        SelectorPrefix = "rounded-e",
        PropertyName = "border-start-end-radius,border-end-end-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedT { get; } = new()
    {
        SelectorPrefix = "rounded-t",
        PropertyName = "border-top-left-radius,border-top-right-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };

    public ScssBaseClass RoundedR { get; } = new()
    {
        SelectorPrefix = "rounded-r",
        PropertyName = "border-top-right-radius,border-bottom-right-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedB { get; } = new()
    {
        SelectorPrefix = "rounded-b",
        PropertyName = "border-bottom-right-radius,border-bottom-left-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedL { get; } = new()
    {
        SelectorPrefix = "rounded-l",
        PropertyName = "border-top-left-radius,border-bottom-left-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedSs { get; } = new()
    {
        SelectorPrefix = "rounded-ss",
        PropertyName = "border-start-start-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedSe { get; } = new()
    {
        SelectorPrefix = "rounded-se",
        PropertyName = "border-start-end-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedEe { get; } = new()
    {
        SelectorPrefix = "rounded-ee",
        PropertyName = "border-end-end-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedEs { get; } = new()
    {
        SelectorPrefix = "rounded-es",
        PropertyName = "border-end-start-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedTl { get; } = new()
    {
        SelectorPrefix = "rounded-tl",
        PropertyName = "border-top-left-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedTr { get; } = new()
    {
        SelectorPrefix = "rounded-tr",
        PropertyName = "border-top-right-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedBr { get; } = new()
    {
        SelectorPrefix = "rounded-br",
        PropertyName = "border-bottom-right-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    public ScssBaseClass RoundedBl { get; } = new()
    {
        SelectorPrefix = "rounded-bl",
        PropertyName = "border-bottom-left-radius",
        PrefixValueTypes = "length,percentage",
        Options = RoundedOptions
    };
    
    #endregion
    
    #region Border Width
    
    private static Dictionary<string, string> BorderWidthOptions => new()
    {
        ["-"] = string.Empty,
        [""] = "1px",
        ["0"] = "0px",
        ["2"] = "0.125rem",
        ["4"] = "0.25rem",
        ["8"] = "0.5rem"
    };
    
    public ScssBaseClass BorderWidth { get; } = new()
    {
        SelectorPrefix = "border",
        PropertyName = "border-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };

    public ScssBaseClass BorderWidthX { get; } = new()
    {
        SelectorPrefix = "border-x",
        PropertyName = "border-left-width,border-right-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };
    
    public ScssBaseClass BorderWidthY { get; } = new()
    {
        SelectorPrefix = "border-y",
        PropertyName = "border-top-width,border-bottom-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };
    
    public ScssBaseClass BorderWidthS { get; } = new()
    {
        SelectorPrefix = "border-s",
        PropertyName = "border-inline-start-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };
    
    public ScssBaseClass BorderWidthE { get; } = new()
    {
        SelectorPrefix = "border-e",
        PropertyName = "border-inline-end-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };
    
    public ScssBaseClass BorderWidthT { get; } = new()
    {
        SelectorPrefix = "border-t",
        PropertyName = "border-top-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };
    
    public ScssBaseClass BorderWidthR { get; } = new()
    {
        SelectorPrefix = "border-r",
        PropertyName = "border-right-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };

    public ScssBaseClass BorderWidthB { get; } = new()
    {
        SelectorPrefix = "border-b",
        PropertyName = "border-bottom-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };

    public ScssBaseClass BorderWidthL { get; } = new()
    {
        SelectorPrefix = "border-l",
        PropertyName = "border-left-width",
        PrefixValueTypes = "length,percentage",
        Options = BorderWidthOptions
    };
    
    #endregion
    
    #region Border Color
    
    public ScssBaseClass BorderColor { get; } = new()
    {
        SelectorPrefix = "border",
        PropertyName = "border-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BorderColorX { get; } = new()
    {
        SelectorPrefix = "border-x",
        PropertyName = "border-left-color,border-right-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BorderColorY { get; } = new()
    {
        SelectorPrefix = "border-y",
        PropertyName = "border-top-color,border-bottom-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass BorderColorS { get; } = new()
    {
        SelectorPrefix = "border-s",
        PropertyName = "border-inline-start-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BorderColorE { get; } = new()
    {
        SelectorPrefix = "border-e",
        PropertyName = "border-inline-end-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BorderColorT { get; } = new()
    {
        SelectorPrefix = "border-t",
        PropertyName = "border-top-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BorderColorR { get; } = new()
    {
        SelectorPrefix = "border-r",
        PropertyName = "border-right-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BorderColorB { get; } = new()
    {
        SelectorPrefix = "border-b",
        PropertyName = "border-bottom-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BorderColorL { get; } = new()
    {
        SelectorPrefix = "border-l",
        PropertyName = "border-left-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
   #endregion
   
    public ScssBaseClass BorderStyle { get; } = new()
    {
        SelectorPrefix = "border",
        PropertyName = "border-style",
        Options = new Dictionary<string, string>
        {
            ["solid"] = "solid",
            ["dashed"] = "dashed",
            ["dotted"] = "dotted",
            ["double"] = "double",
            ["hidden"] = "hidden",
            ["none"] = "none"
        }
    };

    #region Divide
    
    private static Dictionary<string, string> DivideWidthOptions => new()
    {
        ["-"] = string.Empty,
        [""] = "1px",
        ["0"] = "0px",
        ["2"] = "0.125rem",
        ["4"] = "0.25rem",
        ["8"] = "0.5rem"
    };
    
    public ScssBaseClass DivideWidthX { get; } = new()
    {
        SelectorPrefix = "divide-x",
        PropertyTemplate = "border-right-width: 0px; border-left-width: {value};",
        PrefixValueTypes = "length,percentage",
        ChildSelector = "& > * + *",
        Options = DivideWidthOptions
    };

    public ScssBaseClass DivideWidthY { get; } = new()
    {
        SelectorPrefix = "divide-y",
        PropertyTemplate = "border-top-width: {value}; border-bottom-width: 0px;",
        PrefixValueTypes = "length,percentage",
        ChildSelector = "& > * + *",
        Options = DivideWidthOptions
    };
    
    public ScssUtilityBaseClass DivideWidthYReverse { get; } = new()
    {
        Selector = "divide-y-reverse",
        ChildSelector = "& > * + *",
        Template = "--tw-divide-y-reverse: 1;"
    };
    
    public ScssUtilityBaseClass DivideWidthXReverse { get; } = new()
    {
        Selector = "divide-x-reverse",
        ChildSelector = "& > * + *",
        Template = "--tw-divide-x-reverse: 1;"
    };
    
    public ScssBaseClass DivideColor { get; } = new()
    {
        SelectorPrefix = "divide",
        PropertyName = "border-color",
        PrefixValueTypes = "color",
        ChildSelector = "& > * + *",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass DivideStyle { get; } = new()
    {
        SelectorPrefix = "divide",
        PropertyName = "border-style",
        ChildSelector = "& > * + *",
        Options = new Dictionary<string, string>
        {
            ["solid"] = "solid",
            ["dashed"] = "dashed",
            ["dotted"] = "dotted",
            ["double"] = "double",
            ["none"] = "none"
        }
    };
    
    #endregion

    #region Outline
    
    public ScssBaseClass OutlineWidth { get; } = new()
    {
        SelectorPrefix = "outline",
        PropertyName = "outline-width",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["1"] = "1px",
            ["2"] = "0.125rem",
            ["4"] = "0.25rem",
            ["8"] = "0.5rem"
        }
    };
    
    public ScssBaseClass OutlineColor { get; } = new()
    {
        SelectorPrefix = "outline",
        PropertyName = "outline-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass OutlineStyle { get; } = new()
    {
        SelectorPrefix = "outline",
        PropertyName = "outline-style",
        Options = new Dictionary<string, string>
        {
            [""] = "solid",
            ["dashed"] = "dashed",
            ["dotted"] = "dotted",
            ["double"] = "double",
            ["none"] = "none"
        }
    };

    public ScssBaseClass OutlineOffset { get; } = new()
    {
        SelectorPrefix = "outline-offset",
        PropertyName = "outline-offset",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["1"] = "1px",
            ["2"] = "0.125rem",
            ["4"] = "0.25rem",
            ["8"] = "0.5rem"
        }
    };
    
    #endregion
    
    #region Ring
    
    public ScssBaseClass RingWidth { get; } = new()
    {
        SelectorPrefix = "ring",
        PropertyTemplate = "box-shadow: var(--tw-ring-inset) 0 0 0 calc({value} + var(--tw-ring-offset-width)) var(--tw-ring-color);",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = "0.1875rem",
            ["0"] = "0px",
            ["1"] = "1px",
            ["2"] = "0.125rem",
            ["4"] = "0.25rem",
            ["8"] = "0.5rem"
        }
    };
    
    public ScssUtilityBaseClass RingWidthInset { get; } = new()
    {
        Selector = "ring-inset",
        Template = "--tw-ring-inset: inset;"
    };
    
    public ScssBaseClass RingColor { get; } = new()
    {
        SelectorPrefix = "ring",
        PrefixValueTypes = "--tw-ring-color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass RingOffsetWidth { get; } = new()
    {
        SelectorPrefix = "ring-offset",
        PropertyTemplate = "--tw-ring-offset-width: {value}; box-shadow: 0 0 0 var(--tw-ring-offset-width) var(--tw-ring-offset-color), var(--tw-ring-shadow);",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["1"] = "1px",
            ["2"] = "0.125rem",
            ["4"] = "0.25rem",
            ["8"] = "0.5rem"
        }
    };
    
    public ScssBaseClass RingOffsetColor { get; } = new()
    {
        SelectorPrefix = "ring-offset",
        PropertyTemplate = "--tw-ring-offset-color: {value}; box-shadow: 0 0 0 var(--tw-ring-offset-width) var(--tw-ring-offset-color), var(--tw-ring-shadow);",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    #endregion
}