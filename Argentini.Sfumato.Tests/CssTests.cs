// ReSharper disable ConvertToPrimaryConstructor

using Argentini.Sfumato.Entities.CssClassProcessing;
using Argentini.Sfumato.Extensions;
using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class CssTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private AppState AppState { get; } = new();

    public CssTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    #region Constants

    private static string Css => """
                                    /* SFUMATO BROWSER RESET */
                                    *,
                                    ::before,
                                    ::after {
                                      box-sizing: border-box;
                                      border-width: 0;
                                      border-style: solid;
                                      border-color: transparent;
                                    }
                                    
                                    * {
                                      min-width: 0px;
                                      min-height: 0rem;
                                    }
                                    
                                    html {
                                      line-height: 1.5;
                                      -webkit-text-size-adjust: 100%;
                                      -moz-tab-size: 4;
                                      tab-size: 4;
                                      font-family: ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, "Aptos", "Segoe UI", Roboto, "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
                                    }
                                    
                                    /*# sourceMappingURL=app.css.map */
                                    
                                    """;
    
    #endregion

    [Fact]
    public void CompactCss()
    {
        var minified = Css.CompactCss();
        
        Assert.Equal(420, minified.Length);
    }
}
