// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class Scrollbar : ClassDictionaryBase
{
    public Scrollbar()
    {
        Group = "scrollbar";
        Description = "Utilities for styling the browser scrollbar.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "scrollbar-h-hover", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-scrollbar-track-color: rgba(0, 0, 0, 0);
                        --sf-scrollbar-thumb-color: rgba(0, 0, 0, 0);

                        --sf-scrollbar-track-hover-color: rgba(0, 0, 0, 0.07);
                        --sf-scrollbar-thumb-hover-color: rgba(0, 0, 0, 0.15);

                        --sf-scrollbar-track-hover-dark-color: rgba(255, 255, 255, 0.15);
                        --sf-scrollbar-thumb-hover-dark-color: rgba(255, 255, 255, 0.20);

                        --sf-scrollbar-size: 0.5rem;

                        display: block;
                        position: relative;
                        width: 100%;
                        overflow-x: scroll;
                        overflow-y: hidden;

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
                        """
                }
            },
            {
                "scrollbar-v-hover", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-scrollbar-track-color: rgba(0, 0, 0, 0);
                        --sf-scrollbar-thumb-color: rgba(0, 0, 0, 0);
                        
                        --sf-scrollbar-track-hover-color: rgba(0, 0, 0, 0.07);
                        --sf-scrollbar-thumb-hover-color: rgba(0, 0, 0, 0.15);
                        
                        --sf-scrollbar-track-hover-dark-color: rgba(255, 255, 255, 0.15);
                        --sf-scrollbar-thumb-hover-dark-color: rgba(255, 255, 255, 0.20);
                        
                        --sf-scrollbar-size: 0.5rem;
                        
                        display: block;
                        position: relative;
                        height: 100%;
                        overflow-x: hidden;
                        overflow-y: scroll;
                        
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
                        """
                }
            },
            {
                "scrollbar-dark", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    SelectorSort = 2,
                    Template =
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
                        """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}