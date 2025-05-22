// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Display : ClassDictionaryBase
{
    public Display()
    {
        Group = "display";
        Description = "Utilities for setting how elements are displayed.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: inline;
                               """
                }
            },
            {
                "block", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: block;
                               """
                }
            },
            {
                "inline-block", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: inline-block;
                               """
                }
            },
            {
                "flow-root", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: flow-root;
                               """
                }
            },
            {
                "flex", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: flex;
                               """
                }
            },
            {
                "inline-flex", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: inline-flex;
                               """
                }
            },
            {
                "grid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: grid;
                               """
                }
            },
            {
                "inline-grid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: inline-grid;
                               """
                }
            },
            {
                "contents", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: contents;
                               """
                }
            },
            {
                "table", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table;
                               """
                }
            },
            {
                "inline-table", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: inline-table;
                               """
                }
            },
            {
                "table-caption", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table-caption;
                               """
                }
            },
            {
                "table-cell", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table-cell;
                               """
                }
            },
            {
                "table-column", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table-column;
                               """
                }
            },
            {
                "table-column-group", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table-column-group;
                               """
                }
            },
            {
                "table-footer-group", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table-footer-group;
                               """
                }
            },
            {
                "table-header-group", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table-header-group;
                               """
                }
            },
            {
                "table-row-group", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table-row-group;
                               """
                }
            },
            {
                "table-row", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: table-row;
                               """
                }
            },
            {
                "list-item", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: list-item;
                               """
                }
            },
            {
                "hidden", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               display: none;
                               """
                }
            },
            {
                "sr-only", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               position: absolute;
                               width: 1px;
                               height: 1px;
                               padding: 0;
                               margin: -1px;
                               overflow: hidden;
                               clip: rect(0, 0, 0, 0);
                               white-space: nowrap;
                               border-width: 0;
                               """
                }
            },
            {
                "not-sr-only", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               position: static;
                               width: auto;
                               height: auto;
                               padding: 0;
                               margin: 0;
                               overflow: visible;
                               clip: auto;
                               white-space: normal;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}