using Argentini.Sfumato.Entities;

namespace Argentini.Sfumato.Tests;

public class CssSelectorTests
{
    [Fact]
    public async Task InvalidEmpty()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "");
        
        Assert.True(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Empty(selector.RootClassSegment);
        Assert.Empty(selector.CustomValueSegment);
        Assert.Empty(selector.FixedSelector);
        Assert.Empty(selector.EscapedSelector);
    }

    [Fact]
    public async Task InvalidBracketOrder()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "]dsfdfsfsd[");
        
        Assert.True(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Empty(selector.RootClassSegment);
        Assert.Empty(selector.FixedSelector);
        Assert.Empty(selector.CustomValueSegment);
        Assert.Empty(selector.EscapedSelector);
    }

    [Fact]
    public async Task InvalidBracketContent()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dsfdfsfsd[]");
        
        Assert.True(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Empty(selector.RootClassSegment);
        Assert.Empty(selector.CustomValueSegment);
        Assert.Empty(selector.FixedSelector);
        Assert.Empty(selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClass()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Empty(selector.CustomValueSegment);
        Assert.Equal(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task MediaQuery()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "tabp:text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal("tabp", selector.MediaQueryVariants[0]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base", selector.RootClassSegment);
        Assert.Equal("tabp\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task MediaQueries()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base", selector.RootClassSegment);
        Assert.Equal("dark\\:tabp\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task PseudoClass()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "hover:text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base", selector.RootClassSegment);
        Assert.Equal("hover\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task PseudoClasses()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "hover:focus:text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Equal(2, selector.PseudoClassVariants.Count);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Equal("focus", selector.PseudoClassVariants[1]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base", selector.RootClassSegment);
        Assert.Equal("hover\\:focus\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task MixedPrefixes()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:hover:focus:text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Equal(2, selector.PseudoClassVariants.Count);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Equal("focus", selector.PseudoClassVariants[1]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base", selector.RootClassSegment);
        Assert.Equal("dark\\:tabp\\:hover\\:focus\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task MixedPrefixesReordered()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "focus:dark:hover:tabp:dark:text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Equal(2, selector.PseudoClassVariants.Count);
        Assert.Equal("focus", selector.PseudoClassVariants[0]);
        Assert.Equal("hover", selector.PseudoClassVariants[1]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.NotEqual(selector.Selector, selector.FixedSelector);
        Assert.Equal("dark:tabp:focus:hover:text-base", selector.FixedSelector);
        Assert.Equal("text-base", selector.RootClassSegment);
        Assert.Equal("focus\\:dark\\:hover\\:tabp\\:dark\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClassCustomValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-[3rem]");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-", selector.RootClassSegment);
        Assert.Equal("[3rem]", selector.CustomValueSegment);
        Assert.Equal("text-\\[3rem\\]", selector.EscapedSelector);
        Assert.Equal("length", selector.ArbitraryValueType);
    }
    
    [Fact]
    public async Task BaseClassSlashCustomValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base/3");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base/3", selector.RootClassSegment);
        Assert.Equal(string.Empty, selector.CustomValueSegment);
        Assert.Equal("text-base\\/3", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClassCustomValueWithPrefixes()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:hover:text-[3rem]");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-", selector.RootClassSegment);
        Assert.Equal("[3rem]", selector.CustomValueSegment);
        Assert.Equal("dark\\:tabp\\:hover\\:text-\\[3rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClassSlashCustomValueWithPrefixes()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:hover:text-base/3");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base/3", selector.RootClassSegment);
        Assert.Equal(string.Empty, selector.CustomValueSegment);
        Assert.Equal("dark\\:tabp\\:hover\\:text-base\\/3", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClassSlashWithCustomValueWithPrefixes()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:hover:text-base/[3]");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.NotEqual(selector.Selector, selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base/", selector.RootClassSegment);
        Assert.Equal("[3]", selector.CustomValueSegment);
        Assert.Equal("dark\\:tabp\\:hover\\:text-base\\/\\[3\\]", selector.EscapedSelector);
        Assert.Equal("integer", selector.ArbitraryValueType);
    }
    
    [Fact]
    public async Task ArbitraryCss()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "[font-size:1rem]");
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Empty(selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.CustomValueSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("\\[font-size\\:1rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task ArbitraryCssWithPrefix()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "tabp:[font-size:1rem]");
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Equal("tabp", selector.MediaQueryVariants[0]);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Empty(selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("[font-size:1rem]", selector.CustomValueSegment);
        Assert.Equal("tabp\\:\\[font-size\\:1rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task ArbitraryCssWithSpaces()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "tabp:[padding:1rem_2rem]");
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Equal("tabp", selector.MediaQueryVariants[0]);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Empty(selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("[padding:1rem_2rem]", selector.CustomValueSegment);
        Assert.Equal("tabp\\:\\[padding\\:1rem_2rem\\]", selector.EscapedSelector);
    }

    [Fact]
    public async Task TrailingSlash()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base/");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal("text-base/", selector.RootClassSegment);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base\\/", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task IsImportant()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "!text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal("text-base", selector.RootClassSegment);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!text-base");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("text-base", selector.RootClassSegment);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!text-base/3");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("text-base/3", selector.RootClassSegment);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!text-base/[3]");
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("text-base/", selector.RootClassSegment);
        Assert.Equal("[3]", selector.CustomValueSegment);
        Assert.Equal("3", selector.ArbitraryValue);
        Assert.True(selector.IsImportant);
    }
}
