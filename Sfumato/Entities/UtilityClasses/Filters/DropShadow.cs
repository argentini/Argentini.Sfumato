// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Filters;

public sealed class DropShadow : ClassDictionaryBase
{
    public DropShadow()
    {
        Group = "filter/drop-shadow";
        GroupDescription = "Utilities for adding drop shadow effects to elements.";
        Description = "Utilities for adding drop shadow effects to elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "drop-shadow-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        --sf-drop-shadow-size: drop-shadow({0});
                        --sf-drop-shadow: drop-shadow(var(--sf-drop-shadow-size));
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
            {
                "drop-shadow-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-drop-shadow: ;
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--drop-shadow-")))
        {
            var key = text.Key.Trim('-');
            var newValue = text.Value;
            var color = newValue.ExtractWebColors().FirstOrDefault() ?? "oklch(0 0 0 / 10%)";
            var pct = color.ExtractPercentages().FirstOrDefault() ?? "10%";

            newValue = newValue.Replace(color, "var(--sf-drop-shadow-color)");
            color = color.Replace(pct, "var(--sf-drop-shadow-alpha)");
            
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                UsesSlashModifier = true,
                Template =
                    $$"""
                      --sf-drop-shadow-alpha: {{pct}};
                      --sf-drop-shadow-color: {{color}};
                      --sf-drop-shadow: drop-shadow({{newValue}});
                      filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                      """,
                ModifierTemplate = 
                    $$"""
                      --sf-drop-shadow-alpha: {1}%;
                      --sf-drop-shadow-color: {{color}};
                      --sf-drop-shadow: drop-shadow({{newValue}});
                      filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                      """,
                ArbitraryModifierTemplate = 
                    $$"""
                      --sf-drop-shadow-alpha: {1};
                      --sf-drop-shadow-color: {{color}};
                      --sf-drop-shadow: drop-shadow({{newValue}});
                      filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                      """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}