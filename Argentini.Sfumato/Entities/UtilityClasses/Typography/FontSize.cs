// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontSize : ClassDictionaryBase
{
    public FontSize()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-", new ClassDefinition
                {
                    UsesDimensionLength = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: {0};
                               """,
                    ModifierTemplate = """
                                       font-size: {0};
                                       line-height: calc(var(--spacing) * {1});
                                       """,
                    ArbitraryModifierTemplate = """
                                       font-size: {0};
                                       line-height: {1};
                                       """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--text-") && i.Key.LastIndexOf("--", StringComparison.Ordinal) == 0))
        {
            if (text.Key.EndsWith("--line-height"))
                continue;

            var key = text.Key.Trim('-');
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template =
                    $"""
                    font-size: var({text.Key});
                    line-height: var({text.Key}--line-height, initial);
                    letter-spacing: var({text.Key}--letter-spacing, initial);
                    font-weight: var({text.Key}--font-weight, initial);
                    """,
                ModifierTemplate =
                    $$"""
                    font-size: var({{text.Key}});
                    line-height: calc(var(--spacing) * {1});
                    letter-spacing: var({{text.Key}}--letter-spacing, initial);
                    font-weight: var({{text.Key}}--font-weight, initial);
                    """,
                ArbitraryModifierTemplate =
                    $$"""
                    font-size: var({{text.Key}});
                    line-height: {1};
                    letter-spacing: var({{text.Key}}--letter-spacing, initial);
                    font-weight: var({{text.Key}}--font-weight, initial);
                    """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}