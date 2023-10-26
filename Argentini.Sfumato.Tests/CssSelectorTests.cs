using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Tests;

public class CssSelectorTests
{
    [Fact]
    public async Task InvalidEmpty()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "");

        await selector.ProcessSelector();
        
        Assert.True(selector.IsInvalid);
    }

    [Fact]
    public async Task InvalidBracketOrder()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "]dsfdfsfsd[");

        await selector.ProcessSelector();
        
        Assert.True(selector.IsInvalid);
    }

    [Fact]
    public async Task InvalidBracketContent()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dsfdfsfsd[]");
        
        await selector.ProcessSelector();
        
        Assert.True(selector.IsInvalid);
    }
    
    [Fact]
    public async Task BaseClass()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Empty(selector.ArbitraryValue);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task MediaQuery()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "tabp:text-base");

        await selector.ProcessSelector();

        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal("tabp", selector.MediaQueryVariants[0]);
        Assert.Empty(selector.ArbitraryValue);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("tabp\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task MediaQueries()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:text-base");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Empty(selector.ArbitraryValue);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("dark\\:tabp\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task PseudoClass()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "hover:text-base");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Empty(selector.ArbitraryValue);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("hover\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task PseudoClasses()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "hover:focus:text-base");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Equal(2, selector.PseudoClassVariants.Count);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Equal("focus", selector.PseudoClassVariants[1]);
        Assert.Empty(selector.ArbitraryValue);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("hover\\:focus\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task MixedPrefixes()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:hover:focus:text-base");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Equal(2, selector.PseudoClassVariants.Count);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Equal("focus", selector.PseudoClassVariants[1]);
        Assert.Empty(selector.ArbitraryValue);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("dark\\:tabp\\:hover\\:focus\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task MixedPrefixesReordered()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "focus:dark:hover:tabp:dark:text-base");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Equal(2, selector.PseudoClassVariants.Count);
        Assert.Equal("focus", selector.PseudoClassVariants[0]);
        Assert.Equal("hover", selector.PseudoClassVariants[1]);
        Assert.Empty(selector.ArbitraryValue);
        Assert.NotEqual(selector.Selector, selector.FixedSelector);
        Assert.Equal("dark:tabp:focus:hover:text-base", selector.FixedSelector);
        Assert.Equal("focus\\:dark\\:hover\\:tabp\\:dark\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClassCustomValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-[3rem]");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("3rem", selector.ArbitraryValue);
        Assert.Equal("text-\\[3rem\\]", selector.EscapedSelector);
        Assert.Equal("length", selector.ArbitraryValueType);
    }
    
    [Fact]
    public async Task BaseClassSlashCustomValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base/3");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("base", selector.CoreSegment);
        Assert.Equal("/3", selector.ModifierSegment);
        Assert.Equal("3", selector.ModifierValue);
        Assert.Equal(string.Empty, selector.ArbitraryValue);
        Assert.Equal("text-base\\/3", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClassCustomValueWithPrefixes()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:hover:text-[3rem]");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("3rem", selector.ArbitraryValue);
        Assert.Equal("dark\\:tabp\\:hover\\:text-\\[3rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClassSlashCustomValueWithPrefixes()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:hover:text-base/3");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("base", selector.CoreSegment);
        Assert.Equal("/3", selector.ModifierSegment);
        Assert.Equal("3", selector.ModifierValue);
        Assert.Equal(string.Empty, selector.ArbitraryValue);
        Assert.Equal("dark\\:tabp\\:hover\\:text-base\\/3", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task BaseClassSlashWithCustomValueWithPrefixes()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dark:tabp:hover:text-base/[3]");

        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("tabp", selector.MediaQueryVariants[1]);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("base", selector.CoreSegment);
        Assert.Equal("/", selector.ModifierSegment);
        Assert.Equal("3", selector.ArbitraryValue);
        Assert.Equal("dark\\:tabp\\:hover\\:text-base\\/\\[3\\]", selector.EscapedSelector);
        Assert.Equal("integer", selector.ArbitraryValueType);
    }
    
    [Fact]
    public async Task ArbitraryCss()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "[font-size:1rem]", true);

        await selector.ProcessSelector();
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal("font-size:1rem", selector.ArbitraryValue);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("\\[font-size\\:1rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task ArbitraryCssWithPrefix()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "tabp:[font-size:1rem]", true);

        await selector.ProcessSelector();
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Equal("tabp", selector.MediaQueryVariants[0]);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("font-size:1rem", selector.ArbitraryValue);
        Assert.Equal("tabp\\:\\[font-size\\:1rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public async Task ArbitraryCssWithSpaces()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "tabp:[padding:1rem_2rem]", true);

        await selector.ProcessSelector();
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Equal("tabp", selector.MediaQueryVariants[0]);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.Equal(selector.Selector, selector.FixedSelector);
        Assert.Equal("padding:1rem 2rem", selector.ArbitraryValue);
        Assert.Equal("tabp\\:\\[padding\\:1rem_2rem\\]", selector.EscapedSelector);
    }

    [Fact]
    public async Task TrailingSlash()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base/");

        await selector.ProcessSelector();

        Assert.True(selector.IsInvalid);
    }
    
    [Fact]
    public async Task IsImportant()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "!text-base");
        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!text-base");
        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!text-base/3");
        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("text", selector.PrefixSegment);
        Assert.Equal("base", selector.CoreSegment);
        Assert.Equal("/3", selector.ModifierSegment);
        Assert.Equal("3", selector.ModifierValue);
        Assert.Equal("integer", selector.ModifierValueType);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!text-base/[3]");
        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("text", selector.PrefixSegment);
        Assert.Equal("base", selector.CoreSegment);
        Assert.Equal("/", selector.ModifierSegment);
        Assert.Equal("3", selector.ArbitraryValue);
        Assert.Equal("integer", selector.ArbitraryValueType);
        Assert.True(selector.IsImportant);
    }
    
    /*
     * Structure of a CSS selector:
     * ============================
     *
     * Starts with optional variants:
     * {media-queries}: {pseudo-classes}:
     *
     * Followed by optional important flag
     * {!}
     *
     * Arbitrary CSS:
     * [{arbitrary-css}]
     *
     * OR
     *
     * Utility class:
     * Prefix, core, modifier, and arbitrary value segments:
     * {prefix}-{core} { /{modifier-value} or /[{arbitrary-modifier-value}] or -[{arbitrary-value}] }
     *
     * Segment Examples:
     * -----------------
     * text-base		  {prefix:text}-{core:base}
     * text-base/5        {prefix:text}-{core:base}/{modifier-value:5}
     * text-base/[3rem]   {prefix:text}-{core:base}/[{arbitrary-modifier-value:3rem}]
     * bg-[#aabbcc]       {prefix:bg}-[{arbitrary-value:#aabbcc}]
     * bg-rose/50         {prefix:bg}-{core:rose}/{modifier-value:5}
     * w-1/2	          {prefix:w}-{core:1/2}
     * sr-only	          {prefix:sr-only}
     *
     * Selector examples:
     * ------------------
     * tabp:focus:bg-rose/50
     * dark:tabp:hover:!text-base/5
     *
     */

    [Fact]
    public async Task BaseUtility()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClass);
        Assert.Equal("font-size: 1rem;".CompactCss(), selector.ScssUtilityClass.ScssMarkup.CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("text-base", selector.Selector);
        Assert.Equal("text-base", selector.EscapedSelector);
        Assert.Equal("text-base", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("text", selector.PrefixSegment);
        Assert.Equal("base", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithModifier()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base/5");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClass);
        Assert.Equal("font-size: 1rem; line-height: 1.25rem;".CompactCss(), selector.ScssUtilityClass.ScssMarkup.CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.True(selector.UsesModifier);
        Assert.True(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("text-base/5", selector.Selector);
        Assert.Equal("text-base\\/5", selector.EscapedSelector);
        Assert.Equal("text-base/5", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("text", selector.PrefixSegment);
        Assert.Equal("base", selector.CoreSegment);
        Assert.Equal("/5", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("5", selector.ModifierValue);
        Assert.Equal("integer", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithModifierArbitraryValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base/[3rem]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClass);
        Assert.Equal("font-size: 1rem; line-height: 3rem;".CompactCss(), selector.ScssUtilityClass.ScssMarkup.CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.True(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.True(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("text-base/[3rem]", selector.Selector);
        Assert.Equal("text-base\\/\\[3rem\\]", selector.EscapedSelector);
        Assert.Equal("text-base/[3rem]", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("text", selector.PrefixSegment);
        Assert.Equal("base", selector.CoreSegment);
        Assert.Equal("/", selector.ModifierSegment);

        Assert.Equal("3rem", selector.ArbitraryValue);
        Assert.Equal("length", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithArbitraryValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-[3rem]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClass);
        Assert.Equal("font-size: 3rem;".CompactCss(), selector.ScssUtilityClass.ScssMarkup.CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.True(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("text-[3rem]", selector.Selector);
        Assert.Equal("text-\\[3rem\\]", selector.EscapedSelector);
        Assert.Equal("text-[3rem]", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("text", selector.PrefixSegment);
        Assert.Equal("", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("3rem", selector.ArbitraryValue);
        Assert.Equal("length", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithArbitraryValue2()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-[#abcdef]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClass);
        Assert.Equal("color: #abcdef;".CompactCss(), selector.ScssUtilityClass.ScssMarkup.CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.True(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("text-[#abcdef]", selector.Selector);
        Assert.Equal("text-\\[\\#abcdef\\]", selector.EscapedSelector);
        Assert.Equal("text-[#abcdef]", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("text", selector.PrefixSegment);
        Assert.Equal("", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("#abcdef", selector.ArbitraryValue);
        Assert.Equal("color", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithArbitraryValueWithType()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-[color:#abcdef]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClass);
        Assert.Equal("color: #abcdef;".CompactCss(), selector.ScssUtilityClass.ScssMarkup.CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.True(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("text-[color:#abcdef]", selector.Selector);
        Assert.Equal("text-\\[color\\:\\#abcdef\\]", selector.EscapedSelector);
        Assert.Equal("text-[color:#abcdef]", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("text", selector.PrefixSegment);
        Assert.Equal("", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("#abcdef", selector.ArbitraryValue);
        Assert.Equal("color", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
}
