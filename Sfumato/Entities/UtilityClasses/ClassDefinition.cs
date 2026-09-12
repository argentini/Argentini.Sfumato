// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Text.Json.Serialization;

namespace Sfumato.Entities.UtilityClasses;

[Flags]
public enum UtilityValueKinds : ushort
{
    None = 0,
    Simple = 1 << 0,
    Percentage = 1 << 1,
    Number = 1 << 2,
    Angle = 1 << 3,
    Color = 1 << 4,
    Length = 1 << 5,
    Duration = 1 << 6,
    Flex = 1 << 7,
    Frequency = 1 << 8,
    Url = 1 << 9,
    Integer = 1 << 10,
    Ratio = 1 << 11,
    Abstract = 1 << 12,
    Resolution = 1 << 13,
    String = 1 << 14,
}

[Flags]
public enum UtilityBehaviors : byte
{
    None = 0,
    SlashModifier = 1 << 0,
    ArbitraryLengthOnly = 1 << 1,
    DisallowFractions = 1 << 2,
    OpacityModifier = 1 << 3,
    RazorSyntax = 1 << 4,
}

public sealed class ClassDefinition
{
    private UtilityValueKinds _valueKinds;
    private UtilityBehaviors _behaviors;

    [JsonIgnore]
    public UtilityValueKinds ValueKinds
    {
        get => _valueKinds;
        init => _valueKinds = value;
    }

    [JsonIgnore]
    public UtilityBehaviors Behaviors
    {
        get => _behaviors;
        init => _behaviors = value;
    }

    public bool Accepts(UtilityValueKinds valueKind) => (_valueKinds & valueKind) != 0;

    public bool HasBehavior(UtilityBehaviors behavior) => (_behaviors & behavior) != 0;

    /// <summary>
    /// Is a simple utility with static properties (e.g. "antialiased");
    /// is used as-is and doesn't have any custom properties.
    /// </summary>
    public bool InSimpleUtilityCollection
    {
        get => Accepts(UtilityValueKinds.Simple);
        init => SetValueKind(UtilityValueKinds.Simple, value);
    }

    /// <summary>
    /// Class uses an integer (e.g. "3%")
    /// </summary>
    public bool InPercentageCollection
    {
        get => Accepts(UtilityValueKinds.Percentage);
        init => SetValueKind(UtilityValueKinds.Percentage, value);
    }

    /// <summary>
    /// Class uses float (e.g. "leading-[1.3]")
    /// </summary>
    public bool InFloatNumberCollection
    {
        get => Accepts(UtilityValueKinds.Number);
        init => SetValueKind(UtilityValueKinds.Number, value);
    }

    /// <summary>
    /// Class uses angles (e.g. "90deg")
    /// </summary>
    public bool InAngleHueCollection
    {
        get => Accepts(UtilityValueKinds.Angle);
        init => SetValueKind(UtilityValueKinds.Angle, value);
    }

    /// <summary>
    /// Class uses color (e.g. "#aabbcc")
    /// </summary>
    public bool InColorCollection
    {
        get => Accepts(UtilityValueKinds.Color);
        init => SetValueKind(UtilityValueKinds.Color, value);
    }

    /// <summary>
    /// Class uses length (e.g. "1rem")
    /// </summary>
    public bool InLengthCollection
    {
        get => Accepts(UtilityValueKinds.Length);
        init => SetValueKind(UtilityValueKinds.Length, value);
    }

    /// <summary>
    /// Class uses duration (e.g. "10s")
    /// </summary>
    public bool InDurationCollection
    {
        get => Accepts(UtilityValueKinds.Duration);
        init => SetValueKind(UtilityValueKinds.Duration, value);
    }

    /// <summary>
    /// Class uses flex (e.g. "1fr")
    /// </summary>
    public bool InFlexCollection
    {
        get => Accepts(UtilityValueKinds.Flex);
        init => SetValueKind(UtilityValueKinds.Flex, value);
    }

    /// <summary>
    /// Class uses frequency (e.g. "10Hz")
    /// </summary>
    public bool InFrequencyCollection
    {
        get => Accepts(UtilityValueKinds.Frequency);
        init => SetValueKind(UtilityValueKinds.Frequency, value);
    }

    /// <summary>
    /// Class uses a URL (e.g. "url('/images/bg.jpg')")
    /// </summary>
    public bool InUrlCollection
    {
        get => Accepts(UtilityValueKinds.Url);
        init => SetValueKind(UtilityValueKinds.Url, value);
    }

    /// <summary>
    /// Class uses an integer (e.g. "3")
    /// </summary>
    public bool InIntegerCollection
    {
        get => Accepts(UtilityValueKinds.Integer);
        init => SetValueKind(UtilityValueKinds.Integer, value);
    }

    /// <summary>
    /// Class uses a ratio (e.g. "1 / 2")
    /// </summary>
    public bool InRatioCollection
    {
        get => Accepts(UtilityValueKinds.Ratio);
        init => SetValueKind(UtilityValueKinds.Ratio, value);
    }

    /// <summary>
    /// Class uses abstract value (e.g. "ui-sans-serif, system-ui")
    /// </summary>
    public bool InAbstractValueCollection
    {
        get => Accepts(UtilityValueKinds.Abstract);
        init => SetValueKind(UtilityValueKinds.Abstract, value);
    }

    /// <summary>
    /// Class uses resolution (e.g. "10dpi")
    /// </summary>
    public bool InResolutionCollection
    {
        get => Accepts(UtilityValueKinds.Resolution);
        init => SetValueKind(UtilityValueKinds.Resolution, value);
    }

    /// <summary>
    /// Class uses a string (e.g. "'hello world'")
    /// </summary>
    public bool InStringCollection
    {
        get => Accepts(UtilityValueKinds.String);
        init => SetValueKind(UtilityValueKinds.String, value);
    }

    /// <summary>
    /// Class uses slash modifiers (e.g. "text-base/2")
    /// </summary>
    public bool UsesSlashModifier
    {
        get => HasBehavior(UtilityBehaviors.SlashModifier);
        init => SetBehavior(UtilityBehaviors.SlashModifier, value);
    }

    /// <summary>
    /// Bare numeric values cannot use this length definition.
    /// </summary>
    public bool ArbitraryLengthOnly
    {
        get => HasBehavior(UtilityBehaviors.ArbitraryLengthOnly);
        init => SetBehavior(UtilityBehaviors.ArbitraryLengthOnly, value);
    }

    /// <summary>
    /// Slash fractions cannot use this definition.
    /// </summary>
    public bool DisallowsFractions
    {
        get => HasBehavior(UtilityBehaviors.DisallowFractions);
        init => SetBehavior(UtilityBehaviors.DisallowFractions, value);
    }

    /// <summary>
    /// Slash modifiers must use Tailwind opacity syntax.
    /// </summary>
    public bool ModifierIsOpacity
    {
        get => HasBehavior(UtilityBehaviors.OpacityModifier);
        init => SetBehavior(UtilityBehaviors.OpacityModifier, value);
    }

    /// <summary>
    /// Used for items beginning with "@@" that need to be converted to "@" 
    /// </summary>
    public bool IsRazorSyntax
    {
        get => HasBehavior(UtilityBehaviors.RazorSyntax);
        init => SetBehavior(UtilityBehaviors.RazorSyntax, value);
    }

    /// <summary>
    /// CSS class property template (e.g. "top-0" => "top: {0};").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for a custom value.
    /// </summary>
    public string Template { get; init; } = string.Empty;

    /// <summary>
    /// CSS class property template using a modifier (e.g. "text-base/5" => "font-size: {0}; line-height: calc(var(--spacing) * {1})").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for a custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public string ModifierTemplate { get; init; } = string.Empty;

    /// <summary>
    /// CSS class property template using an arbitrary modifier (e.g. "text-base/[1]" => "font-size: {0}; line-height: {1};").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for a custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public string ArbitraryModifierTemplate { get; init; } = string.Empty;

    /// <summary>
    /// CSS class property template for arbitrary CSS values (e.g. "text-[1rem]" => "font-size: {0};").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// </summary>
    public string ArbitraryCssValueTemplate { get; init; } = string.Empty;

    /// <summary>
    /// CSS class property template for arbitrary CSS values using a modifier (e.g. "text-[1rem]/5" => "font-size: {0}; line-height: calc(var(--spacing) * {1});").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for a custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public string ArbitraryCssValueWithModifierTemplate { get; init; } = string.Empty;

    /// <summary>
    /// CSS class property template for arbitrary CSS values using an arbitrary modifier (e.g. "text-[1rem]/[1.2]" => "font-size: {0}; line-height: {1};").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for a custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public string ArbitraryCssValueWithArbitraryModifierTemplate { get; init; } = string.Empty;

    /// <summary>
    /// Order output class declarations by this integer (default is 0).
    /// </summary>
    public int SelectorSort { get; init; }

    /// <summary>
    /// Collection of usage definitions with styles.
    /// </summary>
    public Dictionary<string, string> DocDefinitions { get; init; } = [];

    /// <summary>
    /// Collection of usage examples with styles.
    /// </summary>
    public Dictionary<string, string> DocExamples { get; init; } = [];

    public ClassDefinition CloneForDocumentation()
    {
        return new ClassDefinition
        {
            ValueKinds = ValueKinds,
            Behaviors = Behaviors,
            Template = Template,
            ModifierTemplate = ModifierTemplate,
            ArbitraryModifierTemplate = ArbitraryModifierTemplate,
            ArbitraryCssValueTemplate = ArbitraryCssValueTemplate,
            ArbitraryCssValueWithModifierTemplate = ArbitraryCssValueWithModifierTemplate,
            ArbitraryCssValueWithArbitraryModifierTemplate = ArbitraryCssValueWithArbitraryModifierTemplate,
            SelectorSort = SelectorSort,
            DocDefinitions = new Dictionary<string, string>(DocDefinitions, StringComparer.Ordinal),
            DocExamples = new Dictionary<string, string>(DocExamples, StringComparer.Ordinal),
        };
    }

    private void SetValueKind(UtilityValueKinds valueKind, bool enabled)
    {
        if (enabled)
            _valueKinds |= valueKind;
        else
            _valueKinds &= ~valueKind;
    }

    private void SetBehavior(UtilityBehaviors behavior, bool enabled)
    {
        if (enabled)
            _behaviors |= behavior;
        else
            _behaviors &= ~behavior;
    }
}
