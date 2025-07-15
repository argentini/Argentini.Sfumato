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
                        --sf-leading: calc(var(--spacing) * {0});
                        line-height: var(--sf-leading);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        --sf-leading: {0};
                        line-height: var(--sf-leading);
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
                        --sf-leading: calc(var(--spacing) * {0} * -1);
                        line-height: var(--sf-leading);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        --sf-leading: calc({0} * -1);
                        line-height: var(--sf-leading);
                        """,
                }
            },
            {
                "leading-none", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-leading: 1;
                        line-height: var(--sf-leading);
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
                    $"""
                    --sf-leading: var({font.Key});
                    line-height: var(--sf-leading);
                    """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}