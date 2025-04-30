// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Left : ClassDictionaryBase
{
    public Left()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "left-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               left: auto;
                               """
                }
            },
            {
                "left-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               left: 1px;
                               """
                }
            },
            {
                "left-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               left: 100%;
                               """
                }
            },
            
            {
                "-left-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               left: -1px;
                               """
                }
            },
            {
                "-left-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
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
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               left: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        left: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-left-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               left: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        left: calc({0} * -1);
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
