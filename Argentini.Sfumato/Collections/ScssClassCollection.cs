namespace Argentini.Sfumato.Collections;

public sealed class ScssClassCollection
{
    #region Classes (Arbitrary Value Support)
    
    public BgColor BgColor { get; } = new();
    public TextColor TextColor { get; } = new();
    public TextSize TextSize { get; } = new();
    public AspectRatio AspectRatio { get; } = new();
    public Columns Columns { get; } = new();

    #endregion
    
    #region Utility Classes (Read-Only, No Values)
    
    public Container Container { get; } = new();

    public ScssUtilityBaseClass BoxDecorationBreak { get; } = new()
    {
        SelectorPrefix = "box-decoration",
        PropertyName = "box-decoration-break",
        Options = new Dictionary<string, string>
        {
            ["clone"] = "clone",
            ["slice"] = "slice"
        }
    };

    public ScssUtilityBaseClass BreakAfter { get; } = new()
    {
        SelectorPrefix = "break-after",
        PropertyName = "break-after",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["all"] = "all",
            ["avoid-page"] = "avoid-page",
            ["page"] = "page",
            ["left"] = "left",
            ["right"] = "right",
            ["column"] = "column"
        }
    };

    public ScssUtilityBaseClass BreakBefore { get; } = new()
    {
        SelectorPrefix = "break-before",
        PropertyName = "break-before",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["all"] = "all",
            ["avoid-page"] = "avoid-page",
            ["page"] = "page",
            ["left"] = "left",
            ["right"] = "right",
            ["column"] = "column"
        }
    };

    public ScssUtilityBaseClass BreakInside { get; } = new()
    {
        SelectorPrefix = "break-inside",
        PropertyName = "break-inside",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["avoid-page"] = "avoid-page",
            ["avoid-column"] = "avoid-column"
        }
    };
    
    public ScssUtilityBaseClass BoxSizing { get; } = new()
    {
        SelectorPrefix = "box",
        PropertyName = "box-sizing",
        Options = new Dictionary<string, string>
        {
            ["border"] = "border-box",
            ["content"] = "content-box"
        }
    };
    
    public ScssUtilityBaseClass Display { get; } = new()
    {
        PropertyName = "display",
        Options = new Dictionary<string, string>
        {
            ["block"] = "block",
            ["inline-block"] = "inline-block",
            ["inline"] = "inline",
            ["flex"] = "flex",
            ["inline-flex"] = "inline-flex",
            ["table"] = "table",
            ["inline-table"] = "inline-table",
            ["table-caption"] = "table-caption",
            ["table-cell"] = "table-cell",
            ["table-column"] = "table-column",
            ["table-column-group"] = "table-column-group",
            ["table-footer-group"] = "table-footer-group",
            ["table-header-group"] = "table-header-group",
            ["table-row-group"] = "table-row-group",
            ["table-row"] = "table-row",
            ["flow-root"] = "flow-root",
            ["grid"] = "grid",
            ["inline-grid"] = "inline-grid",
            ["contents"] = "contents",
            ["list-item"] = "list-item",
            ["hidden"] = "none"
        }
    };
    
    public ScssUtilityBaseClass Floats { get; } = new()
    {
        SelectorPrefix = "float",
        PropertyName = "float",
        Options = new Dictionary<string, string>
        {
            ["right"] = "right",
            ["left"] = "left",
            ["none"] = "none"
        }
    };

    public ScssUtilityBaseClass Clear { get; } = new()
    {
        SelectorPrefix = "clear",
        PropertyName = "clear",
        Options = new Dictionary<string, string>
        {
            ["right"] = "right",
            ["left"] = "left",
            ["both"] = "both",
            ["none"] = "none"
        }
    };
    
    public ScssUtilityBaseClass Isolation { get; } = new()
    {
        PropertyName = "isolation",
        Options = new Dictionary<string, string>
        {
            ["isolate"] = "isolate",
            ["isolation-auto"] = "auto"
        }
    };
    
    public ScssUtilityBaseClass ObjectFit { get; } = new()
    {
        SelectorPrefix = "object",
        PropertyName = "object-fit",
        Options = new Dictionary<string, string>
        {
            ["contain"] = "contain",
            ["cover"] = "cover",
            ["fill"] = "fill",
            ["none"] = "none",
            ["scale-down"] = "scale-down"
        }
    };

    public ScssUtilityBaseClass ObjectBottom { get; } = new()
    {
        SelectorPrefix = "object",
        PropertyName = "object-position",
        Options = new Dictionary<string, string>
        {
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
    
    public ScssUtilityBaseClass Overflow { get; } = new()
    {
        SelectorPrefix = "overflow",
        PropertyName = "overflow",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssUtilityBaseClass OverflowX { get; } = new()
    {
        SelectorPrefix = "overflow-x",
        PropertyName = "overflow-x",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssUtilityBaseClass OverflowY { get; } = new()
    {
        SelectorPrefix = "overflow-y",
        PropertyName = "overflow-y",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssUtilityBaseClass OverscrollBehavior { get; } = new()
    {
        SelectorPrefix = "overscroll",
        PropertyName = "overscroll-behavior",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssUtilityBaseClass OverscrollBehaviorX { get; } = new()
    {
        SelectorPrefix = "overscroll-x",
        PropertyName = "overscroll-behavior-x",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssUtilityBaseClass OverscrollBehaviorY { get; } = new()
    {
        SelectorPrefix = "overscroll-y",
        PropertyName = "overscroll-behavior-y",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssUtilityBaseClass Position { get; } = new()
    {
        PropertyName = "position",
        Options = new Dictionary<string, string>
        {
            ["static"] = "static",
            ["fixed"] = "fixed",
            ["absolute"] = "absolute",
            ["relative"] = "relative",
            ["sticky"] = "sticky"
        }
    };
    
    public ScssUtilityBaseClass Top { get; } = new()
    {
        SelectorPrefix = "top",
        PropertyName = "top",
        PrefixValueTypes = "length,precentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto",
            ["1/2"] = "50%",
            ["1/3"] = "33.333333%",
            ["2/3"] = "66.666667%",
            ["1/4"] = "25%",
            ["2/4"] = "50%",
            ["3/4"] = "75%",
            ["full"] = "100%"
        }
    };

    public ScssUtilityBaseClass Right { get; } = new()
    {
        SelectorPrefix = "right",
        PropertyName = "right",
        PrefixValueTypes = "length,precentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto",
            ["1/2"] = "50%",
            ["1/3"] = "33.333333%",
            ["2/3"] = "66.666667%",
            ["1/4"] = "25%",
            ["2/4"] = "50%",
            ["3/4"] = "75%",
            ["full"] = "100%"
        }
    };

    public ScssUtilityBaseClass Bottom { get; } = new()
    {
        SelectorPrefix = "bottom",
        PropertyName = "bottom",
        PrefixValueTypes = "length,precentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto",
            ["1/2"] = "50%",
            ["1/3"] = "33.333333%",
            ["2/3"] = "66.666667%",
            ["1/4"] = "25%",
            ["2/4"] = "50%",
            ["3/4"] = "75%",
            ["full"] = "100%"
        }
    };
    
    public ScssUtilityBaseClass Left { get; } = new()
    {
        SelectorPrefix = "left",
        PropertyName = "left",
        PrefixValueTypes = "length,precentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto",
            ["1/2"] = "50%",
            ["1/3"] = "33.333333%",
            ["2/3"] = "66.666667%",
            ["1/4"] = "25%",
            ["2/4"] = "50%",
            ["3/4"] = "75%",
            ["full"] = "100%"
        }
    };
    
    
    
    
    
    
    
    #endregion

    public ScssClassCollection()
    {
        var step = (decimal)0.5;

        for (var x = (decimal)0.5; x < 97; x += step)
        {
            if (x == 4)
                step = 1;

            Top.Options?.TryAdd($"{x:0.#}", $"{x / 4:0.###}rem");
            Right.Options?.TryAdd($"{x:0.#}", $"{x / 4:0.###}rem");
            Bottom.Options?.TryAdd($"{x:0.#}", $"{x / 4:0.###}rem");
            Left.Options?.TryAdd($"{x:0.#}", $"{x / 4:0.###}rem");
        }
        
        Top.Generate();
        Right.Generate();
        Bottom.Generate();
        Left.Generate();
    }
    
    #region Class Helper Methods
    
    /// <summary>
    /// Get the total number of items in all SCSS class collections. 
    /// </summary>
    /// <returns></returns>
    public int GetClassCount()
    {
        var result = 0;

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null)
                continue;

            result += dictionaryReference.Count;
        }

        return result;
    }

    /// <summary>
    /// Get all ScssClass objects matching a given root class name (not user class name).
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    public IEnumerable<ScssClass> GetAllByClassName(string className)
    {
        var result = new List<ScssClass>();
        var rootClassName = className;

        if (className.EndsWith(']') && className.Contains('['))
        {
            rootClassName = className[..className.IndexOf('[')];
        }
        
        rootClassName = rootClassName.Contains(':') ? rootClassName[(rootClassName.LastIndexOf(':') + 1)..] : rootClassName;

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null)
                continue;

            if (dictionaryReference.TryGetValue(rootClassName, out var scssClass))
            {
                result.Add(scssClass);
            }
        }

        return result;
    }
    
    /// <summary>
    /// Get the first key of all collections; list has only unique prefixes (e.g. "bg-", "text-", etc.).
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetClassPrefixesForRegex()
    {
        var result = new Dictionary<string,string>();

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null || dictionaryReference.Count < 1)
                continue;

            var key = dictionaryReference.First().Key.Replace("-", "\\-");
            
            result.TryAdd(key, string.Empty);
        }

        return result.Keys;
    }

    /// <summary>
    /// Gets a list of unique utility class names.
    /// Utility classes are ScssClass items with empty Value and ValueTypes properties.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetUtilityClassNames()
    {
        var result = new Dictionary<string,string>();

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null || dictionaryReference.Count < 1)
                continue;

            foreach (var (key, value) in dictionaryReference)
            {
                if (value is { Value: "", ValueTypes: "" })
                    result.TryAdd(key, string.Empty);
            }
        }

        return result.Keys;
    }
    
    #endregion
}