// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextDecorationLine : ClassDictionaryBase
{
    public TextDecorationLine()
    {
        Group = "text-decoration-line";
        Description = "Utilities for setting which text decoration lines are drawn.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "underline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-line: underline;
                               """
                }
            },
            {
                "overline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-line: overline;
                               """
                }
            },
            {
                "line-through", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-line: line-through;
                               """
                }
            },
            {
                "no-underline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-line: none;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}