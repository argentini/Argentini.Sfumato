// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class Cursor : ClassDictionaryBase
{
    public Cursor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "cursor-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template = """
                               cursor: {0};
                               """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in Cursors)
        {
            var key = $"cursor-{item}";
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = 
                    $"""
                     cursor: {item};
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}