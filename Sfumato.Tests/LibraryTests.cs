using Sfumato.Entities.UtilityClasses;
using UtilityLibrary = Sfumato.Entities.Library.Library;

namespace Sfumato.Tests;

public class LibraryTests
{
    [Fact]
    public void CachedBaseDefinitionsAreSharedAcrossLibraries()
    {
        var first = new UtilityLibrary();
        var second = new UtilityLibrary();

        Assert.True(first.SimpleClasses.TryGetValue("block", out var firstDefinition));
        Assert.True(second.SimpleClasses.TryGetValue("block", out var secondDefinition));
        Assert.Same(firstDefinition, secondDefinition);
    }

    [Fact]
    public void ThemeOverlaysRemainIsolatedBetweenLibraries()
    {
        var first = new UtilityLibrary();
        var second = new UtilityLibrary();
        var overlay = new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = "display: overlay;"
        };

        first.SimpleClasses["block"] = overlay;
        first.ScannerClassNamePrefixes.Insert("overlay-only", null);

        Assert.Same(overlay, first.SimpleClasses["block"]);
        Assert.NotSame(overlay, second.SimpleClasses["block"]);
        Assert.True(first.ScannerClassNamePrefixes.ContainsKey("overlay-only"));
        Assert.False(second.ScannerClassNamePrefixes.ContainsKey("overlay-only"));
    }
}
