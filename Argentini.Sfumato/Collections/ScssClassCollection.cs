namespace Argentini.Sfumato.Collections;

public sealed class ScssClassCollection
{
    public BgColor BgColor { get; } = new();
    public TextColor TextColor { get; } = new();
    public TextSize TextSize { get; } = new();
    public AspectRatio AspectRatio { get; } = new();
    public ElasticContainer ElasticContainer { get; } = new();

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
                    result.TryAdd(dictionaryReference.First().Key, string.Empty);
            }
        }

        return result.Keys;
    }
}