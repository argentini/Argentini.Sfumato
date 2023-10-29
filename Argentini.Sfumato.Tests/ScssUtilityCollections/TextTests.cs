using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Tests.ScssUtilityCollections;

public class TextTests
{
    [Fact]
    public async Task StaticValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-left");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("text-align: left;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-center");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("text-align: center;".CompactCss(), selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task CalculatedValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-rose-100");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: rgba(255,228,230,1.0);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-lg");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 1.125rem; line-height: 1.75rem;".CompactCss(), selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task ModifierValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-rose-100/50");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: rgba(255,228,230,0.50);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-rose-100/[50]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: rgba(255,228,230,0.50);".CompactCss(), selector.GetStyles().CompactCss());        

        selector = new CssSelector(appState, "text-rose-100/[0.75]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: rgba(255,228,230,0.75);".CompactCss(), selector.GetStyles().CompactCss());        
        
        selector = new CssSelector(appState, "text-lg/[0.5]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 1.125rem; line-height: 0.5;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-lg/[3rem]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 1.125rem; line-height: 3rem;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-lg/[110%]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 1.125rem; line-height: 110%;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-lg/loose");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 1.125rem; line-height: 2;".CompactCss(), selector.GetStyles().CompactCss());
    }
    
    [Fact]
    public async Task ArbitraryValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-[#aabbcc]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: #aabbcc;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-[18px]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 18px;".CompactCss(), selector.GetStyles().CompactCss());

        selector = new CssSelector(appState, "text-[110%]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 110%;".CompactCss(), selector.GetStyles().CompactCss());
    }
}
