// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TextShadow : ClassDictionaryBase
{
    public TextShadow()
    {
        Group = "text-shadow";
        Description = "Utilities for adding shadow to text.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-shadow-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        text-shadow: {0};
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        text-shadow: {0};
                        """,
                }
            },
            {
                "text-shadow-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = 
                        """
                        text-shadow: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--text-shadow-")))
        {
            var key = text.Key.Trim('-');
            var newValue = text.Value;
            var color = newValue.ExtractWebColors().FirstOrDefault() ?? "oklch(0 0 0 / 10%)";
            var pct = color.ExtractPercentages().FirstOrDefault() ?? "10%";

            newValue = newValue.Replace(color, "var(--sf-text-shadow-color)");
            color = color.Replace(pct, "var(--sf-text-shadow-alpha)");
            
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                UsesSlashModifier = true,
                Template =
                    $$"""
                      --sf-text-shadow-alpha: {{pct}};
                      --sf-text-shadow-color: {{color}};
                      --sf-text-shadow: {{newValue}};
                      text-shadow: var(--sf-text-shadow);
                      """,
                ModifierTemplate = 
                    $$"""
                      --sf-text-shadow-alpha: {1}%;
                      --sf-text-shadow-color: {{color}};
                      --sf-text-shadow: {{newValue}};
                      text-shadow: var(--sf-text-shadow);
                      """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
