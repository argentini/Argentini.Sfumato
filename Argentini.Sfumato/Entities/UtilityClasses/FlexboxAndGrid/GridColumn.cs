// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridColumn : ClassDictionaryBase
{
    public GridColumn()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region col-span
            
            {
                "col-span-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-column: span {0} / span {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-column: span {0} / span {0};
                        """,
                }
            },
            {
                "col-span-full", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-column: 1 / -1;
                        """,
                }
            },
          
            #endregion

            #region col
            
            {
                "col-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-column: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-column: {0};
                        """,
                }
            },
            {
                "-col-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-column: calc({0} * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-column: calc({0} * -1);
                        """,
                }
            },
            {
                "col-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-column: auto;
                        """,
                }
            },
            
            #endregion
            
            #region col-start

            {
                "col-start-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-column-start: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-column-start: {0};
                        """,
                }
            },
            {
                "-col-start-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-column-start: calc({0} * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-column-start: calc({0} * -1);
                        """,
                }
            },
            {
                "col-start-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-column-start: auto;
                        """,
                }
            },
            
            #endregion
            
            #region col-end
            
            {
                "col-end-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-column-end: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-column-end: {0};
                        """,
                }
            },
            {
                "-col-end-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-column-end: calc({0} * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-column-end: calc({0} * -1);
                        """,
                }
            },
            {
                "col-end-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-column-end: auto;
                        """,
                }
            },
            
            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}