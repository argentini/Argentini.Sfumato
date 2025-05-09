// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class ObjectPosition : ClassDictionaryBase
{
    public ObjectPosition()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "object-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        object-position: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        object-position: {0};
                        """
                }
            },
            {
                "object-top-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: top left;
                               """
                }
            },
            {
                "object-top", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: top;
                               """
                }
            },
            {
                "object-top-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: top right;
                               """
                }
            },
            {
                "object-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: left;
                               """
                }
            },
            {
                "object-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: center;
                               """
                }
            },
            {
                "object-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: right;
                               """
                }
            },
            {
                "object-bottom-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: bottom left;
                               """
                }
            },
            {
                "object-bottom", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: bottom;
                               """
                }
            },
            {
                "object-bottom-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               object-position: bottom right;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}