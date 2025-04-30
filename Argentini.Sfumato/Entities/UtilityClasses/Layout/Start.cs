// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Start : ClassDictionaryBase
{
    public Start()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "start-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-start: auto;
                               """
                }
            },
            {
                "start-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-start: 1px;
                               """
                }
            },
            {
                "start-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-start: 100%;
                               """
                }
            },
            
            {
                "-start-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-start: -1px;
                               """
                }
            },
            {
                "-start-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
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
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-inline-start: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline-start: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-start-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-inline-start: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline-start: calc({0} * -1);
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
