using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    #region Backgrounds
    
    public ScssBaseClass BackgroundAttachment { get; } = new()
    {
        SelectorPrefix = "bg",
        PropertyName = "background-attachment",
        Options = new Dictionary<string, string>
        {
            ["fixed"] = "fixed",
            ["local"] = "local",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssBaseClass BackgroundClip { get; } = new()
    {
        SelectorPrefix = "bg-clip",
        PropertyName = "background-clip",
        Options = new Dictionary<string, string>
        {
            ["border"] = "border-box",
            ["padding"] = "padding-box",
            ["content"] = "content-box",
            ["text"] = "text"
        }
    };
    
    public ScssBaseClass BackgroundColor { get; } = new()
    {
        SelectorPrefix = "bg",
        PropertyName = "background-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BackgroundOrigin { get; } = new()
    {
        SelectorPrefix = "bg-origin",
        PropertyName = "background-origin",
        Options = new Dictionary<string, string>
        {
            ["border"] = "border-box",
            ["padding"] = "padding-box",
            ["content"] = "content-box"
        }
    };
    
    public ScssBaseClass BackgroundPosition { get; } = new()
    {
        SelectorPrefix = "bg",
        PropertyName = "background-position",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["bottom"] = "bottom",
            ["center"] = "center",
            ["left"] = "left",
            ["left-bottom"] = "left bottom",
            ["left-top"] = "left top",
            ["right"] = "right",
            ["right-bottom"] = "right bottom",
            ["right-top"] = "right top",
            ["top"] = "top"
        }
    };
    
    public ScssBaseClass BackgroundRepeatBase { get; } = new()
    {
        SelectorPrefix = "bg-repeat",
        PropertyName = "background-repeat",
        Options = new Dictionary<string, string>
        {
            ["x"] = "border-box",
            ["y"] = "padding-box",
            ["round"] = "round",
            ["space"] = "space"
        }
    };
    
    public ScssBaseClass BackgroundRepeat { get; } = new()
    {
        PropertyName = "background-repeat",
        Options = new Dictionary<string, string>
        {
            ["bg-repeat"] = "repeat",
            ["bg-no-repeat"] = "no-repeat"
        }
    };
    
    public ScssBaseClass BackgroundSize { get; } = new()
    {
        SelectorPrefix = "bg",
        PropertyName = "background-size",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto",
            ["cover"] = "cover",
            ["contain"] = "contain"
        }
    };
    
    public ScssBaseClass BackgroundImage { get; } = new()
    {
        SelectorPrefix = "bg",
        PropertyName = "background-image",
        PrefixValueTypes = "url",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["none"] = "none"
        }
    };
    
    #endregion
    
    #region Gradients
    
    public ScssUtilityBaseClass BackgroundImageGradientT { get; } = new()
    {
        Selector = "bg-gradient-to-t",
        Template = """
                   background-image: linear-gradient(to top, var(--sf-gradient-stops));
                   """
    };
    
    public ScssUtilityBaseClass BackgroundImageGradientTr { get; } = new()
    {
        Selector = "bg-gradient-to-tr",
        Template = """
                   background-image: linear-gradient(to top right, var(--sf-gradient-stops));
                   """
    };
    
    public ScssUtilityBaseClass BackgroundImageGradientR { get; } = new()
    {
        Selector = "bg-gradient-to-r",
        Template = """
                   background-image: linear-gradient(to right, var(--sf-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientBr { get; } = new()
    {
        Selector = "bg-gradient-to-br",
        Template = """
                   background-image: linear-gradient(to bottom right, var(--sf-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientB { get; } = new()
    {
        Selector = "bg-gradient-to-b",
        Template = """
                   background-image: linear-gradient(to bottom, var(--sf-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientBl { get; } = new()
    {
        Selector = "bg-gradient-to-bl",
        Template = """
                   background-image: linear-gradient(to bottom left, var(--sf-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientL { get; } = new()
    {
        Selector = "bg-gradient-to-l",
        Template = """
                   background-image: linear-gradient(to left, var(--sf-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientTl { get; } = new()
    {
        Selector = "bg-gradient-to-tl",
        Template = """
                   background-image: linear-gradient(to top left, var(--sf-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass GradientColorStopsFromInherit { get; } = new()
    {
        Selector = "from-inherit",
        Template = """
                   --sf-gradient-from: inherit var(--sf-gradient-from-position);
                   --sf-gradient-to: rgb(255 255 255 / 0) var(--sf-gradient-to-position);
                   --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
                   """
    };

    public ScssBaseClass GradientColorStopsFromColors { get; } = new()
    {
        SelectorPrefix = "from",
        PrefixValueTypes = "color",
        GlobalGrouping = "gradients",
        AddColorOptions = true,
        PropertyTemplate = """
                           --sf-gradient-from: {value} var(--sf-gradient-from-position);
                           --sf-gradient-to: transparent var(--sf-gradient-to-position);
                           --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
                           """,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass GradientColorStopsFromPercentages { get; } = new()
    {
        SelectorPrefix = "from",
        PropertyName = "--sf-gradient-from-position",
        PrefixValueTypes = "percentage",
        AddPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssUtilityBaseClass GradientColorStopsViaInherit { get; } = new()
    {
        Selector = "via-inherit",
        Template = """
                   --sf-gradient-to: rgb(255 255 255 / 0)  var(--sf-gradient-to-position);
                   --sf-gradient-stops: var(--sf-gradient-from), inherit var(--sf-gradient-via-position), var(--sf-gradient-to);
                   """
    };

    public ScssBaseClass GradientColorStopsViaColors { get; } = new()
    {
        SelectorPrefix = "via",
        PrefixValueTypes = "color",
        GlobalGrouping = "gradients",
        AddColorOptions = true,
        PropertyTemplate = """
                           --sf-gradient-to: transparent  var(--sf-gradient-to-position);
                           --sf-gradient-stops: var(--sf-gradient-from), {value} var(--sf-gradient-via-position), var(--sf-gradient-to);
                           """,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass GradientColorStopsViaPercentages { get; } = new()
    {
        SelectorPrefix = "via",
        PropertyName = "--sf-gradient-via-position",
        PrefixValueTypes = "percentage",
        AddPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssUtilityBaseClass GradientColorStopsToInherit { get; } = new()
    {
        Selector = "to-inherit",
        Template = """
                   --sf-gradient-to: inherit var(--sf-gradient-to-position);
                   """
    };

    public ScssBaseClass GradientColorStopsToColors { get; } = new()
    {
        SelectorPrefix = "to",
        PrefixValueTypes = "color",
        GlobalGrouping = "gradients",
        AddColorOptions = true,
        PropertyTemplate = """
                           --sf-gradient-to: {value} var(--sf-gradient-to-position);
                           """,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass GradientColorStopsToPercentages { get; } = new()
    {
        SelectorPrefix = "to",
        PropertyName = "--sf-gradient-to-position",
        PrefixValueTypes = "percentage",
        AddPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    #endregion
}