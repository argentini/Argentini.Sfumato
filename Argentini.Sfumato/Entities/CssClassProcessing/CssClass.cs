// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Argentini.Sfumato.Entities.UtilityClasses;
using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.CssClassProcessing;

public sealed class CssClass
{
    #region Properties
    
    public AppState? AppState { get; set; }
    
    /// <summary>
    /// Utility class name from scanned files.
    /// (e.g. "dark:tabp:text-base/6")
    /// </summary>
    public string Name
    {
        get => _name;
        
        set
        {
            _name = value;

            CssSelector = string.Empty;
            IsValid = false;

            SelectorSort = 0;

            Wrappers.Clear();
            AllSegments.Clear();
            VariantSegments.Clear();
            CoreSegments.Clear();
            
            AllSegments.AddRange(ContentScanner.SplitByColonsRegex().Split(_name.TrimEnd('!')));

            ProcessData();

            if (IsValid == false)
                return;

            IsImportant = _name.EndsWith('!');

            GenerateSelector();
            GenerateWrappers();
        }
    }
    private string _name = string.Empty;

    /// <summary>
    /// Name broken into variant and core segments.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]", "text-base/6"])
    /// </summary>
    public List<string> AllSegments { get; } = [];

    /// <summary>
    /// Variant segments used in the class name.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]"])
    /// </summary>
    public Dictionary<string,VariantMetadata> VariantSegments { get; } = new(StringComparer.Ordinal);

    /// <summary>
    /// Core class segments; only one segment for static utilities.
    /// (e.g. "dark:tabp:text-base/6" => ["text-", "base", "6"])
    /// (e.g. "dark:tabp:-min-w-10" => ["-min-w-", "10"])
    /// (e.g. "antialiased" => ["antialiased"])
    /// </summary>
    public List<string> CoreSegments { get; } = [];

    /// <summary>
    /// CSS selector generated from the Name; 
    /// (e.g. "dark:tabp:text-base/6" => ".dark\:tabp\:text-base\/6")
    /// </summary>
    public string CssSelector { get; set; } = string.Empty;

    /// <summary>
    /// Master class definition for this utility class.
    /// </summary>
    public ClassDefinition? ClassDefinition;

    public List<string> Wrappers { get; } = [];

    public string Selector { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string ModifierValue { get; set; } = string.Empty;
    public long SelectorSort { get; set; }

    public bool IsValid { get; set; }
    public bool IsCustomCss { get; set; }
    public bool IsCssCustomPropertyAssignment { get; set; }
    public bool IsImportant { get; set; }

    #endregion
    
    #region Lifecycle

    public CssClass(AppState appState, string name)
    {
        AppState = appState;
        Name = name;
    }

    #endregion
    
    #region Initialization
    
    private void ProcessData()
    {
        if (AppState is null || string.IsNullOrEmpty(Name))
            return;

        var sb = AppState.StringBuilderPool.Get();

        // todo: create code to get class styles from parsed base class segments
        // todo: create code to iterate, group, wrap classes, generate actual CSS (perhaps create a list of segments based on like media queries, then stack them in the final CSS)
        
        try
        {
            #region Variants

            if (AllSegments.Count > 1)
            {
                // One or more invalid variants invalidate the entire utility class

                foreach (var segment in AllSegments.Take(AllSegments.Count - 1))
                {
                    if (string.IsNullOrEmpty(segment))
                        return;

                    if (TryVariantIsMediaQuery(segment, out var mediaQuery))
                    {
                        if (mediaQuery is null)
                            return;

                        VariantSegments.Add(segment, mediaQuery);
                    }
                    else if (TryVariantIsPseudoClass(segment, out var pseudoClass))
                    {
                        if (pseudoClass is null)
                            return;

                        VariantSegments.Add(segment, pseudoClass);
                    }
                    else if (TryVariantIsGroup(segment, out var group))
                    {
                        if (group is null)
                            return;

                        VariantSegments.Add(segment, group);
                    }
                    else if (TryVariantIsPeer(segment, out var peer))
                    {
                        if (peer is null)
                            return;

                        VariantSegments.Add(segment, peer);
                    }
                    else if (TryVariantIsNth(segment, out var nth))
                    {
                        if (nth is null)
                            return;

                        VariantSegments.Add(segment, nth);
                    }
                    else if (TryVariantIsHas(segment, out var has))
                    {
                        if (has is null)
                            return;

                        VariantSegments.Add(segment, has);
                    }
                    else if (TryVariantIsSupports(segment, out var supports))
                    {
                        if (supports is null)
                            return;

                        VariantSegments.Add(segment, supports);
                    }
                    else if (TryVariantIsNotSupports(segment, out var notSupports))
                    {
                        if (notSupports is null)
                            return;

                        VariantSegments.Add(segment, notSupports);
                    }
                    else if (TryVariantIsData(segment, out var data))
                    {
                        if (data is null)
                            return;

                        VariantSegments.Add(segment, data);
                    }
                    else if (TryVariantIsCustom(segment, out var custom))
                    {
                        if (custom is null)
                            return;

                        VariantSegments.Add(segment, custom);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            #endregion

            #region Custom CSS

            if (AllSegments[^1][0] == '[' && AllSegments[^1][^1] == ']')
            {
                var trimmedValue = AllSegments[^1].TrimStart('[').TrimEnd(']').Trim('_');
                var colonIndex = trimmedValue.IndexOf(':');

                if (colonIndex < 1 || colonIndex > trimmedValue.Length - 2)
                    return;

                if (trimmedValue.StartsWith("--", StringComparison.Ordinal))
                {
                    // [--my-color-var:red]
                    CoreSegments.Add(trimmedValue);
                    IsCssCustomPropertyAssignment = true;
                    IsValid = true;

                    return;
                }

                /*
                if (ContentScanner.PatternCssCustomPropertyAssignmentRegex().Match(trimmedValue).Success)
                {
                    // [--my-color-var:red]
                    CoreSegments.Add(trimmedValue);
                    IsCssCustomPropertyAssignment = true;
                    IsValid = true;

                    return;
                }tr
                */

                if (AppState.Library.CssPropertyNamesWithColons.HasPrefixIn(trimmedValue) == false)
                    return;

                // [color:red]
                CoreSegments.Add(trimmedValue);
                IsCustomCss = true;
                IsValid = true;

                return;
            }

            #endregion

            #region Utility Classes

            var prefix = AppState.Library.ScannerClassNamePrefixes.GetLongestMatchingPrefix(AllSegments[^1]);

            if (string.IsNullOrEmpty(prefix))
                return;

            var value = AllSegments[^1].TrimStart(prefix) ?? string.Empty;
            var slashSegments = ContentScanner.SplitBySlashesRegex().Split(value);

            if (slashSegments.Length == 2)
            {
                ModifierValue = slashSegments[^1];
                value = value.TrimEnd($"/{ModifierValue}") ?? string.Empty;
                ModifierValue = ModifierValue.TrimStart('[').TrimEnd(']');
            }

            CoreSegments.Add(prefix);

            if (string.IsNullOrEmpty(value) == false)
                CoreSegments.Add(value);

            if (string.IsNullOrEmpty(ModifierValue) == false)
                CoreSegments.Add(ModifierValue);

            if ((value.StartsWith('[') && value.EndsWith(']')) || (value.StartsWith('(') && value.EndsWith(')')))
            {
                var customValue = value.TrimStart('[').TrimStart('(').TrimEnd(']').TrimEnd(')');

                // Specified arbitrary value data type prefix (e.g. "text-[length:var(--my-text-size)]" or "text-(length:--my-text-size)")

                if (customValue.StartsWith("alpha:", StringComparison.Ordinal) || customValue.StartsWith("number:", StringComparison.Ordinal))
                {
                    AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("angle:", StringComparison.Ordinal) || customValue.StartsWith("hue:", StringComparison.Ordinal))
                {
                    AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("color:", StringComparison.Ordinal))
                {
                    AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("dimension:", StringComparison.Ordinal) || customValue.StartsWith("length:", StringComparison.Ordinal))
                {
                    AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("duration:", StringComparison.Ordinal) || customValue.StartsWith("time:", StringComparison.Ordinal))
                {
                    AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("flex:", StringComparison.Ordinal))
                {
                    AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("frequency:", StringComparison.Ordinal))
                {
                    AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("image:", StringComparison.Ordinal) || customValue.StartsWith("url:", StringComparison.Ordinal))
                {
                    AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("integer:", StringComparison.Ordinal))
                {
                    AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("percentage:", StringComparison.Ordinal))
                {
                    AppState.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("ratio:", StringComparison.Ordinal))
                {
                    AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("resolution:", StringComparison.Ordinal))
                {
                    AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("string:", StringComparison.Ordinal))
                {
                    AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is not null)
                {
                    Value = customValue.StartsWith("--", StringComparison.Ordinal) ? $"var({customValue})" : customValue;
                }
                else
                {
                    if (value.StartsWith("(--", StringComparison.Ordinal) || value.StartsWith("[var(--", StringComparison.Ordinal))
                    {
                        AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                            AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                    }
                    
                    if (ClassDefinition is not null)
                    {
                        Value = customValue.StartsWith("--", StringComparison.Ordinal) ? $"var({customValue})" : customValue;
                    }
                    else
                    {
                        // Auto-detect value type

                        if (ValueIsAlphaNumber(customValue))
                        {
                            if (AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition) == false)
                                AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (ValueIsInteger(customValue))
                            AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsColorName(customValue) || customValue.IsValidWebColor())
                            AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsPercentage(customValue))
                            AppState.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsAngleHue(customValue))
                            AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsDimensionLength(customValue))
                            AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsDurationTime(customValue))
                            AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsFrequency(customValue))
                            AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsImageUrl(customValue))
                            AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsRatio(customValue))
                            AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                        else if (ValueIsResolution(customValue))
                            AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                        else
                        {
                            if (AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition) == false)
                                AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        
                        if (ClassDefinition is not null)
                        {
                            Value = customValue.StartsWith("--", StringComparison.Ordinal) ? $"var({customValue})" : customValue;
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(value))
                {
                    AppState.Library.SimpleClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (ValueIsInteger(value))
                {
                    if (AppState.Library.SpacingClasses.TryGetValue(prefix, out ClassDefinition))
                        Value = value;
                }
                else if (ValueIsColorName(value))
                {
                    if (AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition))
                        Value = value;
                }
            }

            if (ClassDefinition is not null)
            {
                IsValid = true;
                SelectorSort = ClassDefinition.SelectorSort;
            }

            #endregion
        }
        finally
        {
            AppState.StringBuilderPool.Return(sb);
        }
    }

    private string Escape(string value)
    {
        var escaped = AppState?.StringBuilderPool.Get();

        if (escaped is null || string.IsNullOrEmpty(value))
            return value;
        
        try
        {
            for (var i = 0; i < value.Length; i++)
            {
                var c = value[i];

                if ((i == 0 && char.IsDigit(c)) || (char.IsLetterOrDigit(c) == false && c != '-' && c != '_'))
                    escaped.Append('\\');

                escaped.Append(c);
            }

            return escaped.ToString();
        }
        catch
        {
            return value;
        }
        finally
        {
            AppState?.StringBuilderPool.Return(escaped);
        }
    }

    private void GenerateSelector()
    {
        var escaped = AppState?.StringBuilderPool.Get();

        if (escaped is null)
            return;
        
        try
        {
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "prefix").OrderByDescending(s => s.Value.PrefixOrder))
                escaped.Append(variant.Value.SelectorPrefix);

            escaped.Append(Escape(Name));
            
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "pseudoclass").OrderByDescending(s => s.Value.PrefixOrder))
                escaped.Append(variant.Value.SelectorSuffix);
            
            Selector = escaped.ToString();
        }
        catch
        {
            // Ignore
        }
        finally
        {
            AppState?.StringBuilderPool.Return(escaped);
        }
    }

    private void GenerateWrappers()
    {
        var escaped = AppState?.StringBuilderPool.Get();

        if (escaped is null)
            return;
        
        try
        {
            if (VariantSegments.TryGetValue("dark", out var darkVariant))
            {
                Wrappers.Add($"@{darkVariant.PrefixType} {darkVariant.Statement} {{");
            }

            foreach (var queryType in new[] { "media", "supports" })
            {
                foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == queryType && s.Key != "dark").OrderByDescending(s => s.Value.PrefixOrder))
                {
                    if (escaped.Length == 0)
                        escaped.Append($"@{queryType} ");
                    else
                        escaped.Append(" and ");
                
                    escaped.Append(variant.Value.Statement);
                }

                if (escaped.Length > 0)
                {
                    escaped.Append(" {");
                    Wrappers.Add(escaped.ToString());
                    escaped.Clear();
                }
            }

            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType is "wrapper").OrderByDescending(s => s.Value.PrefixOrder))
            {
                Wrappers.Add($"{variant.Value.Statement} {{");
            }
        }
        catch
        {
            // Ignore
        }
        finally
        {
            AppState?.StringBuilderPool.Return(escaped);
        }
    }
    
    #endregion
    
    #region Identify Value Data Types
    
    private string GetUnit(string value)
    {
        var index = 0;

        while (index < value.Length && (char.IsDigit(value[index]) || value[index] == '.'))
            index++;

        return index >= value.Length ? string.Empty : value[index..];         
    }
    
    private bool ValueIsAlphaNumber(string value)
    {
        return value.All(c => char.IsDigit(c) || c == '.');
    }

    private bool ValueIsAngleHue(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssAngleUnits.Any(u => u == unit) ?? false;
    }

    private bool ValueIsColorName(string value)
    {
        return AppState?.Library.Colors.ContainsKey(value) ?? false;
    }

    private bool ValueIsDimensionLength(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssLengthUnits.Any(u => u == unit) ?? false;
    }

    private bool ValueIsDurationTime(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssDurationUnits.Any(u => u == unit) ?? false;
    }

    private bool ValueIsFrequency(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssFrequencyUnits.Any(u => u == unit) ?? false;
    }

    private bool ValueIsImageUrl(string value)
    {
        return value.StartsWith("url(", StringComparison.Ordinal) || Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out _);
    }

    private bool ValueIsInteger(string value)
    {
        return value.All(char.IsDigit);
    }

    private bool ValueIsRatio(string value)
    {
        var segments = value.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length != 2)
            return false;
        
        return int.TryParse(segments[0].Trim(), out _) && int.TryParse(segments[1].Trim(), out _);
    }

    private bool ValueIsPercentage(string value)
    {
        return value.All(c => char.IsDigit(c) || c == '.') && value.EndsWith('%');
    }

    private bool ValueIsResolution(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssResolutionUnits.Any(u => u == unit) ?? false;
    }

    #endregion

    #region Identify Variants

    public bool TryVariantIsMediaQuery(string variant, out VariantMetadata? cssMediaQuery)
    {
        cssMediaQuery = null;

        return AppState?.Library.MediaQueryPrefixes.TryGetValue(variant, out cssMediaQuery) == true;
    }
    
    public bool TryVariantIsPseudoClass(string variant, out VariantMetadata? pseudoClass)
    {
        pseudoClass = null;

        return AppState?.Library.PseudoclassPrefixes.TryGetValue(variant, out pseudoClass) == true;
    }

    public bool TryVariantIsGroup(string variant, out VariantMetadata? group)
    {
        group = null;

        if (variant.StartsWith("group-has-["))
        {
            // group-has-[a]: or group-has-[p.my-class]: etc.

            var variantValue = variant.TrimStart("group-has-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            group = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.group):has(:is({variantValue.Replace('_', ' ')})) *)"
            };
        }
        else if (variant.StartsWith("group-aria-"))
        {
            // group-aria-checked:

            var variantValue = variant.TrimStart("group-aria-");

            if (string.IsNullOrEmpty(variantValue) || TryVariantIsPseudoClass(variantValue, out var pseudoClass) == false)
                return false;

            if (pseudoClass is null)
                return false;
            
            group = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.group{pseudoClass.Statement}) *)"
            };
        }
        else if (variant.StartsWith("group-"))
        {
            // group-hover: group-focus/item: etc.

            var variantValue = variant.TrimStart("group-");

            if (string.IsNullOrEmpty(variantValue))
                return false;
            
            var indexOfSlash = variantValue.LastIndexOf('/');
            var slashValue = string.Empty;

            if (indexOfSlash == 0 || indexOfSlash >= variantValue.Length)
                return false;

            if (indexOfSlash > 0)
            {
                slashValue = variantValue[indexOfSlash..];
                variantValue = variantValue[..indexOfSlash];
            }

            if (string.IsNullOrEmpty(variantValue))
                return false;
            
            if (variantValue.StartsWith('[') && variantValue.EndsWith(']'))
            {
                // group-[.is-published]:

                variantValue = variantValue.TrimStart('[').TrimEnd(']');

                if (string.IsNullOrEmpty(variantValue))
                    return false;
                        
                group = new VariantMetadata
                {
                    PrefixType = "prefix",
                    SelectorPrefix = $".group{slashValue.Replace("/", "\\/")}{variantValue.Replace('_', ' ')} ",
                };
            }
            else if (TryVariantIsPseudoClass(variantValue, out var pseudoClass))
            {
                // group-hover:

                if (pseudoClass is null)
                    return false;

                group = new VariantMetadata
                {
                    PrefixType = "prefix",
                    SelectorPrefix = $".group{slashValue.Replace("/", "\\/")}{pseudoClass.Statement} ",
                };
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }
    
    public bool TryVariantIsPeer(string variant, out VariantMetadata? peer)
    {
        peer = null;

        if (variant.StartsWith("peer-has-["))
        {
            // peer-has-[a]: or peer-has-[p.my-class]: etc.

            var variantValue = variant.TrimStart("peer-has-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            peer = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.peer):has(:is({variantValue.Replace('_', ' ')})) ~ *)"
            };
        }
        else if (variant.StartsWith("peer-aria-"))
        {
            // peer-aria-checked:

            var variantValue = variant.TrimStart("peer-aria-");

            if (string.IsNullOrEmpty(variantValue) || TryVariantIsPseudoClass(variantValue, out var pseudoClass) == false)
                return false;

            if (pseudoClass is null)
                return false;
            
            peer = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.peer{pseudoClass.Statement}) ~ *)"
            };
        }
        else if (variant.StartsWith("peer-"))
        {
            // peer-hover: peer-focus: etc.

            var variantValue = variant.TrimStart("peer-");

            if (string.IsNullOrEmpty(variantValue))
                return false;
            
            var indexOfSlash = variantValue.LastIndexOf('/');
            var slashValue = string.Empty;

            if (indexOfSlash == 0 || indexOfSlash >= variantValue.Length)
                return false;

            if (indexOfSlash > 0)
            {
                slashValue = variantValue[indexOfSlash..];
                variantValue = variantValue[..indexOfSlash];
            }

            if (string.IsNullOrEmpty(variantValue))
                return false;
            
            if (variantValue.StartsWith('[') && variantValue.EndsWith(']'))
            {
                // peer-[.is-published]:

                variantValue = variantValue.TrimStart('[').TrimEnd(']');

                if (string.IsNullOrEmpty(variantValue))
                    return false;
                        
                peer = new VariantMetadata
                {
                    PrefixType = "prefix",
                    SelectorPrefix = $".peer{slashValue.Replace("/", "\\/")}{variantValue.Replace('_', ' ')} ~ ",
                };
            }
            else if (TryVariantIsPseudoClass(variantValue, out var pseudoClass))
            {
                // peer-hover:

                if (pseudoClass is null)
                    return false;

                peer = new VariantMetadata
                {
                    PrefixType = "prefix",
                    SelectorPrefix = $".peer{slashValue.Replace("/", "\\/")}{pseudoClass.Statement} ~ ",
                };
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    public bool TryVariantIsNth(string variant, out VariantMetadata? nth)
    {
        nth = null;

        if (variant.StartsWith("nth-", StringComparison.Ordinal) == false)
            return false;
        
        var variantValue = variant.TrimStart("nth-last-of-type-").TrimStart("nth-of-type-").TrimStart("nth-last-").TrimStart("nth-");

        if (string.IsNullOrEmpty(variantValue) || variantValue.Length == variant.Length)
            return false;

        if (variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
            return false;
        
        // nth-[3n+1]:
        var pseudoClass = variant.TrimEnd(variantValue);

        if (string.IsNullOrEmpty(pseudoClass))
            return false;

        variantValue = variantValue.TrimStart('[').TrimEnd(']');

        if (string.IsNullOrEmpty(variantValue))
            return false;
                        
        nth = new VariantMetadata
        {
            PrefixType = "pseudoclass",
            SelectorSuffix = $":{variant.Replace('_', ' ')}"
        };

        return true;
    }    

    public bool TryVariantIsHas(string variant, out VariantMetadata? has)
    {
        has = null;

        if (variant.StartsWith("has-", StringComparison.Ordinal) == false)
            return false;
        
        if (variant.StartsWith("has-["))
        {
            // has-[a]: or has-[a.link]: etc.

            var variantValue = variant.TrimStart("has-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            has = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":has({variantValue.Replace('_', ' ')})"
            };
        }
        else if (variant.StartsWith("has-"))
        {
            // has-hover: has-focus: etc.

            var variantValue = variant.TrimStart("has-");

            if (string.IsNullOrEmpty(variantValue))
                return false;

            if (TryVariantIsPseudoClass(variantValue, out var pseudoClass) == false)
                return false;
            
            has = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = $":has({pseudoClass?.SelectorSuffix})"
            };
        }
        else
        {
            return false;
        }

        return true;
    }

    public bool TryVariantIsSupports(string variant, out VariantMetadata? supports)
    {
        supports = null;

        if (variant.StartsWith("supports-", StringComparison.Ordinal) == false)
            return false;

        if (variant.StartsWith("supports-["))
        {
            // supports-[display:grid]:

            var variantValue = variant.TrimStart("supports-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            supports = new VariantMetadata
            {
                PrefixType = "supports",
                PrefixOrder = AppState?.Library.MediaQueryPrefixes["supports-backdrop-blur"].PrefixOrder ?? 0,
                Statement = $"({variantValue.Replace('_', ' ')})"
            };
        }
        else if (variant.StartsWith("supports-"))
        {
            // supports-hover:

            var variantValue = variant.TrimStart("supports-");

            if (string.IsNullOrEmpty(variantValue))
                return false;

            var match = AppState?.Library.CssPropertyNamesWithColons.GetLongestMatchingPrefix($"{variantValue}:")?.TrimEnd(':');

            if (string.IsNullOrEmpty(match))
                return false;

            supports = new VariantMetadata
            {
                PrefixType = "supports",
                PrefixOrder = AppState?.Library.MediaQueryPrefixes["supports-backdrop-blur"].PrefixOrder ?? 0,
                Statement = $"({match}: initial)"
            };
        }
        else
        {
            return false;
        }

        return true;
    }
    
    public bool TryVariantIsNotSupports(string variant, out VariantMetadata? notSupports)
    {
        notSupports = null;

        if (variant.StartsWith("not-supports-", StringComparison.Ordinal) == false)
            return false;

        if (variant.StartsWith("not-supports-["))
        {
            // not-supports-[display:grid]:

            var variantValue = variant.TrimStart("not-supports-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            notSupports = new VariantMetadata
            {
                PrefixType = "not-supports",
                PrefixOrder = AppState?.Library.MediaQueryPrefixes["supports-backdrop-blur"].PrefixOrder ?? 0,
                Statement = $"not ({variantValue.Replace('_', ' ')})"
            };
        }
        else if (variant.StartsWith("not-supports-"))
        {
            // not-supports-hover:

            var variantValue = variant.TrimStart("not-supports-");

            if (string.IsNullOrEmpty(variantValue))
                return false;

            var match = AppState?.Library.CssPropertyNamesWithColons.GetLongestMatchingPrefix($"{variantValue}:")?.TrimEnd(':');

            if (string.IsNullOrEmpty(match))
                return false;

            notSupports = new VariantMetadata
            {
                PrefixType = "not-supports",
                PrefixOrder = AppState?.Library.MediaQueryPrefixes["supports-backdrop-blur"].PrefixOrder ?? 0,
                Statement = $"not ({match}: initial)"
            };
        }
        else
        {
            return false;
        }

        return true;
    }
    
    public bool TryVariantIsData(string variant, out VariantMetadata? data)
    {
        data = null;

        if (variant.StartsWith("data-", StringComparison.Ordinal) == false)
            return false;

        if (variant.StartsWith("data-["))
        {
            // data-[size=large]:

            var variantValue = variant.TrimStart("data-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            data = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = variantValue.Replace('_', ' ')
            };
        }
        else if (variant.StartsWith("data-"))
        {
            // data-active:

            data = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $"[{variant}]"
            };
        }
        else
        {
            return false;
        }

        return true;
    }
    
    public bool TryVariantIsCustom(string variant, out VariantMetadata? custom)
    
    {
        custom = null;

        var variantValue = variant.TrimStart('[').TrimEnd(']');

        if (string.IsNullOrEmpty(variantValue) || variantValue.Length == variant.Length)
            return false;

        if (variantValue.StartsWith('@'))
        {
            // [@supports_(display:grid)]

            custom = new VariantMetadata
            {
                PrefixType = "wrapper",
                PrefixOrder = AppState?.Library.MediaQueryPrefixes["supports-backdrop-blur"].PrefixOrder ?? 0,
                Statement = $"{variantValue.Replace('_', ' ')}"
            };
        }
        else if (variantValue.StartsWith('&'))
        {
            // [&.active]:

            custom = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                PrefixOrder = 1,
                SelectorSuffix = $"{variantValue.TrimStart('&').Replace('_', ' ')}"
            };
        }
        else
        {
            return false;
        }
        
        return true;
    }
    
    #endregion
}