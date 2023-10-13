using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass TransitionBase { get; } = new()
    {
        SelectorPrefix = "transition",
        PropertyName = "transition",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssUtilityBaseClass Transition { get; } = new()
    {
        Selector = "transition",
        Template = """
                   transition-property: color, background-color, border-color, text-decoration-color, fill, stroke, opacity, box-shadow, transform, filter, backdrop-filter;
                   transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                   transition-duration: 150ms;
                   """
    };
    
    public ScssUtilityBaseClass TransitionNone { get; } = new()
    {
        Selector = "transition-none",
        Template = "transition-property: none;"
    };
    
    public ScssUtilityBaseClass TransitionAll { get; } = new()
    {
        Selector = "transition-all",
        Template = """
                   transition-property: all;
                   transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                   transition-duration: 150ms;
                   """
    };    
    
    public ScssUtilityBaseClass TransitionColors { get; } = new()
    {
        Selector = "transition-colors",
        Template = """
                   transition-property: color, background-color, border-color, text-decoration-color, fill, stroke;
                   transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                   transition-duration: 150ms;
                   """
    };    
    
    public ScssUtilityBaseClass TransitionOpacity { get; } = new()
    {
        Selector = "transition-opacity",
        Template = """
                   transition-property: opacity;
                   transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                   transition-duration: 150ms;
                   """
    };    
    
    public ScssUtilityBaseClass TransitionShadow { get; } = new()
    {
        Selector = "transition-shadow",
        Template = """
                   transition-property: box-shadow;
                   transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                   transition-duration: 150ms;
                   """
    };    
    
    public ScssUtilityBaseClass TransitionTransform { get; } = new()
    {
        Selector = "transition-transform",
        Template = """
                   transition-property: transform;
                   transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                   transition-duration: 150ms;
                   """
    };    

    public ScssBaseClass TransitionDuration { get; } = new()
    {
        SelectorPrefix = "duration",
        PropertyName = "transition-duration",
        PrefixValueTypes = "time",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0s",
            ["75"] = "75ms",
            ["100"] = "100ms",
            ["150"] = "150ms",
            ["200"] = "200ms",
            ["300"] = "300ms",
            ["500"] = "500ms",
            ["700"] = "700ms",
            ["1000"] = "1000ms"
        }
    };
    
    public ScssBaseClass TransitionTimingFunction { get; } = new()
    {
        SelectorPrefix = "ease",
        PropertyName = "transition-timing-function",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["linear"] = "linear",
            ["in"] = "cubic-bezier(0.4, 0, 1, 1)",
            ["out"] = "cubic-bezier(0, 0, 0.2, 1)",
            ["in-out"] = "cubic-bezier(0.4, 0, 0.2, 1)",
        }
    };
    
    public ScssBaseClass TransitionDelay { get; } = new()
    {
        SelectorPrefix = "delay",
        PropertyName = "transition-delay",
        PrefixValueTypes = "time",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0s",
            ["75"] = "75ms",
            ["100"] = "100ms",
            ["150"] = "150ms",
            ["200"] = "200ms",
            ["300"] = "300ms",
            ["500"] = "500ms",
            ["700"] = "700ms",
            ["1000"] = "1000ms"
        }
    };
    
    public ScssBaseClass AnimationBase { get; } = new()
    {
        SelectorPrefix = "animate",
        PropertyName = "animation",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssUtilityBaseClass AnimationNone { get; } = new()
    {
        Selector = "animate",
        Template = "animation: none;"
    };
    
    public ScssUtilityBaseClass AnimationSpin { get; } = new()
    {
        Selector = "animate-spin",
        Template = """
                   animation: spin 1s linear infinite;
                   
                   @keyframes spin {
                      from {
                          transform: rotate(0deg);
                       }
                       to {
                          transform: rotate(360deg);
                       }
                   }
                   """
    };
    
    public ScssUtilityBaseClass AnimationPing { get; } = new()
    {
        Selector = "animate-ping",
        Template = """
                   animation: ping 1s cubic-bezier(0, 0, 0.2, 1) infinite;
                   
                    @keyframes ping {
                        75%, 100% {
                            transform: scale(2);
                            opacity: 0;
                        }
                    }
                   """
    };
    
    public ScssUtilityBaseClass AnimationPulse { get; } = new()
    {
        Selector = "animate-pulse",
        Template = """
                   animation: pulse 2s cubic-bezier(0.4, 0, 0.6, 1) infinite;
                   
                    @keyframes pulse {
                        0%, 100% {
                            opacity: 1;
                        }
                        50% {
                            opacity: .5;
                        }
                    }
                   """
    };

    public ScssUtilityBaseClass AnimationBounce { get; } = new()
    {
        Selector = "animate-bounce",
        Template = """
                   animation: bounce 1s infinite;
                   
                    @keyframes bounce {
                        0%, 100% {
                            transform: translateY(-25%);
                            animation-timing-function: cubic-bezier(0.8, 0, 1, 1);
                        }
                        50% {
                            transform: translateY(0);
                            animation-timing-function: cubic-bezier(0, 0, 0.2, 1);
                        }
                    }
                   """
    };
}