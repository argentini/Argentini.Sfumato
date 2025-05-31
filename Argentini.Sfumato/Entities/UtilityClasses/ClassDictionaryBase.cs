// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBeProtected.Global

namespace Argentini.Sfumato.Entities.UtilityClasses;

public abstract class ClassDictionaryBase
{
    public Dictionary<string, ClassDefinition> Data { get; } = new (StringComparer.Ordinal);
    public string Description { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string GroupDescription { get; set; } = string.Empty;
    
    public ClassDefinition this[string key]
    {
        get => Data[key];
        set => Data[key] = value;
    }

    public abstract void ProcessThemeSettings(AppRunner appRunner);
    
    #region Constants

    protected readonly Dictionary<string, string> WidthSizes = new ()
    {
        {
            "auto", "auto"
        },
        {
            "px", "1px"
        },
        {
            "full", "100%"
        },
        {
            "screen", "100vw"
        },
        {
            "dvw", "100dvw"
        },
        {
            "dvh", "100dvh"
        },
        {
            "lvw", "100lvw"
        },
        {
            "lvh", "100lvh"
        },
        {
            "svw", "100svw"
        },
        {
            "svh", "100svh"
        },
        {
            "min", "min-content"
        },
        {
            "max", "max-content"
        },
        {
            "fit", "fit-content"
        },
    };

    protected readonly Dictionary<string, string> HeightSizes = new ()
    {
        {
            "px", "1px"
        },
        {
            "auto", "auto"
        },
        {
            "none", "none"
        },
        {
            "full", "100%"
        },
        {
            "screen", "100vh"
        },
        {
            "dvw", "100dvw"
        },
        {
            "dvh", "100dvh"
        },
        {
            "lvw", "100lvw"
        },
        {
            "lvh", "100lvh"
        },
        {
            "svw", "100svw"
        },
        {
            "svh", "100svh"
        },
        {
            "min", "min-content"
        },
        {
            "max", "max-content"
        },
        {
            "fit", "fit-content"
        },
        {
            "lh", "1lh"
        },
    };

    protected readonly List<string> Cursors = [
        "auto",
        "default", 
        "pointer", 
        "wait",
        "text",
        "move",
        "help",
        "not-allowed",
        "none",
        "context-menu",
        "progress",
        "cell",
        "crosshair",
        "vertical-text",
        "alias",
        "copy",
        "no-drop",
        "grab",
        "grabbing",
        "all-scroll",
        "col-resize",
        "row-resize",
        "n-resize",
        "e-resize",
        "s-resize",
        "w-resize",
        "ne-resize",
        "nw-resize",
        "se-resize",
        "sw-resize",
        "ew-resize",
        "ns-resize",
        "nesw-resize",
        "nwse-resize",
        "zoom-in",
        "zoom-out",
    ];

    #endregion
}