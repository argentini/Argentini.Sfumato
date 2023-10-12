namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public List<ScssClass> AllClasses { get; } = new();
    
    public ScssClassCollection()
    {
        var sortIndex = 0;
        
        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(List<>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;

            if (classesProperty.GetValue(propertyValue) is not List<ScssClass> listReference)
                continue;

            foreach (var scssClass in listReference)
            {
                scssClass.SortOrder = sortIndex++;
            }
            
            AllClasses.AddRange(listReference);
        }
    }
    
    #region Class Helper Methods
    
    /// <summary>
    /// Get all ScssClass objects matching a given root class name (not user class name).
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    public IEnumerable<ScssClass> GetAllByClassName(string className)
    {
        var rootClassName = className;

        if (className.EndsWith(']') && className.Contains('['))
        {
            rootClassName = className[..className.IndexOf('[')];
        }
        
        rootClassName = rootClassName.Contains(':') ? rootClassName[(rootClassName.LastIndexOf(':') + 1)..] : rootClassName;

        return AllClasses.Where(x => x.RootClassName == rootClassName);
    }
    
    #endregion
}