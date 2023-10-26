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

        await backgrounds.AddAllBackgroundClassesAsync();
        
        Assert.Equal(4, backgrounds.Count);
        Assert.Equal(310, backgrounds["bg"].Classes.Count);
        Assert.Equal("background-color: rgb(255,255,255);", backgrounds["bg"].Classes.First(o => o.Selector == "bg-white").ScssMarkup);
    }
    
    [Fact]
    public async Task ArbitraryValue()
    {
        var backgrounds = new ConcurrentDictionary<string, ScssUtilityClassGroup>();

        await backgrounds.AddAllBackgroundClassesAsync();
        
        var scssUtilityClass = backgrounds["bg"].Classes.First(o => o.Selector == "bg-");

        scssUtilityClass.Value = "100%";
        
        Assert.Equal("background-position: 100%;", scssUtilityClass.ScssMarkup);
    }
    
    [Fact]
    public async Task MatchingClasses()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());
        
        var backgrounds = new ConcurrentDictionary<string, ScssUtilityClassGroup>();

        await backgrounds.AddAllBackgroundClassesAsync();

        var cssSelector = new CssSelector(appState, "bg-white");
        await cssSelector.ProcessValue();
        
        var matches = await backgrounds.GetMatchingClassesAsync(cssSelector);
        
        Assert.Single(matches);

        cssSelector = new CssSelector(appState, "bg-[#aabbcc]");
        await cssSelector.ProcessValue();
        
        matches = await backgrounds.GetMatchingClassesAsync(cssSelector);
        
        Assert.Single(matches);
        
        matches[0].Value = cssSelector.ArbitraryValue;
        
        Assert.Equal("background-color: #aabbcc;", matches[0].ScssMarkup);

        cssSelector = new CssSelector(appState, "bg-[top_center]");
        await cssSelector.ProcessValue();
        
        matches = await backgrounds.GetMatchingClassesAsync(cssSelector);
        
        Assert.Single(matches);
        
        matches[0].Value = cssSelector.ArbitraryValue;
        
        Assert.Equal("background-position: top center;", matches[0].ScssMarkup);
    }
}
