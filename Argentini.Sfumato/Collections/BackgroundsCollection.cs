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
    
    
    
    
    
}