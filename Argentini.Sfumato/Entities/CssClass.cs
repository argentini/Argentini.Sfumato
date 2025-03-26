// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities;

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

            AllSegments.Clear();
            VariantSegments.Clear();
            CoreSegments.Clear();
            
            AllSegments.AddRange(ContentScanner.SplitByColonsRegex().Split(_name.TrimEnd('!')));

            ProcessData();

            if (IsValid == false)
                return;

            IsImportant = _name.EndsWith('!');

            // EscapeCssClassName();
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

    public bool IsValid { get; set; }
    public bool IsCustomCss { get; set; }
    public bool IsCssCustomPropertyAssignment { get; set; }
    public bool IsImportant { get; set; }

    #endregion
    
    #region Initialization

    public CssClass(AppState appState, string name)
    {
        AppState = appState;
        Name = name;
    }

    private void ProcessData()
    {
        if (AppState is null || string.IsNullOrEmpty(Name))
            return;

        #region Variants

        if (AllSegments.Count > 1)
        {
            // One or more invalid variants invalidate the entire utility class
            
            foreach (var segment in AllSegments.Take(AllSegments.Count - 1))
            {
                var variant = segment.TrimStart("max-").TrimStart("not-") ?? string.Empty;

                if (string.IsNullOrEmpty(variant))
                    return;

                if (TryVariantIsMediaQuery(variant, out var mediaQuery))
                {
                    if (mediaQuery is null)
                        return;

                    VariantSegments.Add(segment, mediaQuery);
                }
                else if (TryVariantIsPseudoClass(variant, out var pseudoClass))
                {
                    if (pseudoClass is null)
                        return;

                    VariantSegments.Add(segment, pseudoClass);
                }
                else if (TryVariantIsGroup(variant, out var group))
                {
                    if (group is null)
                        return;

                    VariantSegments.Add(segment, group);
                }
                else if (TryVariantIsPeer(variant, out var peer))
                {
                    if (peer is null)
                        return;

                    VariantSegments.Add(segment, peer);
                }
                else if (TryVariantIsNth(variant, out var nth))
                {
                    if (nth is null)
                        return;

                    VariantSegments.Add(segment, nth);
                }
                else if (TryVariantIsHas(variant, out var has))
                {
                    if (has is null)
                        return;

                    VariantSegments.Add(segment, has);
                }
                else if (TryVariantIsSupports(variant, out var supports))
                {
                    if (supports is null)
                        return;

                    VariantSegments.Add(segment, supports);
                }
                else if (TryVariantIsNotSupports(variant, out var notSupports))
                {
                    if (notSupports is null)
                        return;

                    VariantSegments.Add(segment, notSupports);
                }
                else if (TryVariantIsData(variant, out var data))
                {
                    if (data is null)
                        return;

                    VariantSegments.Add(segment, data);
                }

                

                else if ((segment.StartsWith("[&") || segment.StartsWith("[@")) && segment.EndsWith(']'))
                {
                    // [&.class] or [&_p] or [@supports(display:grid)] etc.
                    
                    VariantSegments.Add(segment);
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
        var modifier = string.Empty;
        var slashSegments = ContentScanner.SplitBySlashesRegex().Split(value);

        if (slashSegments.Length == 2)
        {
            modifier = slashSegments[^1];
            value = value.TrimEnd($"/{modifier}") ?? string.Empty;
        }

        CoreSegments.Add(prefix);

        if (string.IsNullOrEmpty(value) == false)
            CoreSegments.Add(value);

        if (string.IsNullOrEmpty(modifier) == false)
            CoreSegments.Add(modifier);

        if ((value.StartsWith('[') && value.EndsWith(']')) || (value.StartsWith('(') && value.EndsWith(')')))
        {
            var customValue = value.TrimStart('[').TrimStart('(').TrimEnd(']').TrimEnd(')');

            // Specified arbitrary value data type prefix (e.g. "text-[length:var(--my-text-size)]" or "text-(length:--my-text-size)")
            if (customValue.StartsWith("alpha:", StringComparison.Ordinal) || customValue.StartsWith("number:", StringComparison.Ordinal))
                AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("angle:", StringComparison.Ordinal) || customValue.StartsWith("hue:", StringComparison.Ordinal))
                AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("color:", StringComparison.Ordinal))
                AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("dimension:", StringComparison.Ordinal) || customValue.StartsWith("length:", StringComparison.Ordinal))
                AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("duration:", StringComparison.Ordinal) || customValue.StartsWith("time:", StringComparison.Ordinal))
                AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("flex:", StringComparison.Ordinal))
                AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("frequency:", StringComparison.Ordinal))
                AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("image:", StringComparison.Ordinal) || customValue.StartsWith("url:", StringComparison.Ordinal))
                AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("integer:", StringComparison.Ordinal))
                AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("percentage:", StringComparison.Ordinal))
                AppState.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("ratio:", StringComparison.Ordinal))
                AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("resolution:", StringComparison.Ordinal))
                AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("string:", StringComparison.Ordinal))
                AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
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
                }
            }
        }
        else
        {
            if (string.IsNullOrEmpty(value))
                AppState.Library.SimpleClasses.TryGetValue(prefix, out ClassDefinition);

            else if (ValueIsInteger(value))
                AppState.Library.SpacingClasses.TryGetValue(prefix, out ClassDefinition);

            else if (ValueIsColorName(value))
                AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
        }

        if (ClassDefinition is not null)
            IsValid = true;

        #endregion
    }

    #endregion
    
    #region Identify Value Types
    
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

        if (variant.StartsWith("group-has-"))
        {
            // group-has-[a]: or group-has-[p.my-class]: etc.

            var variantValue = variant.TrimStart("group-has-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = $":is(:where(.group):has(:is({variantValue.Replace('_', ' ')})) *)"
            });

            return true;
        }

        if (variant.StartsWith("group-aria-"))
        {
            // group-aria-checked:

            var variantValue = variant.TrimStart("group-aria-");

            if (string.IsNullOrEmpty(variantValue) || TryVariantIsPseudoClass(variantValue, out var pseudoClass) == false)
                return false;

            if (pseudoClass is null)
                return false;
            
            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = $":is(:where(.group):has({pseudoClass.Statement}) *)"
            });

            return true;
        }
        else
        {
            // group-hover: group-focus: etc.

            var variantValue = variant.TrimStart("group-");
            var indexOfSlash = variantValue?.LastIndexOf('/') ?? -1;

            if (indexOfSlash > 0)
                variantValue = variantValue?[..indexOfSlash];

            if (string.IsNullOrEmpty(variantValue))
                return false;
            
            if (variantValue.StartsWith('[') && variantValue.EndsWith(']'))
            {
                // group-[.is-published]:

                variantValue = variantValue.TrimStart('[').TrimEnd(']');

                if (string.IsNullOrEmpty(variantValue))
                    return false;
                        
                VariantSegments.Add(variant, new VariantMetadata
                {
                    PrefixType = "pseudoclass",
                    Statement = $":is(:where(.group):has({variantValue.Replace('_', ' ')}) *)"
                });

                return true;
            }

            if (TryVariantIsPseudoClass(variantValue, out var pseudoClass))
            {
                // group-hover:
                
                if (pseudoClass is null)
                    return false;
            
                VariantSegments.Add(variant, new VariantMetadata
                {
                    PrefixType = "pseudoclass",
                    Statement = $":is(:where(.group){pseudoClass.Statement}) *)"
                });

                return true;
            }
        }

        return false;
    }
    
    public bool TryVariantIsPeer(string variant, out VariantMetadata? peer)
    {
        peer = null;

        if (variant.StartsWith("peer-has-"))
        {
            // peer-has-[a]: or peer-has-[p.my-class]: etc.

            var variantValue = variant.TrimStart("peer-has-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = $":is(:where(.peer):has(:is({variantValue.Replace('_', ' ')}))~*)"
            });

            return true;
        }

        if (variant.StartsWith("peer-aria-"))
        {
            // peer-aria-checked:

            var variantValue = variant.TrimStart("peer-aria-");

            if (string.IsNullOrEmpty(variantValue) || TryVariantIsPseudoClass(variantValue, out var pseudoClass) == false)
                return false;

            if (pseudoClass is null)
                return false;
            
            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = $":is(:where(.peer):has({pseudoClass.Statement})~*)"
            });

            return true;
        }
        else
        {
            // peer-hover: peer-focus: etc.

            var variantValue = variant.TrimStart("peer-");
            var indexOfSlash = variantValue?.LastIndexOf('/') ?? -1;

            if (indexOfSlash > 0)
                variantValue = variantValue?[..indexOfSlash];

            if (string.IsNullOrEmpty(variantValue))
                return false;
            
            if (variantValue.StartsWith('[') && variantValue.EndsWith(']'))
            {
                // peer-[.is-published]:

                variantValue = variantValue.TrimStart('[').TrimEnd(']');

                if (string.IsNullOrEmpty(variantValue))
                    return false;
                        
                VariantSegments.Add(variant, new VariantMetadata
                {
                    PrefixType = "pseudoclass",
                    Statement = $":is(:where(.peer):has({variantValue.Replace('_', ' ')})~*)"
                });

                return true;
            }

            if (TryVariantIsPseudoClass(variantValue, out var pseudoClass))
            {
                // peer-hover:
                
                if (pseudoClass is null)
                    return false;
            
                VariantSegments.Add(variant, new VariantMetadata
                {
                    PrefixType = "pseudoclass",
                    Statement = $":is(:where(.peer){pseudoClass.Statement}~*)"
                });

                return true;
            }
        }

        return false;
    }

    public bool TryVariantIsNth(string variant, out VariantMetadata? nth)
    {
        nth = null;

        var variantValue = variant.TrimStart("nth-last-of-type-").TrimStart("nth-of-type-").TrimStart("nth-last-").TrimStart("nth-");

        if (string.IsNullOrEmpty(variantValue))
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
            Statement = $":{variant.Replace('_', ' ')}"
        };

        return true;
    }    

    public bool TryVariantIsHas(string variant, out VariantMetadata? has)
    {
        has = null;

        if (variant.StartsWith("has-["))
        {
            // has-[a]: or has-[a.link]: etc.

            var variantValue = variant.TrimStart("has-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = $":has({variantValue.Replace('_', ' ')})"
            });

            return true;
        }
        else
        {
            // has-hover: has-focus: etc.

            var variantValue = variant.TrimStart("has-");

            if (string.IsNullOrEmpty(variantValue))
                return false;

            if (TryVariantIsPseudoClass(variantValue, out var pseudoClass))
            {
                if (pseudoClass is null)
                    return false;
            
                VariantSegments.Add(variant, new VariantMetadata
                {
                    PrefixType = "pseudoclass",
                    Statement = $":has({pseudoClass.Statement})"
                });

                return true;
            }
        }

        return false;
    }

    public bool TryVariantIsSupports(string variant, out VariantMetadata? supports)
    {
        supports = null;

        if (variant.StartsWith("supports-["))
        {
            // supports-[display:grid]:

            var variantValue = variant.TrimStart("supports-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "supports",
                Statement = $"@supports ({variantValue.Replace('_', ' ')}) {{"
            });
        }
        else
        {
            // supports-hover:

            var variantValue = variant.TrimStart("supports-");

            if (string.IsNullOrEmpty(variantValue))
                return false;

            var match = AppState?.Library.CssPropertyNamesWithColons.GetLongestMatchingPrefix($"{variantValue}:")?.TrimEnd(':');

            if (string.IsNullOrEmpty(match))
                return false;

            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "supports",
                Statement = $"@supports ({match}: initial) {{"
            });
        }

        return true;
    }
    
    public bool TryVariantIsNotSupports(string variant, out VariantMetadata? notSupports)
    {
        notSupports = null;

        if (variant.StartsWith("not-supports-["))
        {
            // not-supports-[display:grid]:

            var variantValue = variant.TrimStart("not-supports-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            variantValue = variantValue.TrimStart('[').TrimEnd(']');
            
            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "not-supports",
                Statement = $"@supports not ({variantValue.Replace('_', ' ')}) {{"
            });
        }
        else
        {
            // not-supports-hover:

            var variantValue = variant.TrimStart("not-supports-");

            if (string.IsNullOrEmpty(variantValue))
                return false;

            var match = AppState?.Library.CssPropertyNamesWithColons.GetLongestMatchingPrefix($"{variantValue}:")?.TrimEnd(':');

            if (string.IsNullOrEmpty(match))
                return false;

            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "not-supports",
                Statement = $"@supports not ({match}: initial) {{"
            });
        }

        return true;
    }
    
    public bool TryVariantIsData(string variant, out VariantMetadata? data)
    {
        data = null;

        if (variant.StartsWith("data-["))
        {
            // data-[size=large]:

            var variantValue = variant.TrimStart("data-");

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = variantValue.Replace('_', ' ')
            });
        }
        else
        {
            // data-active:

            VariantSegments.Add(variant, new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = $"[{variant}]"
            });
        }

        return true;
    }
    
    
    
    
    
    #endregion
    
}