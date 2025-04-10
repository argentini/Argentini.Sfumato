// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class LetterSpacing : ClassDictionaryBase
{
    public LetterSpacing()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "tracking-", new ClassDefinition
                {
                    UsesDimensionLength = true,
                    Template = """
                               letter-spacing: {0};
                               """
                }
            },
            {
                "-tracking-", new ClassDefinition
                {
                    UsesDimensionLength = true,
                    Template = """
                               letter-spacing: calc({0} * -1);
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var font in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--tracking-")))
        {
            var key = font.Key.Trim('-');
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = 
                    $"letter-spacing: var({font.Key});",
                UsesCssCustomProperties = [ font.Key ]
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}