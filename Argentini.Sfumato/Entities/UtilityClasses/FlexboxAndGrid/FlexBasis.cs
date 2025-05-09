// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexBasis : ClassDictionaryBase
{
    public FlexBasis()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "basis-", new ClassDefinition
                {
                    InLengthCollection = true,
                    InFlexCollection = true,
                    Template =
                        """
                        flex-basis: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex-basis: {0};
                        """,
                }
            },
            {
                "-basis-", new ClassDefinition
                {
                    InLengthCollection = true,
                    InFlexCollection = true,
                    Template =
                        """
                        flex-basis: calc(var(--spacing) * -{0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex-basis: calc({0} * -1);
                        """,
                }
            },
            {
                "basis-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-basis: 100%;
                        """,
                }
            },
            {
                "basis-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-basis: auto;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--container-")))
        {
            var key = item.Key.Trim('-').Replace("container", "basis");
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     flex-basis: var({item.Key});
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}