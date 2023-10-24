using Argentini.Sfumato.Entities;

namespace Argentini.Sfumato.Tests;

public class CssSelectorTests
{
    [Fact]
    public void InvalidEmpty()
    {
        var selector = new CssSelector
        {
            Value = ""
        };
        
        Assert.True(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Empty(selector.RootSegment);
        Assert.Empty(selector.CustomValueSegment);
        Assert.Empty(selector.FixedValue);
        Assert.Empty(selector.EscapedSelector);
    }

    [Fact]
    public void InvalidBracketOrder()
    {
        var selector = new CssSelector
        {
            Value = "]dsfdfsfsd["
        };
        
        Assert.True(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Empty(selector.RootSegment);
        Assert.Empty(selector.FixedValue);
        Assert.Empty(selector.CustomValueSegment);
        Assert.Empty(selector.EscapedSelector);
    }

    [Fact]
    public void InvalidBracketContent()
    {
        var selector = new CssSelector
        {
            Value = "dsfdfsfsd[]"
        };
        
        Assert.True(selector.IsInvalid);
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Empty(selector.RootSegment);
        Assert.Empty(selector.CustomValueSegment);
        Assert.Empty(selector.FixedValue);
        Assert.Empty(selector.EscapedSelector);
    }
    
    [Fact]
    public void BaseClass()
    {
        var selector = new CssSelector
        {
            Value = "text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Empty(selector.CustomValueSegment);
        Assert.Equal(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public void MediaQuery()
    {
        var selector = new CssSelector
        {
            Value = "tabp:text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Equal("tabp", selector.MediaQueries[0]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base", selector.RootSegment);
        Assert.Equal("tabp\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public void MediaQueries()
    {
        var selector = new CssSelector
        {
            Value = "dark:tabp:text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.PseudoClasses);
        Assert.Equal(2, selector.MediaQueries.Count);
        Assert.Equal("dark", selector.MediaQueries[0]);
        Assert.Equal("tabp", selector.MediaQueries[1]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base", selector.RootSegment);
        Assert.Equal("dark\\:tabp\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public void PseudoClass()
    {
        var selector = new CssSelector
        {
            Value = "hover:text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Single(selector.PseudoClasses);
        Assert.Equal("hover", selector.PseudoClasses[0]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base", selector.RootSegment);
        Assert.Equal("hover\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public void PseudoClasses()
    {
        var selector = new CssSelector
        {
            Value = "hover:focus:text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Equal(2, selector.PseudoClasses.Count);
        Assert.Equal("hover", selector.PseudoClasses[0]);
        Assert.Equal("focus", selector.PseudoClasses[1]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base", selector.RootSegment);
        Assert.Equal("hover\\:focus\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public void MixedPrefixes()
    {
        var selector = new CssSelector
        {
            Value = "dark:tabp:hover:focus:text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueries.Count);
        Assert.Equal("dark", selector.MediaQueries[0]);
        Assert.Equal("tabp", selector.MediaQueries[1]);
        Assert.Equal(2, selector.PseudoClasses.Count);
        Assert.Equal("hover", selector.PseudoClasses[0]);
        Assert.Equal("focus", selector.PseudoClasses[1]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base", selector.RootSegment);
        Assert.Equal("dark\\:tabp\\:hover\\:focus\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public void MixedPrefixesReordered()
    {
        var selector = new CssSelector
        {
            Value = "focus:dark:hover:tabp:dark:text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueries.Count);
        Assert.Equal("dark", selector.MediaQueries[0]);
        Assert.Equal("tabp", selector.MediaQueries[1]);
        Assert.Equal(2, selector.PseudoClasses.Count);
        Assert.Equal("focus", selector.PseudoClasses[0]);
        Assert.Equal("hover", selector.PseudoClasses[1]);
        Assert.Empty(selector.CustomValueSegment);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.NotEqual(selector.Value, selector.FixedValue);
        Assert.Equal("dark:tabp:focus:hover:text-base", selector.FixedValue);
        Assert.Equal("text-base", selector.RootSegment);
        Assert.Equal("focus\\:dark\\:hover\\:tabp\\:dark\\:text-base", selector.EscapedSelector);
    }
    
    [Fact]
    public void BaseClassCustomValue()
    {
        var selector = new CssSelector
        {
            Value = "text-[3rem]"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-", selector.RootClass);
        Assert.Equal("text-", selector.RootSegment);
        Assert.Equal("[3rem]", selector.CustomValueSegment);
        Assert.Equal("text-\\[3rem\\]", selector.EscapedSelector);
        Assert.Equal("length", selector.CustomValueType);
    }
    
    [Fact]
    public void BaseClassSlashCustomValue()
    {
        var selector = new CssSelector
        {
            Value = "text-base/3"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Equal(selector.Value, selector.RootSegment);
        Assert.NotEqual(selector.Value, selector.RootClass);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base/", selector.RootClass);
        Assert.Equal("text-base/3", selector.RootSegment);
        Assert.Equal(string.Empty, selector.CustomValueSegment);
        Assert.Equal("text-base\\/3", selector.EscapedSelector);
    }
    
    [Fact]
    public void BaseClassCustomValueWithPrefixes()
    {
        var selector = new CssSelector
        {
            Value = "dark:tabp:hover:text-[3rem]"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueries.Count);
        Assert.Equal("dark", selector.MediaQueries[0]);
        Assert.Equal("tabp", selector.MediaQueries[1]);
        Assert.Single(selector.PseudoClasses);
        Assert.Equal("hover", selector.PseudoClasses[0]);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-", selector.RootSegment);
        Assert.Equal("[3rem]", selector.CustomValueSegment);
        Assert.Equal("dark\\:tabp\\:hover\\:text-\\[3rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public void BaseClassSlashCustomValueWithPrefixes()
    {
        var selector = new CssSelector
        {
            Value = "dark:tabp:hover:text-base/3"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueries.Count);
        Assert.Equal("dark", selector.MediaQueries[0]);
        Assert.Equal("tabp", selector.MediaQueries[1]);
        Assert.Single(selector.PseudoClasses);
        Assert.Equal("hover", selector.PseudoClasses[0]);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base/", selector.RootClass);
        Assert.Equal("text-base/3", selector.RootSegment);
        Assert.Equal(string.Empty, selector.CustomValueSegment);
        Assert.Equal("dark\\:tabp\\:hover\\:text-base\\/3", selector.EscapedSelector);
    }
    
    [Fact]
    public void BaseClassSlashWithCustomValueWithPrefixes()
    {
        var selector = new CssSelector
        {
            Value = "dark:tabp:hover:text-base/[3]"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Equal(2, selector.MediaQueries.Count);
        Assert.Equal("dark", selector.MediaQueries[0]);
        Assert.Equal("tabp", selector.MediaQueries[1]);
        Assert.Single(selector.PseudoClasses);
        Assert.Equal("hover", selector.PseudoClasses[0]);
        Assert.NotEqual(selector.Value, selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base/", selector.RootSegment);
        Assert.Equal("[3]", selector.CustomValueSegment);
        Assert.Equal("dark\\:tabp\\:hover\\:text-base\\/\\[3\\]", selector.EscapedSelector);
        Assert.Equal("integer", selector.CustomValueType);
    }
    
    [Fact]
    public void ArbitraryCss()
    {
        var selector = new CssSelector
        {
            Value = "[font-size:1rem]"
        };
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Empty(selector.RootSegment);
        Assert.Equal(selector.Value, selector.CustomValueSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("\\[font-size\\:1rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public void ArbitraryCssWithPrefix()
    {
        var selector = new CssSelector
        {
            Value = "tabp:[font-size:1rem]"
        };
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueries);
        Assert.Equal("tabp", selector.MediaQueries[0]);
        Assert.Empty(selector.PseudoClasses);
        Assert.Empty(selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("[font-size:1rem]", selector.CustomValueSegment);
        Assert.Equal("tabp\\:\\[font-size\\:1rem\\]", selector.EscapedSelector);
    }
    
    [Fact]
    public void ArbitraryCssWithSpaces()
    {
        var selector = new CssSelector
        {
            Value = "tabp:[padding:1rem_2rem]"
        };
        
        Assert.True(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueries);
        Assert.Equal("tabp", selector.MediaQueries[0]);
        Assert.Empty(selector.PseudoClasses);
        Assert.Empty(selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("[padding:1rem_2rem]", selector.CustomValueSegment);
        Assert.Equal("tabp\\:\\[padding\\:1rem_2rem\\]", selector.EscapedSelector);
    }

    [Fact]
    public void TrailingSlash()
    {
        var selector = new CssSelector
        {
            Value = "text-base/"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Equal("text-base/", selector.RootSegment);
        Assert.Equal(selector.Value, selector.FixedValue);
        Assert.Equal("text-base\\/", selector.EscapedSelector);
    }
    
    [Fact]
    public void IsImportant()
    {
        var selector = new CssSelector
        {
            Value = "!text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Empty(selector.MediaQueries);
        Assert.Empty(selector.PseudoClasses);
        Assert.Equal("text-base", selector.RootSegment);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector
        {
            Value = "tabp:focus:!text-base"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueries);
        Assert.Single(selector.PseudoClasses);
        Assert.Equal("text-base", selector.RootSegment);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector
        {
            Value = "tabp:focus:!text-base/3"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueries);
        Assert.Single(selector.PseudoClasses);
        Assert.Equal("text-base/3", selector.RootSegment);
        Assert.True(selector.IsImportant);
        
        selector = new CssSelector
        {
            Value = "tabp:focus:!text-base/[3]"
        };
        
        Assert.False(selector.IsArbitraryCss);
        Assert.Single(selector.MediaQueries);
        Assert.Single(selector.PseudoClasses);
        Assert.Equal("text-base/", selector.RootSegment);
        Assert.Equal("[3]", selector.CustomValueSegment);
        Assert.Equal("3", selector.CustomValue);
        Assert.True(selector.IsImportant);
    }
}
