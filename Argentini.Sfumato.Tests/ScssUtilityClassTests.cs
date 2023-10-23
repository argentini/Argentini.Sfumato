using System.Collections.Concurrent;
using Argentini.Sfumato.ScssUtilityCollections;
using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.Tests;

public class ScssUtilityClassTests
{
    [Fact]
    public async Task BackgroundColor()
    {
        var backgrounds = new ConcurrentDictionary<string, ScssUtilityClass>();

        await backgrounds.AddBackgroundAsync();
        
        Assert.Single(backgrounds);
        Assert.Equal(310, backgrounds["bg"].Options.Count);
    }
}
