using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
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
    
    public ScssBaseClass BgColor { get; } = new()
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
    
    public ScssUtilityBaseClass BackgroundImageGradientT { get; } = new()
    {
        Selector = "bg-gradient-to-t",
        Template = """
                   background-image: linear-gradient(to top, var(--tw-gradient-stops));
                   """
    };
    
    public ScssUtilityBaseClass BackgroundImageGradientTr { get; } = new()
    {
        Selector = "bg-gradient-to-tr",
        Template = """
                   background-image: linear-gradient(to top right, var(--tw-gradient-stops));
                   """
    };
    
    public ScssUtilityBaseClass BackgroundImageGradientR { get; } = new()
    {
        Selector = "bg-gradient-to-r",
        Template = """
                   background-image: linear-gradient(to right, var(--tw-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientBr { get; } = new()
    {
        Selector = "bg-gradient-to-br",
        Template = """
                   background-image: linear-gradient(to bottom right, var(--tw-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientB { get; } = new()
    {
        Selector = "bg-gradient-to-b",
        Template = """
                   background-image: linear-gradient(to bottom, var(--tw-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientBl { get; } = new()
    {
        Selector = "bg-gradient-to-bl",
        Template = """
                   background-image: linear-gradient(to bottom left, var(--tw-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientL { get; } = new()
    {
        Selector = "bg-gradient-to-l",
        Template = """
                   background-image: linear-gradient(to left, var(--tw-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass BackgroundImageGradientTl { get; } = new()
    {
        Selector = "bg-gradient-to-tl",
        Template = """
                   background-image: linear-gradient(to top left, var(--tw-gradient-stops));
                   """
    };

    public ScssUtilityBaseClass GradientColorStopsFromInherit { get; } = new()
    {
        Selector = "from-inherit",
        Template = """
                   --tw-gradient-from: inherit var(--tw-gradient-from-position);
                   --tw-gradient-to: rgb(255 255 255 / 0) var(--tw-gradient-to-position);
                   --tw-gradient-stops: var(--tw-gradient-from), var(--tw-gradient-to);
                   """
    };

    public ScssBaseClass GradientColorStopsFromColors { get; } = new()
    {
        SelectorPrefix = "from",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        PropertyTemplate = """
                           --tw-gradient-from: {value} var(--tw-gradient-from-position);
                           --tw-gradient-to: transparent var(--tw-gradient-to-position);
                           --tw-gradient-stops: var(--tw-gradient-from), var(--tw-gradient-to);
                           """,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass GradientColorStopsFromPercentages { get; } = new()
    {
        SelectorPrefix = "from",
        PropertyName = "--tw-gradient-from-position",
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
                   --tw-gradient-to: rgb(255 255 255 / 0)  var(--tw-gradient-to-position);
                   --tw-gradient-stops: var(--tw-gradient-from), inherit var(--tw-gradient-via-position), var(--tw-gradient-to);
                   """
    };

    public ScssBaseClass GradientColorStopsViaColors { get; } = new()
    {
        SelectorPrefix = "via",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        PropertyTemplate = """
                           --tw-gradient-to: transparent  var(--tw-gradient-to-position);
                           --tw-gradient-stops: var(--tw-gradient-from), {value} var(--tw-gradient-via-position), var(--tw-gradient-to);
                           """,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass GradientColorStopsViaPercentages { get; } = new()
    {
        SelectorPrefix = "via",
        PropertyName = "--tw-gradient-via-position",
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
                   --tw-gradient-to: inherit var(--tw-gradient-to-position);
                   """
    };

    public ScssBaseClass GradientColorStopsToColors { get; } = new()
    {
        SelectorPrefix = "to",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        PropertyTemplate = """
                           --tw-gradient-to: {value} var(--tw-gradient-to-position);
                           """,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass GradientColorStopsToPercentages { get; } = new()
    {
        SelectorPrefix = "to",
        PropertyName = "--tw-gradient-to-position",
        PrefixValueTypes = "percentage",
        AddPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
}