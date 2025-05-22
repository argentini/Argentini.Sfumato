// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class LineHeight : ClassDictionaryBase
{
    public LineHeight()
    {
        Group = "line-height";
        Description = "Utilities for setting line height.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "leading-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InLengthCollection = true,
                    InFloatNumberCollection = true,
                    Template =
                        """
                        line-height: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        line-height: {0};
                        """,
                }
            },
            {
                "-leading-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InLengthCollection = true,
                    InFloatNumberCollection = true,
                    Template =
                        """
                        line-height: calc(var(--spacing) * {0} * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        line-height: -{0};
                        """,
                }
            },
            {
                "leading-none", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
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
                InSimpleUtilityCollection = true,
                Template = 
                    $"line-height: var({font.Key});",
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}