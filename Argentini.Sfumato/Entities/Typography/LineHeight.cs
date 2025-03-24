// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.Typography;

public sealed class LineHeight : ClassDictionaryBase
{
    public LineHeight()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "leading-", new ClassDefinition
                {
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    SelectorSort = 1,
                    Template = """
                               line-height: calc(var(--spacing) * {0} * -1);
                               """,
                    CustomCssTemplate = """
                                        line-height: {0};
                                        """
                }
            },
            {
                "leading-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    SelectorSort = 1,
                    Template = """
                               line-height: 1;
                               """
                }
            },
        });
    }
}