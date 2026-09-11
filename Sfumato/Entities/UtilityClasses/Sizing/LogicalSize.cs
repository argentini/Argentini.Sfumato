// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Sizing;

public sealed class LogicalSize : ClassDictionaryBase
{
    private static readonly (string Name, string Property, bool Inline)[] Definitions =
    [
        ("inline", "inline-size", true),
        ("min-inline", "min-inline-size", true),
        ("max-inline", "max-inline-size", true),
        ("block", "block-size", false),
        ("min-block", "min-block-size", false),
        ("max-block", "max-block-size", false),
    ];

    public LogicalSize()
    {
        Group = "sizing";
        Description = "Utilities for logical inline and block sizing.";

        for (var index = 0; index < Definitions.Length; index++)
        {
            var (name, property, inline) = Definitions[index];

            AddSimple(name, property, "full", "100%");
            AddSimple(name, property, "min", "min-content");
            AddSimple(name, property, "max", "max-content");
            AddSimple(name, property, "fit", "fit-content");

            if (name.StartsWith("max-", StringComparison.Ordinal))
                AddSimple(name, property, "none", "none");
            else
                AddSimple(name, property, "auto", "auto");

            if (name == "inline")
            {
                AddSimple(name, property, "screen", "100vw");
                AddSimple(name, property, "svw", "100svw");
                AddSimple(name, property, "lvw", "100lvw");
                AddSimple(name, property, "dvw", "100dvw");
            }
            else if (inline == false)
            {
                AddSimple(name, property, "screen", "100vh");
                AddSimple(name, property, "svh", "100svh");
                AddSimple(name, property, "lvh", "100lvh");
                AddSimple(name, property, "dvh", "100dvh");
                AddSimple(name, property, "lh", "1lh");
            }

            Data.Add($"{name}-", new ClassDefinition
            {
                InLengthCollection = true,
                DisallowsFractions = name.StartsWith("min-", StringComparison.Ordinal) || name.StartsWith("max-", StringComparison.Ordinal),
                Template = $"{property}: calc(var(--spacing) * {{0}});",
                ArbitraryCssValueTemplate = $"{property}: {{0}};",
            });
        }
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(item => item.Key.StartsWith("--container-", StringComparison.Ordinal)))
        {
            var suffix = item.Key[12..];

            AddThemeSize(appRunner, $"inline-{suffix}", "inline-size", item.Key);
            AddThemeSize(appRunner, $"min-inline-{suffix}", "min-inline-size", item.Key);
            AddThemeSize(appRunner, $"max-inline-{suffix}", "max-inline-size", item.Key);
        }
    }

    private void AddSimple(string name, string property, string suffix, string value)
    {
        Data.Add($"{name}-{suffix}", new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = $"{property}: {value};",
        });
    }

    private static void AddThemeSize(AppRunner appRunner, string name, string property, string customProperty)
    {
        var definition = new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = $"{property}: var({customProperty});",
        };

        if (appRunner.Library.SimpleClasses.TryAdd(name, definition))
            appRunner.Library.ScannerClassNamePrefixes.Insert(name, null);
        else
            appRunner.Library.SimpleClasses[name] = definition;
    }
}
