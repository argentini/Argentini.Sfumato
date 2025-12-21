// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Right : ClassDictionaryBase
{
    public Right()
    {
        Group = "right";
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
            {
                "right-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               right: env(safe-area-inset-right);
                               """
                }
            },
            {
                "right-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               right: env(safe-area-inset-right, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        right: env(safe-area-inset-right, {0});
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
