namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class ScrollbarTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Scrollbar()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "scrollbar-h-hover",
                EscapedClassName = ".scrollbar-h-hover",
                Styles =
                        """
                        --sf-scrollbar-track-color: rgba(0, 0, 0, 0);
                        --sf-scrollbar-thumb-color: rgba(0, 0, 0, 0);
                        
                        --sf-scrollbar-track-hover-color: rgba(0, 0, 0, 0);
                        --sf-scrollbar-thumb-hover-color: rgba(0, 0, 0, 0.25);
                        
                        --sf-scrollbar-track-hover-dark-color: rgba(0, 0, 0, 0);
                        --sf-scrollbar-thumb-hover-dark-color: rgba(255, 255, 255, 0.35);
                        
                        --sf-scrollbar-size: 0.5rem;

                        display: block;
                        width: 100%;
                        overflow-x: scroll;
                        overflow-y: hidden;
                        -webkit-overflow-scrolling: touch;
                        
                        scrollbar-gutter: stable;

                        & > *:first-child {
                            margin-bottom: calc(var(--sf-scrollbar-size) * -1);
                        }

                        &::-webkit-scrollbar-corner {
                            background: transparent;
                        }

                        &::-webkit-scrollbar {
                            width: 0;
                            height: var(--sf-scrollbar-size);
                            cursor: pointer;
                        }

                        &::-webkit-scrollbar-track {
                            background-color: var(--sf-scrollbar-track-color);
                            cursor: pointer;
                        }

                        &::-webkit-scrollbar-thumb {
                            background-color: var(--sf-scrollbar-thumb-color);
                            border-radius: var(--sf-scrollbar-size);
                            cursor: pointer;
                        }

                        @-moz-document url-prefix() {
                            padding-bottom: var(--sf-scrollbar-size);
                        }

                        @supports (-webkit-touch-callout: none) {
                            padding-bottom: var(--sf-scrollbar-size);
                        }

                        @supports (scrollbar-color: red blue) {
                            * {
                                scrollbar-color: var(--sf-scrollbar-thumb-color) var(--sf-scrollbar-track-color);
                            }
                        }

                        &:hover {

                            &::-webkit-scrollbar-track {
                                background-color: var(--sf-scrollbar-track-hover-color);
                            }

                            &::-webkit-scrollbar-thumb {
                                background-color: var(--sf-scrollbar-thumb-hover-color);
                            }

                           @supports (scrollbar-color: red blue) {
                               * {
                                   scrollbar-color: var(--sf-scrollbar-thumb-hover-color) var(--sf-scrollbar-track-hover-color);
                                   scrollbar-width: thin;
                               }
                           }
                        }

                        @variant dark {

                            &:hover {

                                &::-webkit-scrollbar-track {
                                    background-color: var(--sf-scrollbar-track-hover-dark-color);
                                }
                               
                                &::-webkit-scrollbar-thumb {
                                    background-color: var(--sf-scrollbar-thumb-hover-dark-color);
                                }
                               
                                @supports (scrollbar-color: red blue) {
                                    * {
                                        scrollbar-color: var(--sf-scrollbar-thumb-hover-dark-color) var(--sf-scrollbar-track-hover-dark-color);
                                    }
                                }
                            }
                        }
                        """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scrollbar-v-hover",
                EscapedClassName = ".scrollbar-v-hover",
                Styles =
                        """
                        --sf-scrollbar-track-color: rgba(0, 0, 0, 0);
                        --sf-scrollbar-thumb-color: rgba(0, 0, 0, 0);
                        
                        --sf-scrollbar-track-hover-color: rgba(0, 0, 0, 0);
                        --sf-scrollbar-thumb-hover-color: rgba(0, 0, 0, 0.25);
                        
                        --sf-scrollbar-track-hover-dark-color: rgba(0, 0, 0, 0);
                        --sf-scrollbar-thumb-hover-dark-color: rgba(255, 255, 255, 0.35);
                        
                        --sf-scrollbar-size: 0.5rem;
                        
                        display: block;
                        height: 100%;
                        overflow-x: hidden;
                        overflow-y: scroll;
                        -webkit-overflow-scrolling: touch;
                        
                        scrollbar-gutter: stable;
                        
                        & > *:first-child {
                            margin-right: calc(var(--sf-scrollbar-size) * -1);
                        }
                        
                        &::-webkit-scrollbar-corner {
                            background: transparent;
                        }
                        
                        &::-webkit-scrollbar {
                            width: var(--sf-scrollbar-size);
                            height: 0;
                            cursor: pointer;
                        }
                        
                        &::-webkit-scrollbar-track {
                            background-color: var(--sf-scrollbar-track-color);
                            cursor: pointer;
                        }
                        
                        &::-webkit-scrollbar-thumb {
                            background-color: var(--sf-scrollbar-thumb-color);
                            border-radius: var(--sf-scrollbar-size);
                            cursor: pointer;
                        }
                        
                        @-moz-document url-prefix() {
                            padding-right: var(--sf-scrollbar-size);
                        }
                        
                        @supports (-webkit-touch-callout: none) {
                            padding-right: var(--sf-scrollbar-size);
                        }
                        
                        @supports (scrollbar-color: red blue) {
                            * {
                                scrollbar-color: var(--sf-scrollbar-thumb-color) var(--sf-scrollbar-track-color);
                            }
                        }
                        
                        &:hover {
                        
                            &::-webkit-scrollbar-track {
                                background-color: var(--sf-scrollbar-track-hover-color);
                            }

                            &::-webkit-scrollbar-thumb {
                                background-color: var(--sf-scrollbar-thumb-hover-color);
                            }

                            @supports (scrollbar-color: red blue) {
                                * {
                                    scrollbar-color: var(--sf-scrollbar-thumb-hover-color) var(--sf-scrollbar-track-hover-color);
                                    scrollbar-width: thin;
                                }
                            }
                        }
                        
                        @variant dark {
                        
                            &:hover {

                                &::-webkit-scrollbar-track {
                                    background-color: var(--sf-scrollbar-track-hover-dark-color);
                                }

                                &::-webkit-scrollbar-thumb {
                                    background-color: var(--sf-scrollbar-thumb-hover-dark-color);
                                }

                                @supports (scrollbar-color: red blue) {
                                    * {
                                        scrollbar-color: var(--sf-scrollbar-thumb-hover-dark-color) var(--sf-scrollbar-track-hover-dark-color);
                                    }
                                }
                            }
                        }
                        """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scrollbar-dark",
                EscapedClassName = ".scrollbar-dark",
                Styles =
                    """
                    &:hover {

                        &::-webkit-scrollbar-track {
                            background-color: var(--sf-scrollbar-track-hover-dark-color);
                        }

                        &::-webkit-scrollbar-thumb {
                            background-color: var(--sf-scrollbar-thumb-hover-dark-color);
                        }

                        @supports (scrollbar-color: red blue) {
                            * {
                                scrollbar-color: var(--sf-scrollbar-thumb-hover-dark-color) var(--sf-scrollbar-track-hover-dark-color);
                            }
                        }
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
