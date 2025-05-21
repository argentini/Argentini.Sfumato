// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class InsetShadow : ClassDictionaryBase
{
    public InsetShadow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inset-shadow-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        box-shadow: inset {0};
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        box-shadow: inset {0};
                        """,
                }
            },
            {
                "inset-shadow-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = 
                        """
                        box-shadow: inset 0 0 #0000;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--inset-shadow-")))
        {
            var key = text.Key.Trim('-');
            var newValue = text.Value;
            var color = newValue.ExtractWebColors().FirstOrDefault() ?? "oklch(0 0 0 / 5%)";
            var pct = color.ExtractPercentages().FirstOrDefault() ?? "5%";

            newValue = newValue.Replace(color, "var(--sf-inset-shadow-color)");
            color = color.Replace(pct, "var(--sf-inset-shadow-alpha)");
            
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                UsesSlashModifier = true,
                Template =
                    $$"""
                      --sf-inset-shadow-alpha: {{pct}};
                      --sf-inset-shadow-color: {{color}};
                      --sf-inset-shadow: {{newValue}};
                      box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                      """,
                ModifierTemplate = 
                    $$"""
                      --sf-inset-shadow-alpha: {1}%;
                      --sf-inset-shadow-color: {{color}};
                      --sf-inset-shadow: {{newValue}};
                      box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                      """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
