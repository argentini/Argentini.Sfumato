// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FLexBasis : ClassDictionaryBase
{
    public FLexBasis()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "basis-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template =
                        """
                        flex-basis: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex-basis: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-basis-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template =
                        """
                        flex-basis: calc(var(--spacing) * -{0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex-basis: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "basis-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-basis: 100%;
                        """,
                }
            },
            {
                "basis-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
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
                IsSimpleUtility = true,
                Template = 
                    $"""
                     flex-basis: var({item.Key});
                     """,
                UsesCssCustomProperties = [ item.Key ]
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}