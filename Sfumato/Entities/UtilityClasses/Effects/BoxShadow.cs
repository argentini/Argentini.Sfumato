// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BoxShadow : ClassDictionaryBase
{
    public BoxShadow()
    {
        Group = "box-shadow";
        GroupDescription = "Utilities for adding shadow effects to element boxes.";
        Description = "Utilities for adding shadow effects to element boxes.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "shadow-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        box-shadow: {0};
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        box-shadow: {0};
                        """,
                    ModifierTemplate = 
                        """
                        box-shadow: {0};
                        """
                }
            },
            {
                "shadow-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = 
                        """
                        box-shadow: 0 0 #0000;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--shadow-")))
        {
            var key = text.Key.Trim('-');
            var newValue = text.Value;
            var color = newValue.ExtractWebColors().FirstOrDefault() ?? "oklch(0 0 0 / 10%)";
            var pct = color.ExtractPercentages().FirstOrDefault() ?? "10%";

            newValue = newValue.Replace(color, "var(--sf-shadow-color)");
            color = color.Replace(pct, "var(--sf-shadow-alpha)");
            
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                UsesSlashModifier = true,
                Template =
                    $$"""
                    --sf-shadow-alpha: {{pct}};
                    --sf-shadow-color: {{color}};
                    --sf-shadow: {{newValue}};
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                ModifierTemplate = 
                    $$"""
                    --sf-shadow-alpha: {1}%;
                    --sf-shadow-color: {{color}};
                    --sf-shadow: {{newValue}};
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                ArbitraryModifierTemplate =  
                    $$"""
                      --sf-shadow-alpha: {1};
                      --sf-shadow-color: {{color}};
                      --sf-shadow: {{newValue}};
                      box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                      """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
