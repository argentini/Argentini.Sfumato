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
    public async Task InvalidTrailingSlash()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base/");

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
    public async Task IsImportant()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "!bg-rose-100");
        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!bg-rose-100");
        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!bg-rose-100/50");
        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100/50", selector.CoreSegment);
        Assert.Equal("/50", selector.ModifierSegment);
        Assert.Equal("50", selector.ModifierValue);
        Assert.Equal("integer", selector.ModifierValueType);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector(appState, "tabp:focus:!bg-rose-100/[50]");
        await selector.ProcessSelector();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100/", selector.CoreSegment);
        Assert.Equal("/", selector.ModifierSegment);
        Assert.Equal("50", selector.ArbitraryValue);
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

        var selector = new CssSelector(appState, "bg-rose-100");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgb(255,228,230);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("bg-rose-100", selector.Selector);
        Assert.Equal("bg-rose-100", selector.EscapedSelector);
        Assert.Equal("bg-rose-100", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }

    [Fact]
    public async Task BaseUtilityWithSlashInName()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "w-1/2");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("width: 50%;".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("w-1/2", selector.Selector);
        Assert.Equal("w-1\\/2", selector.EscapedSelector);
        Assert.Equal("w-1/2", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("w", selector.PrefixSegment);
        Assert.Equal("1/2", selector.CoreSegment);
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

        var selector = new CssSelector(appState, "bg-rose-100/50");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.50);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.True(selector.UsesModifier);
        Assert.True(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("bg-rose-100/50", selector.Selector);
        Assert.Equal("bg-rose-100\\/50", selector.EscapedSelector);
        Assert.Equal("bg-rose-100/50", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100/50", selector.CoreSegment);
        Assert.Equal("/50", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("50", selector.ModifierValue);
        Assert.Equal("integer", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithArbitraryValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-[#aabbcc]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: #aabbcc;".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.True(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("bg-[#aabbcc]", selector.Selector);
        Assert.Equal("bg-\\[\\#aabbcc\\]", selector.EscapedSelector);
        Assert.Equal("bg-[#aabbcc]", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("#aabbcc", selector.ArbitraryValue);
        Assert.Equal("color", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithArbitrarySlashValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-rose-100/[50]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.50);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.True(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.True(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("bg-rose-100/[50]", selector.Selector);
        Assert.Equal("bg-rose-100\\/\\[50\\]", selector.EscapedSelector);
        Assert.Equal("bg-rose-100/[50]", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100/", selector.CoreSegment);
        Assert.Equal("/", selector.ModifierSegment);

        Assert.Equal("50", selector.ArbitraryValue);
        Assert.Equal("integer", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithArbitraryValueWithType()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-[color:#abcdef]");

        await selector.ProcessSelector();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: #abcdef;".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.True(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("bg-[color:#abcdef]", selector.Selector);
        Assert.Equal("bg-\\[color\\:\\#abcdef\\]", selector.EscapedSelector);
        Assert.Equal("bg-[color:#abcdef]", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("#abcdef", selector.ArbitraryValue);
        Assert.Equal("color", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
}
