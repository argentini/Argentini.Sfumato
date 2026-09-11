// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Container : ClassDictionaryBase
{
    public Container()
    {
        Group = "container-type";
        Description = "Utilities for constraining and centering layout containers.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "@container", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        container-type: inline-size;
                        """,
                    ModifierTemplate = 
                        """
                        container: {1} / inline-size;
                        """,
                }
            },
            {
                "@@container", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        container-type: inline-size;
                        """,
                    ModifierTemplate = 
                        """
                        container: {1} / inline-size;
                        """,
                    IsRazorSyntax = true,
                }
            },
            {
                "@container-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        container-type: normal;
                        """,
                    ModifierTemplate =
                        """
                        container: {1};
                        """,
                }
            },
            {
                "@@container-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        container-type: normal;
                        """,
                    ModifierTemplate =
                        """
                        container: {1};
                        """,
                    IsRazorSyntax = true,
                }
            },
            {
                "@container-size", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        container-type: size;
                        """,
                    ModifierTemplate =
                        """
                        container: {1} / size;
                        """,
                }
            },
            {
                "@@container-size", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        container-type: size;
                        """,
                    ModifierTemplate =
                        """
                        container: {1} / size;
                        """,
                    IsRazorSyntax = true,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
