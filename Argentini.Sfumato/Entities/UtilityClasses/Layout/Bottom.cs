// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Bottom : ClassDictionaryBase
{
    public Bottom()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "bottom-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               bottom: auto;
                               """
                }
            },
            {
                "bottom-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               bottom: 1px;
                               """
                }
            },
            {
                "bottom-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               bottom: 100%;
                               """
                }
            },
            
            {
                "-bottom-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               bottom: -1px;
                               """
                }
            },
            {
                "-bottom-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               bottom: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "bottom-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               bottom: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        bottom: {0};
                        """,
                }
            },
            {
                "-bottom-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               bottom: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        bottom: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
