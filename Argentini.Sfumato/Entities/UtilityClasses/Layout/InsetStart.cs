// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class InsetStart : ClassDictionaryBase
{
    public InsetStart()
    {
        Group = "inset";
        Description = "Utilities for setting the logical start edge inset.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "start-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline-start: auto;
                               """
                }
            },
            {
                "start-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline-start: 1px;
                               """
                }
            },
            {
                "start-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline-start: 100%;
                               """
                }
            },
            
            {
                "-start-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline-start: -1px;
                               """
                }
            },
            {
                "-start-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline-start: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "start-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               inset-inline-start: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline-start: {0};
                        """,
                }
            },
            {
                "-start-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               inset-inline-start: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline-start: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
