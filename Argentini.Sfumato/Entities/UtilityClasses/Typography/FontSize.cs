// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontSize : ClassDictionaryBase
{
    public FontSize()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-", new ClassDefinition
                {
                    UsesDimensionLength = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: {0};
                               """,
                    ModifierTemplate = """
                                       font-size: {0};
                                       line-height: calc(var(--spacing) * {1});
                                       """
                }
            },
            {
                "text-xs", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-xs);
                               line-height: var(--text-xs--line-height);
                               """,
                    ModifierTemplate = """
                               --text-xs--line-height: {1};
                               font-size: var(--text-xs);
                               line-height: var(--text-xs--line-height);
                               """
                }
            },
            {
                "text-sm", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-sm);
                               line-height: var(--text-sm--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-sm--line-height: {1};
                                       font-size: var(--text-sm);
                                       line-height: var(--text-sm--line-height);
                                       """
                }
            },
            {
                "text-base", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-base);
                               line-height: var(--text-base--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-base--line-height: {1};
                                       font-size: var(--text-base);
                                       line-height: var(--text-base--line-height);
                                       """
                }
            },
            {
                "text-lg", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-lg);
                               line-height: var(--text-lg--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-lg--line-height: {1};
                                       font-size: var(--text-lg);
                                       line-height: var(--text-lg--line-height);
                                       """
                }
            },
            {
                "text-xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-xl);
                               line-height: var(--text-xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-xl--line-height: {1};
                                       font-size: var(--text-xl);
                                       line-height: var(--text-xl--line-height);
                                       """
                }
            },
            {
                "text-2xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-2xl);
                               line-height: var(--text-2xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-2xl--line-height: {1};
                                       font-size: var(--text-2xl);
                                       line-height: var(--text-2xl--line-height);
                                       """
                }
            },
            {
                "text-3xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-3xl);
                               line-height: var(--text-3xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-3xl--line-height: {1};
                                       font-size: var(--text-3xl);
                                       line-height: var(--text-3xl--line-height);
                                       """
                }
            },
            {
                "text-4xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-4xl);
                               line-height: var(--text-4xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-4xl--line-height: {1};
                                       font-size: var(--text-4xl);
                                       line-height: var(--text-4xl--line-height);
                                       """
                }
            },
            {
                "text-5xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-5xl);
                               line-height: var(--text-5xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-5xl--line-height: {1};
                                       font-size: var(--text-5xl);
                                       line-height: var(--text-5xl--line-height);
                                       """
                }
            },
            {
                "text-6xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-6xl);
                               line-height: var(--text-6xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-6xl--line-height: {1};
                                       font-size: var(--text-6xl);
                                       line-height: var(--text-6xl--line-height);
                                       """
                }
            },
            {
                "text-7xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-7xl);
                               line-height: var(--text-7xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-7xl--line-height: {1};
                                       font-size: var(--text-7xl);
                                       line-height: var(--text-7xl--line-height);
                                       """
                }
            },
            {
                "text-8xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-8xl);
                               line-height: var(--text-8xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-8xl--line-height: {1};
                                       font-size: var(--text-8xl);
                                       line-height: var(--text-8xl--line-height);
                                       """
                }
            },
            {
                "text-9xl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template = """
                               font-size: var(--text-9xl);
                               line-height: var(--text-9xl--line-height);
                               """,
                    ModifierTemplate = """
                                       --text-9xl--line-height: {1};
                                       font-size: var(--text-9xl);
                                       line-height: var(--text-9xl--line-height);
                                       """
                }
            },
        });
    }
}