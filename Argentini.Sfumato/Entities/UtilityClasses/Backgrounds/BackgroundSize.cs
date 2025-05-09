// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundSize : ClassDictionaryBase
{
    public BackgroundSize()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-size-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    InLengthCollection = true,
                    Template =
                        """
                        background-size: {0};
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
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-size: auto;
                        """,
                }
            },
            {
                "bg-cover", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-size: cover;
                        """,
                }
            },
            {
                "bg-contain", new ClassDefinition
                {
                    IsSimpleUtility = true,
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