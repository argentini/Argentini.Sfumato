using System.Collections.Concurrent;
using Argentini.Sfumato.Entities;
using Argentini.Sfumato.ScssUtilityCollections;
using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.Tests;

public class ScssUtilityClassTests
{
    [Fact]
    public async Task BackgroundColor()
    {
        var backgrounds = new ConcurrentDictionary<string, ScssUtilityClassGroup>();

        await backgrounds.AddBackgroundGroupAsync();
        
        Assert.Single(backgrounds);
        Assert.Equal(310, backgrounds["bg"].Classes.Count);
        Assert.Equal("background-color: rgb(255,255,255);", backgrounds["bg"].Classes.First(o => o.Selector == "bg-white").ScssMarkup);
    }
    
    [Fact]
    public async Task ArbitraryValue()
    {
        var backgrounds = new ConcurrentDictionary<string, ScssUtilityClassGroup>();

        await backgrounds.AddBackgroundGroupAsync();
        
        var scssUtilityClass = backgrounds["bg"].Classes.First(o => o.Selector == "bg-");

        scssUtilityClass.Value = "100%";
        
        Assert.Equal("background-position: 100%;", scssUtilityClass.ScssMarkup);
    }
    
    [Fact]
    public async Task MatchingClasses()
    {
        var backgrounds = new ConcurrentDictionary<string, ScssUtilityClassGroup>();

        await backgrounds.AddBackgroundGroupAsync();
        await backgrounds.AddFromGroupAsync();
        await backgrounds.AddViaGroupAsync();
        await backgrounds.AddToGroupAsync();

        var cssSelector = new CssSelector
        {
            Value = "bg-white"
        };
        
        var matches = await backgrounds.GetMatchingClassesAsync(cssSelector);
        
        Assert.Single(matches);
        
        cssSelector = new CssSelector
        {
            Value = "bg-[#aabbcc]"
        };
        
        matches = await backgrounds.GetMatchingClassesAsync(cssSelector);
        
        Assert.Single(matches);
        
        matches[0].Value = cssSelector.CustomValue;
        
        Assert.Equal("background-color: #aabbcc;", matches[0].ScssMarkup);
    }
}
