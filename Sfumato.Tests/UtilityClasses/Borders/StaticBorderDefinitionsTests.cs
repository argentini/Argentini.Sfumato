using System.Collections.Frozen;
using BorderColorDefinitions = Sfumato.Entities.UtilityClasses.Borders.BorderColor;
using BorderRadiusDefinitions = Sfumato.Entities.UtilityClasses.Borders.BorderRadius;
using BorderWidthDefinitions = Sfumato.Entities.UtilityClasses.Borders.BorderWidth;

namespace Sfumato.Tests.UtilityClasses.Borders;

public class StaticBorderDefinitionsTests
{
    [Fact]
    public void StaticBorderDefinitions_AreFrozen()
    {
        Assert.IsAssignableFrom<FrozenDictionary<string, string>>(BorderRadiusDefinitions.Borders);
        Assert.IsAssignableFrom<FrozenDictionary<string, string>>(BorderWidthDefinitions.BorderWidths);
        Assert.IsAssignableFrom<FrozenDictionary<string, string>>(BorderColorDefinitions.BorderColors);
    }
}
