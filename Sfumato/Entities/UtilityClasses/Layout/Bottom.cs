// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Bottom : ClassDictionaryBase
{
    public Bottom()
    {
        Group = "bottom";
        Description = "Utilities for setting the bottom offset of positioned elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "bottom-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               bottom: auto;
                               """
                }
            },
            {
                "bottom-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               bottom: 1px;
                               """
                }
            },
            {
                "bottom-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               bottom: 100%;
                               """
                }
            },
            
            {
                "-bottom-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               bottom: -1px;
                               """
                }
            },
            {
                "-bottom-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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
