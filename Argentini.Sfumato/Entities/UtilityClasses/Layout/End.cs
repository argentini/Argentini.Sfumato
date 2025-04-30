// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class End : ClassDictionaryBase
{
    public End()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "end-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-end: auto;
                               """
                }
            },
            {
                "end-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-end: 1px;
                               """
                }
            },
            {
                "end-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-end: 100%;
                               """
                }
            },
            
            {
                "-end-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-end: -1px;
                               """
                }
            },
            {
                "-end-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               inset-inline-end: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "end-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-inline-end: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline-end: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-end-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-inline-end: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline-end: calc({0} * -1);
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
