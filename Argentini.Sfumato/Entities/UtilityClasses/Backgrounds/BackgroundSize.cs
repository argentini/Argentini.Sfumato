// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundSize : ClassDictionaryBase
{
    public BackgroundSize()
    {
        Group = "background-size";
        Description = "Utilities for setting background image size.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-size-", new ClassDefinition
                {
                    InLengthCollection = true,
                    InAbstractValueCollection = true,
                    Template =
                        """
                        background-size: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        background-size: {0};
                        """, 
                }
            },
            {
                "bg-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-size: auto;
                        """,
                }
            },
            {
                "bg-cover", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-size: cover;
                        """,
                }
            },
            {
                "bg-contain", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-size: contain;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}