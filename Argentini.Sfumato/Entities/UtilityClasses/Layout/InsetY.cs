// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class InsetY : ClassDictionaryBase
{
    public InsetY()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "inset-y-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-block: auto;
                               """
                }
            },
            {
                "inset-y-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-block: 1px;
                               """
                }
            },
            {
                "inset-y-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-block: 100%;
                               """
                }
            },
            
            {
                "-inset-y-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-block: -1px;
                               """
                }
            },
            {
                "-inset-y-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-block: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "inset-y-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-block: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-block: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-inset-y-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-block: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-block: calc({0} * -1);
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
