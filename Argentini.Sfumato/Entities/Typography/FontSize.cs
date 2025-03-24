// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.Typography;

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
                               font-size: var(--text-xs);
                               line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-sm);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-base);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-lg);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-xl);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-2xl);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-3xl);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-4xl);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-5xl);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-6xl);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-7xl);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-8xl);
                                       line-height: calc(var(--spacing) * {1});
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
                                       font-size: var(--text-9xl);
                                       line-height: calc(var(--spacing) * {1});
                                       """
                }
            },
        });
    }
}