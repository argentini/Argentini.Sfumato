// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class InsetX : ClassDictionaryBase
{
    public InsetX()
    {
        Group = "inset";
        Description = "Utilities for setting horizontal insets.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "inset-x-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline: auto;
                               """
                }
            },
            {
                "inset-x-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline: 1px;
                               """
                }
            },
            {
                "inset-x-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline: 100%;
                               """
                }
            },
            
            {
                "-inset-x-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline: -1px;
                               """
                }
            },
            {
                "-inset-x-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset-inline: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "inset-x-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               inset-inline: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline: {0};
                        """,
                }
            },
            {
                "-inset-x-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               inset-inline: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
