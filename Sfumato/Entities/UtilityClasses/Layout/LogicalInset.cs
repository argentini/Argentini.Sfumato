// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class LogicalInset : ClassDictionaryBase
{
    private static readonly (string Name, string Property)[] Definitions =
    [
        ("inset-s", "inset-inline-start"),
        ("inset-e", "inset-inline-end"),
        ("inset-bs", "inset-block-start"),
        ("inset-be", "inset-block-end"),
    ];

    public LogicalInset()
    {
        Group = "inset";
        Description = "Utilities for setting logical inset positions.";

        for (var index = 0; index < Definitions.Length; index++)
        {
            var (name, property) = Definitions[index];

            Data.Add($"{name}-auto", Simple($"{property}: auto;"));
            Data.Add($"{name}-full", Simple($"{property}: 100%;"));
            Data.Add($"-{name}-full", Simple($"{property}: -100%;"));
            Data.Add($"{name}-", Length(property, false));
            Data.Add($"-{name}-", Length(property, true));
        }
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(item => item.Key.StartsWith("--inset-", StringComparison.Ordinal) && item.Key.StartsWith("--inset-shadow-", StringComparison.Ordinal) == false))
        {
            var suffix = item.Key[8..];

            for (var index = 0; index < Definitions.Length; index++)
            {
                var (name, property) = Definitions[index];

                AddThemeValue(appRunner, $"{name}-{suffix}", $"{property}: var({item.Key});");
                AddThemeValue(appRunner, $"-{name}-{suffix}", $"{property}: calc(var({item.Key}) * -1);");
            }
        }
    }

    private static ClassDefinition Simple(string template)
    {
        return new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = template,
        };
    }

    private static ClassDefinition Length(string property, bool negative)
    {
        return new ClassDefinition
        {
            InLengthCollection = true,
            Template = negative
                ? $"{property}: calc(var(--spacing) * -{{0}});"
                : $"{property}: calc(var(--spacing) * {{0}});",
            ArbitraryCssValueTemplate = negative
                ? $"{property}: calc({{0}} * -1);"
                : $"{property}: {{0}};",
        };
    }

    private static void AddThemeValue(AppRunner appRunner, string name, string template)
    {
        var definition = Simple(template);

        if (appRunner.Library.SimpleClasses.TryAdd(name, definition))
            appRunner.Library.ScannerClassNamePrefixes.Insert(name, null);
        else
            appRunner.Library.SimpleClasses[name] = definition;
    }
}
