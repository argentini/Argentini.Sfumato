// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Inset : ClassDictionaryBase
{
    public Inset()
    {
        Group = "inset";
        GroupDescription = "Utilities for setting inset offsets (top, right, bottom, left).";
        Description = "Utilities for setting inset offsets (top, right, bottom, left).";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region Statics
            
            {
                "inset-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset: auto;
                               """
                }
            },
            {
                "inset-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset: 1px;
                               """
                }
            },
            {
                "inset-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset: 100%;
                               """
                }
            },
            
            {
                "-inset-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset: -1px;
                               """
                }
            },
            {
                "-inset-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               inset: -100%;
                               """
                }
            },

            #endregion
            
            #region Numbers, Lengths
            
            {
                "inset-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               inset: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset: {0};
                        """,
                }
            },
            {
                "-inset-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               inset: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        inset: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
