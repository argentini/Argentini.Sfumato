using BenchmarkDotNet.Attributes;
using BorderColorDefinitions = Sfumato.Entities.UtilityClasses.Borders.BorderColor;
using BorderRadiusDefinitions = Sfumato.Entities.UtilityClasses.Borders.BorderRadius;
using BorderWidthDefinitions = Sfumato.Entities.UtilityClasses.Borders.BorderWidth;

namespace Sfumato.Benchmarks;

[BenchmarkCategory("Library")]
[MemoryDiagnoser]
[MedianColumn]
public class StaticBorderDictionaryBenchmarks
{
    private Dictionary<string, string> _borderColors = null!;
    private string[] _borderColorKeys = null!;
    private Dictionary<string, string> _borderRadii = null!;
    private string[] _borderRadiusKeys = null!;
    private Dictionary<string, string> _borderWidths = null!;
    private string[] _borderWidthKeys = null!;

    [GlobalSetup]
    public void Setup()
    {
        _borderRadii = BorderRadiusDefinitions.Borders.ToDictionary(StringComparer.Ordinal);
        _borderWidths = BorderWidthDefinitions.BorderWidths.ToDictionary(StringComparer.Ordinal);
        _borderColors = BorderColorDefinitions.BorderColors.ToDictionary(StringComparer.Ordinal);
        _borderRadiusKeys = BorderRadiusDefinitions.Borders.Keys.ToArray();
        _borderWidthKeys = BorderWidthDefinitions.BorderWidths.Keys.ToArray();
        _borderColorKeys = BorderColorDefinitions.BorderColors.Keys.ToArray();
    }

    [Benchmark(Baseline = true, OperationsPerInvoke = 33)]
    public int MutableLookups()
    {
        var length = 0;

        for (var i = 0; i < _borderRadiusKeys.Length; i++)
            length += _borderRadii[_borderRadiusKeys[i]].Length;

        for (var i = 0; i < _borderWidthKeys.Length; i++)
            length += _borderWidths[_borderWidthKeys[i]].Length;

        for (var i = 0; i < _borderColorKeys.Length; i++)
            length += _borderColors[_borderColorKeys[i]].Length;

        return length;
    }

    [Benchmark(OperationsPerInvoke = 33)]
    public int FrozenLookups()
    {
        var length = 0;

        for (var i = 0; i < _borderRadiusKeys.Length; i++)
            length += BorderRadiusDefinitions.Borders[_borderRadiusKeys[i]].Length;

        for (var i = 0; i < _borderWidthKeys.Length; i++)
            length += BorderWidthDefinitions.BorderWidths[_borderWidthKeys[i]].Length;

        for (var i = 0; i < _borderColorKeys.Length; i++)
            length += BorderColorDefinitions.BorderColors[_borderColorKeys[i]].Length;

        return length;
    }
}
