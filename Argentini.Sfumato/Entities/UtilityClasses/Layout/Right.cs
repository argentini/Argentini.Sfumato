// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Right : ClassDictionaryBase
{
    public Right()
    {
        Description = "Utilities for setting the right offset of positioned elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "right-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               right: auto;
                               """
                }
            },
            {
                "right-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               right: 1px;
                               """
                }
            },
            {
                "right-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               right: 100%;
                               """
                }
            },
            
            {
                "-right-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               right: -1px;
                               """
                }
            },
            {
                "-right-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               right: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "right-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               right: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        right: {0};
                        """,
                }
            },
            {
                "-right-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               right: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        right: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
