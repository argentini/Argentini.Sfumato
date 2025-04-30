// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class InsetX : ClassDictionaryBase
{
    public InsetX()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "inset-x-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline: auto;
                               """
                }
            },
            {
                "inset-x-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline: 1px;
                               """
                }
            },
            {
                "inset-x-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline: 100%;
                               """
                }
            },
            
            {
                "-inset-x-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline: -1px;
                               """
                }
            },
            {
                "-inset-x-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
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
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-inline: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-inset-x-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-inline: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
