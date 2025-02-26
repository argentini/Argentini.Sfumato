using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Tests;

public class CssSelectorTests
{
    #region Invalid Selectors
    
    [Fact]
    public async Task InvalidEmpty()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, string.Empty);

        await selector.ProcessSelectorAsync();
        
        Assert.True(selector.IsInvalid);
    }

    [Fact]
    public async Task InvalidBracketOrder()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "]dsfdfsfsd[");

        await selector.ProcessSelectorAsync();
        
        Assert.True(selector.IsInvalid);
    }

    [Fact]
    public async Task InvalidWebLinkNotClass()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "pages/index.html");

        await selector.ProcessSelectorAsync();
        
        Assert.True(selector.IsInvalid);
    }
    
    [Fact]
    public async Task InvalidBracketContent()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "dsfdfsfsd[]");
        
        await selector.ProcessSelectorAsync();
        
        Assert.True(selector.IsInvalid);
    }

    [Fact]
    public async Task InvalidTrailingSlash()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-base/");

        await selector.ProcessSelectorAsync();

        Assert.True(selector.IsInvalid);
    }
    
    #endregion

    #region Arbitrary CSS

    [Fact]
    public async Task ArbitraryCss()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "p-[1rem]");

        await selector.ProcessSelectorAsync();
        selector.GetStyles();
        
        Assert.Equal("padding: 1rem;", selector.ScssMarkup);
        
        selector = new CssSelector(appState, "[font-size:1rem]", true);

        await selector.ProcessSelectorAsync();

        Assert.Null(selector.ScssUtilityClassGroup);
        Assert.True(selector.IsArbitraryCss);
        Assert.Equal("\\[font-size\\:1rem\\]", selector.EscapedSelector);
        Assert.Equal("font-size:1rem;".CompactCss(), selector.GetStyles().CompactCss());
    }
    
    [Fact]
    public async Task ArbitraryCssWithPrefix()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "sm:[font-size:1rem]", true);

        await selector.ProcessSelectorAsync();

        Assert.Null(selector.ScssUtilityClassGroup);
        Assert.True(selector.IsArbitraryCss);
        Assert.Equal("font-size:1rem", selector.ArbitraryValue);
        Assert.Equal("sm\\:\\[font-size\\:1rem\\]", selector.EscapedSelector);
        Assert.Equal("font-size:1rem;".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.Single(selector.MediaQueryVariants);
        Assert.Equal("sm", selector.MediaQueryVariants[0]);
        Assert.Empty(selector.PseudoClassVariants);
    }
    
    [Fact]
    public async Task ArbitraryCssWithSpaces()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "[font-size:_1rem]", true);

        await selector.ProcessSelectorAsync();

        Assert.Null(selector.ScssUtilityClassGroup);
        Assert.True(selector.IsArbitraryCss);
        Assert.Equal("\\[font-size\\:_1rem\\]", selector.EscapedSelector);
        Assert.Equal("font-size: 1rem;".CompactCss(), selector.GetStyles().CompactCss());
    }
    
    #endregion
    
    #region IsImportant
    
    [Fact]
    public async Task IsImportant()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "!bg-rose-100");
        await selector.ProcessSelectorAsync();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.True(selector.IsImportant);
        
        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1) !important;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "sm:focus:!bg-rose-100");
        await selector.ProcessSelectorAsync();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.True(selector.IsImportant);

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1) !important;".CompactCss(), selector.GetStyles().CompactCss());

        selector = new CssSelector(appState, "sm:focus:!bg-rose-100/50");
        await selector.ProcessSelectorAsync();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("/50", selector.ModifierSegment);
        Assert.Equal("50", selector.ModifierValue);
        Assert.Equal("integer", selector.ModifierValueType);
        Assert.True(selector.IsImportant);

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.5) !important;".CompactCss(), selector.GetStyles().CompactCss());

        selector = new CssSelector(appState, "sm:focus:!bg-rose-100/[50]");
        await selector.ProcessSelectorAsync();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("/", selector.ModifierSegment);
        Assert.Equal("50", selector.ArbitraryValue);
        Assert.Equal("integer", selector.ArbitraryValueType);
        Assert.True(selector.IsImportant);

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.5) !important;".CompactCss(), selector.GetStyles().CompactCss());
    }
    
    #endregion

    #region IsImportant
    
    [Fact]
    public async Task IsNegative()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "-w-full");
        await selector.ProcessSelectorAsync();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.True(selector.IsNegative);
        
        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("width:-100%;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "sm:focus:-w-full");
        await selector.ProcessSelectorAsync();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        Assert.True(selector.IsNegative);

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("width:-100%;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "-gap-x-4");
        await selector.ProcessSelectorAsync();
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        Assert.True(selector.IsNegative);
        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("column-gap:-1rem;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "-gap-x-[0.75rem]");
        await selector.ProcessSelectorAsync();
        
        Assert.True(selector.IsNegative);
        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("column-gap:-0.75rem;".CompactCss(), selector.GetStyles().CompactCss());
    }
    
    #endregion

    #region Base Utilities
    
    [Fact]
    public async Task BaseUtility()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-rose-100");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1);".CompactCss(), selector.GetStyles().CompactCss());
        
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
        
        selector = new CssSelector(appState, "backdrop-blur");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("--sf-backdrop-blur:blur(0.5rem);".CompactCss(), selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task BaseUtilityWithSlashInName()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "w-1/2");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("width: 50%;".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.True(selector.UsesModifier);
        Assert.True(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("w-1/2", selector.Selector);
        Assert.Equal("w-1\\/2", selector.EscapedSelector);
        Assert.Equal("w-1/2", selector.FixedSelector);

        Assert.Equal("", selector.VariantSegment);
        Assert.Equal("w", selector.PrefixSegment);
        Assert.Equal("1", selector.CoreSegment);
        Assert.Equal("/2", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("2", selector.ModifierValue);
        Assert.Equal("integer", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithModifier()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-rose-100/50");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.5);".CompactCss(), selector.GetStyles().CompactCss());
        
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
        Assert.Equal("rose-100", selector.CoreSegment);
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

        await selector.ProcessSelectorAsync();

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
    public async Task BaseUtilityWithArbitraryModifierValue()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-rose-100/[50]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.5);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
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
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("/", selector.ModifierSegment);

        Assert.Equal("50", selector.ArbitraryValue);
        Assert.Equal("integer", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithArbitraryValueWithExplicitType()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-[color:#abcdef]");

        await selector.ProcessSelectorAsync();

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
    
    [Fact]
    public async Task BaseUtilityWithMediaQuery()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "sm:bg-rose-100");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Single(selector.MediaQueryVariants);
        Assert.Empty(selector.PseudoClassVariants);
        
        Assert.Equal("sm:bg-rose-100", selector.Selector);
        Assert.Equal("sm\\:bg-rose-100", selector.EscapedSelector);
        Assert.Equal("sm:bg-rose-100", selector.FixedSelector);

        Assert.Equal("sm:", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }

    [Fact]
    public async Task BaseUtilityWithMediaQueries()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "sm:hover:bg-rose-100");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Single(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        
        Assert.Equal("sm:hover:bg-rose-100", selector.Selector);
        Assert.Equal("sm\\:hover\\:bg-rose-100", selector.EscapedSelector);
        Assert.Equal("sm:hover:bg-rose-100", selector.FixedSelector);

        Assert.Equal("sm:hover:", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
        
        selector = new CssSelector(appState, "supports-backdrop-blur:bg-rose-100/60");

        await selector.ProcessSelectorAsync();
        
        Assert.Single(selector.MediaQueryVariants);
        Assert.Equal("supports-backdrop-blur", selector.MediaQueryVariants[0]);
    }
    
    [Fact]
    public async Task BaseUtilityWithMediaQueriesBadOrder()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "sm:hover:dark:bg-rose-100");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Equal(2, selector.MediaQueryVariants.Count);
        Assert.Equal("dark", selector.MediaQueryVariants[0]);
        Assert.Equal("sm", selector.MediaQueryVariants[1]);
        Assert.Single(selector.PseudoClassVariants);
        
        Assert.Equal("sm:hover:dark:bg-rose-100", selector.Selector);
        Assert.Equal("sm\\:hover\\:dark\\:bg-rose-100", selector.EscapedSelector);
        Assert.Equal("dark:sm:hover:bg-rose-100", selector.FixedSelector);

        Assert.Equal("sm:hover:dark:", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithPseudoclass()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "focus:bg-rose-100");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Single(selector.PseudoClassVariants);
        
        Assert.Equal("focus:bg-rose-100", selector.Selector);
        Assert.Equal("focus\\:bg-rose-100", selector.EscapedSelector);
        Assert.Equal("focus:bg-rose-100", selector.FixedSelector);

        Assert.Equal("focus:", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    [Fact]
    public async Task BaseUtilityWithPseudoclasses()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "hover:focus:bg-rose-100");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1);".CompactCss(), selector.GetStyles().CompactCss());
        
        Assert.False(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.False(selector.IsImportant);
        Assert.False(selector.UsesModifier);
        Assert.False(selector.HasModifierValue);
        Assert.False(selector.HasArbitraryValue);

        Assert.Empty(selector.MediaQueryVariants);
        Assert.Equal(2, selector.PseudoClassVariants.Count);
        Assert.Equal("hover", selector.PseudoClassVariants[0]);
        Assert.Equal("focus", selector.PseudoClassVariants[1]);
        
        Assert.Equal("hover:focus:bg-rose-100", selector.Selector);
        Assert.Equal("hover\\:focus\\:bg-rose-100", selector.EscapedSelector);
        Assert.Equal("hover:focus:bg-rose-100", selector.FixedSelector);

        Assert.Equal("hover:focus:", selector.VariantSegment);
        Assert.Equal("bg", selector.PrefixSegment);
        Assert.Equal("rose-100", selector.CoreSegment);
        Assert.Equal("", selector.ModifierSegment);

        Assert.Equal("", selector.ArbitraryValue);
        Assert.Equal("", selector.ArbitraryValueType);
        Assert.Equal("", selector.ModifierValue);
        Assert.Equal("", selector.ModifierValueType);
    }
    
    #endregion
    
    #region Value Type Detection

    [Fact]
    public void GetUserClassValueType()
    {
        var appState = new SfumatoAppState();
        var cssSelector = new CssSelector(appState, string.Empty);

        var package = cssSelector.SetCustomValueType("", appState);
        Assert.Equal(string.Empty, package.ValueType);
        
        package = cssSelector.SetCustomValueType("[width:3rem]", appState);
        Assert.Equal(string.Empty, package.ValueType);
        
        package = cssSelector.SetCustomValueType("[3rem]", appState);
        Assert.Equal("length", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[3fr]", appState);
        Assert.Equal("flex", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[3%]", appState);
        Assert.Equal("percentage", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[3.5%]", appState);
        Assert.Equal("percentage", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[3]", appState);
        Assert.Equal("integer", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[0.5]", appState);
        Assert.Equal("number", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[3.0]", appState);
        Assert.Equal("number", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[#123]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[#123f]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[#aa1122]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[#aa1122ff]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[rgb(1,2,3)]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[rgba(1,2,3,0.5)]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = cssSelector.SetCustomValueType("['hello_world!']", appState);
        Assert.Equal("string", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[url(http://image.src)]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[http://sfumato.org/images/file.jpg]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[https://sfumato.org/images/file.jpg]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[url(/images/file.jpg)]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[/images/file.jpg]", appState);
        Assert.Equal("url", package.ValueType);

        package = cssSelector.SetCustomValueType("[./images/file.jpg]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = cssSelector.SetCustomValueType("[../images/file.jpg]", appState);
        Assert.Equal("url", package.ValueType);

        foreach (var unit in appState.CssAngleUnits)
        {
            package = cssSelector.SetCustomValueType($"[1{unit}]", appState);
            Assert.Equal("angle", package.ValueType);
        }

        foreach (var unit in appState.CssTimeUnits)
        {
            package = cssSelector.SetCustomValueType($"[100{unit}]", appState);
            Assert.Equal("time", package.ValueType);
        }

        foreach (var unit in appState.CssFrequencyUnits)
        {
            package = cssSelector.SetCustomValueType($"[1{unit}]", appState);
            Assert.Equal("frequency", package.ValueType);
        }

        foreach (var unit in appState.CssResolutionUnits)
        {
            package = cssSelector.SetCustomValueType($"[1024{unit}]", appState);
            Assert.Equal("resolution", package.ValueType);
        }
        
        package = cssSelector.SetCustomValueType("[1/2]", appState);
        Assert.Equal("ratio", package.ValueType);

        package = cssSelector.SetCustomValueType("[1_/_2]", appState);
        Assert.Equal("ratio", package.ValueType);
    }
    
    #endregion
}
