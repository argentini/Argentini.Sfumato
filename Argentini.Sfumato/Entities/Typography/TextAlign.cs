// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.Typography;

public sealed class TextAlign : ClassDictionaryBase
{
    public TextAlign()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>()
        {
            {
                "text-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: left;
                               """
                }
            },
            {
                "text-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: center;
                               """
                }
            },
            {
                "text-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: right;
                               """
                }
            },
            {
                "text-justify", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: justify;
                               """
                }
            },
            {
                "text-start", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: start;
                               """
                }
            },
            {
                "text-end", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: end;
                               """
                }
            },
        });
    }
}