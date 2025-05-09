// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Sizing;

public sealed class Height : ClassDictionaryBase
{
    public Height()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "h-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               height: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        height: {0};
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in HeightSizes)
        {
            var key = $"h-{item.Key}";
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = 
                    $"""
                     height: {item.Value};
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
