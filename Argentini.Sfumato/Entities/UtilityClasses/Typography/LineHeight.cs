// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class LineHeight : ClassDictionaryBase
{
    public LineHeight()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "leading-", new ClassDefinition
                {
                    UsesDimensionLength = true,
                    UsesAlphaNumber = true,
                    UsesSpacing = true,
                    SelectorSort = 1,
                    Template =
                        """
                        line-height: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        line-height: {0};
                        """,
                    UsesCssCustomProperties = [
                        "--spacing"
                    ]
                }
            },
            {
                "-leading-", new ClassDefinition
                {
                    UsesDimensionLength = true,
                    UsesAlphaNumber = true,
                    UsesSpacing = true,
                    SelectorSort = 1,
                    Template =
                        """
                        line-height: calc(var(--spacing) * {0} * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        line-height: -{0};
                        """,
                    UsesCssCustomProperties = [
                        "--spacing"
                    ]
                }
            },
            {
                "leading-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    SelectorSort = 1,
                    Template = """
                               line-height: 1;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var font in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--leading-")))
        {
            var key = font.Key.Trim('-');
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = 
                    $"line-height: var({font.Key});",
                UsesCssCustomProperties = [ font.Key ]
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}