// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.Typography;

public sealed class LineHeight : ClassDictionaryBase
{
    public LineHeight()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>()
        {
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
            {
                "leading-", new ClassDefinition
                {
                    UsesNumber = true,
                    SelectorSort = 1,
                    Template = """
                               line-height: calc(var(--spacing) * {0});
                               """
                }
            },
            {
                "leading-[", new ClassDefinition
                {
                    UsesLength = true,
                    SelectorSort = 1,
                    Template = """
                               line-height: {0};
                               """
                }
            },
            {
                "leading-(", new ClassDefinition
                {
                    UsesLength = true,
                    SelectorSort = 1,
                    Template = """
                               line-height: var({0});
                               """
                }
            },
            {
                "-leading-", new ClassDefinition
                {
                    UsesNumber = true,
                    SelectorSort = 1,
                    Template = """
                               line-height: calc(var(--spacing) * {0} * -1);
                               """
                }
            },
            {
                "-leading-(", new ClassDefinition
                {
                    UsesLength = true,
                    SelectorSort = 1,
                    Template = """
                               line-height: calc(var({0}) * -1);
                               """
                }
            },
        });
    }
}