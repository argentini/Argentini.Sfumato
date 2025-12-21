// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Left : ClassDictionaryBase
{
    public Left()
    {
        Group = "left";
        Description = "Utilities for setting the left offset of positioned elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "left-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               left: auto;
                               """
                }
            },
            {
                "left-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               left: 1px;
                               """
                }
            },
            {
                "left-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               left: 100%;
                               """
                }
            },
            
            {
                "-left-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               left: -1px;
                               """
                }
            },
            {
                "-left-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               left: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "left-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               left: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        left: {0};
                        """,
                }
            },
            {
                "-left-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               left: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        left: calc({0} * -1);
                        """,
                }
            },
            {
                "left-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               left: env(safe-area-inset-left);
                               """
                }
            },
            {
                "left-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               left: env(safe-area-inset-left, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        left: env(safe-area-inset-left, {0});
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
