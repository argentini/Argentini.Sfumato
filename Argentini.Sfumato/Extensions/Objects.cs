namespace Argentini.Sfumato.Extensions;

public static class Objects
{
    /// <summary>
    /// Get all types that inherit from a base type
    /// </summary>
    /// <param name="baseType"></param>
    /// <param name="excludeSystemAssemblies"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetInheritedTypes(this Type baseType, bool excludeSystemAssemblies = true)
    {
        var types = new List<Type>();
		
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => excludeSystemAssemblies == false || (excludeSystemAssemblies && (a.FullName?.StartsWith("System") ?? false) == false && (a.FullName?.StartsWith("Microsoft") ?? false) == false)))
        {
            var subtypes = assembly.GetTypes().Where(theType => theType is { IsClass: true, IsAbstract: false }).ToList();
            var ts = subtypes.Where(t => t != baseType && t.IsAssignableTo(baseType)).ToList();
            
            types.AddRange(ts);
        }
		
        return types;
    }
}