// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class BoxDecorationBreak : ClassDictionaryBase
{
    public BoxDecorationBreak()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "box-decoration-clone", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               box-decoration-break: clone;
                               """
                }
            },
            {
                "box-decoration-slice", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               box-decoration-break: slice;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}