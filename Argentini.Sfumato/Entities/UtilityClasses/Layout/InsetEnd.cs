// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class InsetEnd : ClassDictionaryBase
{
    public InsetEnd()
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
                    UsesNumericSuffix = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-inline-end: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline-end: {0};
                        """,
                }
            },
            {
                "-end-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesDimensionLength = true,
                    Template = """
                               inset-inline-end: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset-inline-end: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
