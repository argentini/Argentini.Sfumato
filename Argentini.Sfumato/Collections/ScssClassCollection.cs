namespace Argentini.Sfumato.Collections;

public sealed class ScssClassCollection
{
    public BgColor BgColor { get; } = new();
    public TextColor TextColor { get; } = new();

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
}