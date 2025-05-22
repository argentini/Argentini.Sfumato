// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Top : ClassDictionaryBase
{
    public Top()
    {
        Description = "Utilities for setting the top offset of positioned elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "top-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               top: auto;
                               """
                }
            },
            {
                "top-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               top: 1px;
                               """
                }
            },
            {
                "top-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               top: 100%;
                               """
                }
            },
            
            {
                "-top-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               top: -1px;
                               """
                }
            },
            {
                "-top-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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
                    InLengthCollection = true,
                    Template = """
                               top: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        top: {0};
                        """,
                }
            },
            {
                "-top-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               top: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        top: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
