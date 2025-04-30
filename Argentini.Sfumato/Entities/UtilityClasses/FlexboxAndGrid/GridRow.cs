// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridRow : ClassDictionaryBase
{
    public GridRow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region row-span
            
            {
                "row-span-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-row: span {0} / span {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-row: span {0} / span {0};
                        """,
                }
            },
            {
                "row-span-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-row: 1 / -1;
                        """,
                }
            },
          
            #endregion

            #region row
            
            {
                "row-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-row: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-row: {0};
                        """,
                }
            },
            {
                "-row-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-row: calc({0} * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-row: calc({0} * -1);
                        """,
                }
            },
            {
                "row-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-row: auto;
                        """,
                }
            },
            
            #endregion
            
            #region row-start

            {
                "row-start-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-row-start: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-row-start: {0};
                        """,
                }
            },
            {
                "-row-start-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-row-start: calc({0} * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-row-start: calc({0} * -1);
                        """,
                }
            },
            {
                "row-start-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-row-start: auto;
                        """,
                }
            },
            
            #endregion
            
            #region row-end
            
            {
                "row-end-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-row-end: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-row-end: {0};
                        """,
                }
            },
            {
                "-row-end-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-row-end: calc({0} * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-row-end: calc({0} * -1);
                        """,
                }
            },
            {
                "row-end-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-row-end: auto;
                        """,
                }
            },
            
            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}