// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class ObjectPosition : ClassDictionaryBase
{
    public ObjectPosition()
    {
        Group = "object-position";
        Description = "Utilities for setting the alignment of replaced content.";
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
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-position: top left;
                               """
                }
            },
            {
                "object-top", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-position: top;
                               """
                }
            },
            {
                "object-top-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-position: top right;
                               """
                }
            },
            {
                "object-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-position: left;
                               """
                }
            },
            {
                "object-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-position: center;
                               """
                }
            },
            {
                "object-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-position: right;
                               """
                }
            },
            {
                "object-bottom-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-position: bottom left;
                               """
                }
            },
            {
                "object-bottom", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-position: bottom;
                               """
                }
            },
            {
                "object-bottom-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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