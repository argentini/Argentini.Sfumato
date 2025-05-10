// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class OutlineOffset : ClassDictionaryBase
{
    public OutlineOffset()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "outline-offset-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = 
                        """
                        outline-offset: {0}px;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        outline-offset: {0};
                        """,
                }
            },
            {
                "-outline-offset-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = 
                        """
                        outline-offset: calc({0}px * -1);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        outline-offset: calc({0} * -1);
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
