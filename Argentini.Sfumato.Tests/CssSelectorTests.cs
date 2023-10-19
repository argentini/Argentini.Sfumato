using Argentini.Sfumato.Entities;

namespace Argentini.Sfumato.Tests;

public class CssSelectorTests
{
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
        Assert.Empty(selector.CustomValue);
        Assert.Equal(selector.Value, selector.Root);
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
        Assert.Empty(selector.CustomValue);
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-base", selector.Root);
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
        Assert.Empty(selector.CustomValue);
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-base", selector.Root);
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
        Assert.Empty(selector.CustomValue);
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-base", selector.Root);
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
        Assert.Empty(selector.CustomValue);
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-base", selector.Root);
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
        Assert.Empty(selector.CustomValue);
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-base", selector.Root);
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
        Assert.Empty(selector.CustomValue);
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-base", selector.Root);
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
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-", selector.Root);
        Assert.Equal("[3rem]", selector.CustomValue);
        Assert.Equal("text-\\[3rem\\]", selector.EscapedSelector);
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
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-base/", selector.Root);
        Assert.Equal("3", selector.CustomValue);
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
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-", selector.Root);
        Assert.Equal("[3rem]", selector.CustomValue);
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
        Assert.NotEqual(selector.Value, selector.Root);
        Assert.Equal("text-base/", selector.Root);
        Assert.Equal("3", selector.CustomValue);
        Assert.Equal("dark\\:tabp\\:hover\\:text-base\\/3", selector.EscapedSelector);
    }
}
