// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontSize : ClassDictionaryBase
{
    public FontSize()
    {
        Group = "font-size";
        Description = "Utilities for setting font size.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-", new ClassDefinition
                {
                    InLengthCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        font-size: calc(var(--spacing) * {0});
                        """,
                    ModifierTemplate =
                        """
                        font-size: calc(var(--spacing) * {0});
                        line-height: calc(var(--spacing) * {1});
                        """,
                    ArbitraryModifierTemplate =
                        """
                        font-size: calc(var(--spacing) * {0});
                        line-height: {1};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        font-size: {0};
                        """,
                    ArbitraryCssValueWithModifierTemplate =
                        """
                        font-size: {0};
                        line-height: calc(var(--spacing) * {1});
                        """,
                    ArbitraryCssValueWithArbitraryModifierTemplate =
                        """
                        font-size: {0};
                        line-height: {1};
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--text-") && i.Key.StartsWith("--text-shadow-") == false && i.Key.LastIndexOf("--", StringComparison.Ordinal) == 0))
        {
            var key = text.Key.Trim('-');
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                UsesSlashModifier = true,
                Template =
                    $"""
                    font-size: var({text.Key});
                    line-height: var(--sf-leading, var({text.Key}--line-height));
                    """,
                ModifierTemplate =
                    $$"""
                    font-size: var({{text.Key}});
                    line-height: calc(var(--spacing) * {1});
                    """,
                ArbitraryModifierTemplate =
                    $$"""
                      font-size: var({{text.Key}});
                      line-height: {1};
                      """,
                ArbitraryCssValueWithModifierTemplate =
                    $$"""
                    font-size: var({{text.Key}});
                    line-height: calc(var(--spacing) * {1});
                    """,
                ArbitraryCssValueWithArbitraryModifierTemplate =
                    $$"""
                      font-size: var({{text.Key}});
                      line-height: {1};
                      """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}