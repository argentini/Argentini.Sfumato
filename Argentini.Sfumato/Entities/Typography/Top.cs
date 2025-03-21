// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.Typography;

public sealed class Top : ClassDictionaryBase
{
    public Top()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>()
        {
            #region Statics
            
            {
                "top-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               top: auto;
                               """
                }
            },
            {
                "top-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               top: 1px;
                               """
                }
            },
            {
                "top-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               top: 100%;
                               """
                }
            },
            
            {
                "-top-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               top: -1px;
                               """
                }
            },
            {
                "-top-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               top: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "top-", new ClassDefinition
                {
                    UsesNumber = true,
                    Template = """
                               top: calc(var(--spacing) * {0});
                               """
                }
            },
            {
                "top-[", new ClassDefinition
                {
                    UsesLength = true,
                    Template = """
                               top: {0};
                               """
                }
            },
            {
                "top-(", new ClassDefinition
                {
                    UsesLength = true,
                    Template = """
                               top: var({0});
                               """
                }
            },

            {
                "-top-", new ClassDefinition
                {
                    UsesLength = true,
                    Template = """
                               top: calc(var(--spacing) * -{0});
                               """
                }
            },
            {
                "-top-(", new ClassDefinition
                {
                    UsesLength = true,
                    Template = """
                               top: calc(var({0}) * -1);
                               """
                }
            },

            #endregion
        });

        AddFractions("top-", "top: calc(({0} / {1}) * 100%);");
        AddFractions("-top-", "top: calc(({0} / {1}) * -100%);");
    }
}
