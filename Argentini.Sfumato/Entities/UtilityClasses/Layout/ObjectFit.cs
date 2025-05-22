// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class ObjectFit : ClassDictionaryBase
{
    public ObjectFit()
    {
        Group = "object-fit";
        Description = "Utilities for controlling how replaced content fits within its box.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "object-contain", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-fit: contain;
                               """
                }
            },
            {
                "object-cover", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-fit: cover;
                               """
                }
            },
            {
                "object-fill", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-fit: fill;
                               """
                }
            },
            {
                "object-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-fit: none;
                               """
                }
            },
            {
                "object-scale-down", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               object-fit: scale-down;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}