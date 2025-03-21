// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.Typography;

public sealed class Display : ClassDictionaryBase
{
    public Display()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>()
        {
            {
                "inline", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: inline;
                               """
                }
            },
            {
                "block", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: block;
                               """
                }
            },
            {
                "inline-block", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: inline-block;
                               """
                }
            },
            {
                "flow-root", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: flow-root;
                               """
                }
            },
            {
                "flex", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: flex;
                               """
                }
            },
            {
                "inline-flex", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: inline-flex;
                               """
                }
            },
            {
                "grid", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: grid;
                               """
                }
            },
            {
                "inline-grid", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: inline-grid;
                               """
                }
            },
            {
                "contents", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: contents;
                               """
                }
            },
            {
                "table", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table;
                               """
                }
            },
            {
                "inline-table", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: inline-table;
                               """
                }
            },
            {
                "table-caption", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table-caption;
                               """
                }
            },
            {
                "table-cell", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table-cell;
                               """
                }
            },
            {
                "table-column", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table-column;
                               """
                }
            },
            {
                "table-column-group", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table-column-group;
                               """
                }
            },
            {
                "table-footer-group", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table-footer-group;
                               """
                }
            },
            {
                "table-header-group", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table-header-group;
                               """
                }
            },
            {
                "table-row-group", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table-row-group;
                               """
                }
            },
            {
                "table-row", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: table-row;
                               """
                }
            },
            {
                "list-item", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: list-item;
                               """
                }
            },
            {
                "hidden", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               display: none;
                               """
                }
            },
            {
                "sr-only", new ClassDefinition
                {
                    IsSimpleUtility = true,
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
                    IsSimpleUtility = true,
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
}