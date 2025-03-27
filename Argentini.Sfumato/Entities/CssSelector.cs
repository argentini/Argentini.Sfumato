// ReSharper disable MemberCanBePrivate.Global

namespace Argentini.Sfumato.Entities;

public sealed class CssSelector
{
	/*
	 * Structure of a CSS selector:
	 * ============================
	 * 
	 * Starts with optional variants:
	 * {media-queries}: {pseudo-classes}:
	 *
	 * Followed by optional important flag
	 * {!}
	 *
	 * Arbitrary CSS:
	 * [{arbitrary-css}]
	 *
	 * OR
	 *
	 * Utility class:
	 * Prefix, core, modifier, and arbitrary value segments:
	 * {prefix}-{core} { /{modifier-value} or /[{arbitrary-modifier-value}] or -[{arbitrary-value}] }
	 *
	 * Segment Examples:
	 * -----------------
	 * text-base		  {prefix:text}-{core:base}
	 * text-base/5        {prefix:text}-{core:base}/{modifier-value:5}
	 * text-base/[3rem]   {prefix:text}-{core:base}/[{arbitrary-modifier-value:3rem}]
	 * bg-[#aabbcc]       {prefix:bg}-[{arbitrary-value:#aabbcc}]
	 * bg-rose/50         {prefix:bg}-{core:rose}/{modifier-value:5}
	 * w-1/2	          {prefix:w}-{core:1/2} 
	 * sr-only	          {prefix:sr-only}
	 *
	 * Selector examples:
	 * ------------------
	 * sm:focus:bg-rose/50
	 * dark:sm:hover:!text-base/5
	 * 
	 */
	
    #region Properties

    public SfumatoAppState? AppState { get; set; }
    public ScssUtilityClassGroupBase? ScssUtilityClassGroup { get; set; }
    public string ScssMarkup { get; set; } = string.Empty;
    public long VariantSortOrder { get; set; }
    public int SelectorSort => ScssUtilityClassGroup?.SelectorSort ?? 0;

    #region Selector
    
    public string Selector { get; set; } = string.Empty;
    public string FixedSelector { get; set; } = string.Empty;
    
    private string _escapedSelector = string.Empty;
    public string EscapedSelector
    {
	    get
	    {
		    if (_escapedSelector == string.Empty)
			    _escapedSelector = EscapeCssClassName();

		    return _escapedSelector;
	    }

	    set => _escapedSelector = value;
    }    

    #endregion
    
    #region Variants
    
    public List<string> MediaQueryVariants { get; set; } = [];
    public List<string> PseudoClassVariants { get; set; } = [];
    
    #endregion

    #region Segments
    
    public string VariantSegment { get; set; } = string.Empty;
    public string PrefixSegment { get; set; } = string.Empty;
    public string CoreSegment { get; set; } = string.Empty;
    public string ModifierSegment { get; set; } = string.Empty;
    public string ModifierValue { get; set; } = string.Empty;
    public string ModifierValueType { get; set; } = string.Empty;
    public string ArbitraryValue { get; set; } = string.Empty;
    public string ArbitraryValueType { get; set; } = string.Empty;
    public string PseudoclassPath { get; set; } = string.Empty;
    public string PeerGroupPrefix { get; set; } = string.Empty;
    
	#endregion    

	#region Metadata
	
    public int Depth => MediaQueryVariants.Count;
    public bool HasModifierValue => ModifierValue.Length > 0;
    public bool HasArbitraryValue  => ArbitraryValue.Length > 0;
    public bool UsesModifier { get; set; }
    public bool IsImportant { get; set; }
    public bool IsNegative { get; set; }
    public bool IsArbitraryCss { get; set; }
    public bool IsInvalid { get; set; }

    #endregion
    
    #endregion

    public CssSelector()
    {
    }

    public CssSelector(SfumatoAppState appState, bool isArbitraryCss = false)
    {
	    AppState = appState;
	    IsArbitraryCss = isArbitraryCss;
    }

    public CssSelector(SfumatoAppState appState, string selector, bool isArbitraryCss = false)
    {
	    AppState = appState;
	    IsArbitraryCss = isArbitraryCss;
        Selector = selector;
    }

    /// <summary>
    /// Get the value type of the custom class value (e.g. "length:...", "color:...", etc.)
    /// </summary>
    /// <returns></returns>
    public ArbitraryValuePackage SetCustomValueType(string arbitraryValue, SfumatoAppState? appState)
    {
	    if (string.IsNullOrEmpty(arbitraryValue))
		    return new ArbitraryValuePackage();

	    var result = new ArbitraryValuePackage
	    {
			Value = arbitraryValue
	    };

	    var value = arbitraryValue.TrimStart('[').TrimEnd(']');
	    
	    if (value.Contains(':'))
	    {
		    var segments = value.Split(':', StringSplitOptions.RemoveEmptyEntries);

		    if (segments.Length > 1)
			    if (appState?.ArbitraryValueTypes.Contains(segments[0]) ?? false)
			    {
				    result.ValueType = segments[0];
				    result.Value = IsNegative ? ProcessNegativeValue(segments[1], appState) : segments[1];
				    return result;
			    }
	    }
        
        if (IsNegative)
            value = ProcessNegativeValue(value, appState);

	    // Determine value type based on value (e.g. text-[red])

	    if (value.EndsWith('%') && double.TryParse(value.TrimEnd('%'), out _))
	    {
		    result.ValueType = "percentage";
		    return result;
	    }

	    if (value.Contains('.') == false && int.TryParse(value, out _))
	    {
		    result.ValueType = "integer";
		    return result;
	    }

	    if (double.TryParse(value, out _))
	    {
		    result.ValueType = "number";
		    return result;
	    }

	    #region length

        if (value.Contains("calc(") || value.Contains("var("))
        {
            result.ValueType = "length";
            return result;
        }

	    var unitless = string.Empty;

	    foreach (var unit in appState?.CssUnits ?? [])
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    result.ValueType = "length";
		    return result;
	    }

	    #endregion

	    if (value.EndsWith("fr") && double.TryParse(value.TrimEnd("fr"), out _))
	    {
		    result.ValueType = "flex";
		    return result;
	    }

	    if (value.IsValidWebColor() || value == "currentColor")
	    {
		    result.ValueType = "color";
		    return result;
	    }

	    if (
		    (value.StartsWith("url(", StringComparison.Ordinal) && value.EndsWith(')'))
		    || ((value.StartsWith('/') || value.StartsWith("./") || value.StartsWith("../")) && Uri.TryCreate(value, UriKind.Relative, out _))
		    || (Uri.TryCreate(value, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
	    )
	    {
		    result.ValueType = "url";
		    return result;
	    }
	    
	    if (value.StartsWith('\'') && value.EndsWith('\''))
	    {
		    result.ValueType = "string";
		    return result;
	    }
	    
	    #region angle

	    unitless = string.Empty;

	    foreach (var unit in appState?.CssAngleUnits ?? [])
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    result.ValueType = "angle";
		    return result;
	    }

	    #endregion

	    #region time

	    unitless = string.Empty;

	    foreach (var unit in appState?.CssTimeUnits ?? [])
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    result.ValueType = "time";
		    return result;
	    }

	    #endregion

	    #region frequency

	    unitless = string.Empty;

	    foreach (var unit in appState?.CssFrequencyUnits ?? [])
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    result.ValueType = "frequency";
		    return result;
	    }

	    #endregion

	    #region resolution

	    unitless = string.Empty;

	    foreach (var unit in appState?.CssResolutionUnits ?? [])
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    result.ValueType = "resolution";
		    return result;
	    }

	    #endregion
	    
	    #region ratio
	    
	    if (value.Length < 3 || value.IndexOf('/') < 1 || value.IndexOf('/') == value.Length - 1)
		    return result;
	    
	    var customValues = value.Replace('_', ' ').Split('/', StringSplitOptions.RemoveEmptyEntries);

	    if (customValues.Length != 2)
		    return result;

	    if (double.TryParse(customValues[0], out _) && double.TryParse(customValues[1], out _))
	    {
		    result.ValueType = "ratio";
		    return result;
	    }

	    #endregion
	    
	    return result;
    }

    /// <summary>
    /// Get the value type of the custom class value (e.g. "length:...", "color:...", etc.)
    /// </summary>
    /// <returns></returns>
    public static string ProcessNegativeValue(string value, SfumatoAppState? appState)
    {
	    if (string.IsNullOrEmpty(value))
		    return value;

	    if (value.EndsWith('%') && double.TryParse(value.TrimEnd('%'), out _))
            return $"-{value.TrimStart()}";

	    if (value.Contains('.') == false && int.TryParse(value, out _))
            return $"-{value.TrimStart()}";

	    if (double.TryParse(value, out _))
            return $"-{value.TrimStart()}";

	    #region length

        if (value.Contains("calc(") || value.Contains("var("))
            return $"calc(-1 * {value.TrimStart()})";

	    var unitless = string.Empty;

	    foreach (var unit in appState?.CssUnits ?? [])
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
            return $"-{value.TrimStart()}";

	    #endregion

	    return value;
    }
    
    /// <summary>
    /// Establish all property values from parsing the Value property.
    /// </summary>
    public async Task ProcessSelectorAsync()
    {
	    MediaQueryVariants.Clear();
	    PseudoClassVariants.Clear();
        
	    EscapedSelector = string.Empty;
        FixedSelector = Selector;

	    VariantSegment = string.Empty;
	    PrefixSegment = string.Empty;
	    CoreSegment = string.Empty;
	    ModifierSegment = string.Empty;
	    
	    ArbitraryValue = string.Empty;
	    ArbitraryValueType = string.Empty;
	    ModifierValue = string.Empty;
	    ModifierValueType = string.Empty;

	    VariantSortOrder = 0;
	    
	    IsImportant = false;
        IsNegative = false;
	    IsInvalid = false;
	    UsesModifier = false;

	    if (string.IsNullOrEmpty(Selector))
	    {
		    IsInvalid = true;
		    return;
	    }

	    var peerGroupBase = string.Empty;
	    
	    var rightOfVariants = Selector;
	    var rootSegment = Selector;
	    
	    var indexOfBracket = rootSegment.IndexOf('[');
	    var indexOfBracketClose = rootSegment.LastIndexOf(']');

	    if (rootSegment.EndsWith('/') || (indexOfBracket > -1 && indexOfBracketClose > -1 && indexOfBracketClose < indexOfBracket + 2))
	    {
		    IsInvalid = true;
		    return;
	    }

	    #region Pre-Process Peer/Group

	    var peerGroupSegment = IsPeerGroupSelector(Selector, "peer") ? "peer" : IsPeerGroupSelector(Selector, "group") ? "group" : string.Empty;

	    if (string.IsNullOrEmpty(peerGroupSegment) == false)
	    {
		    if (HasPeerGroupVariant(AppState, Selector, peerGroupSegment, out peerGroupBase, out var peerGroupPrefix))
			    PeerGroupPrefix = peerGroupPrefix;
		    
		    indexOfBracket = peerGroupBase.IndexOf('[');
		    indexOfBracketClose = peerGroupBase.LastIndexOf(']');
	    }
	    
	    #endregion

	    var workingSelector = string.IsNullOrEmpty(peerGroupBase) ? Selector : peerGroupBase;

	    if (string.IsNullOrEmpty(peerGroupBase) == false)
		    rootSegment = workingSelector;
	    
	    #region Process Arbitrary Value
	    
	    if (workingSelector.IndexOf('[') > -1 && workingSelector.IndexOf(']') > 0 && indexOfBracket > -1 && indexOfBracketClose > indexOfBracket)
	    {
		    ArbitraryValue = workingSelector.Substring(indexOfBracket + 1, indexOfBracketClose - indexOfBracket - 1)
			    .Replace('_', ' ').Replace("\\ ", "\\_");

		    rootSegment = workingSelector[..indexOfBracket].TrimEnd('-');

		    if (IsArbitraryCss == false)
		    {
			    var valueTypePackage = SetCustomValueType(ArbitraryValue, AppState);

			    ArbitraryValueType = valueTypePackage.ValueType;
			    ArbitraryValue = valueTypePackage.Value;

			    if (ArbitraryValue.StartsWith("--"))
				    ArbitraryValue = $"var({ArbitraryValue})";

			    else if (ArbitraryValueType == "url" && ArbitraryValue.StartsWith("url(") == false)
				    ArbitraryValue = $"url({ArbitraryValue})";
		    }
	    }
	    
	    #endregion
	    
	    #region Process Modifier Value
	    
	    if (IsArbitraryCss == false)
	    {
		    var indexOfLastSlash = rootSegment.LastIndexOf('/');

		    if (indexOfLastSlash == 0)
		    {
			    IsInvalid = true;
			    return;
		    }
            
            // Handle text-[0.75rem]/1

            if (indexOfLastSlash < 0 && workingSelector.LastIndexOf('/') > workingSelector.LastIndexOf(']'))
            {
                indexOfLastSlash = workingSelector.LastIndexOf('/');

                if (indexOfLastSlash > 0 && indexOfLastSlash < workingSelector.Length - 1)
                {
                    UsesModifier = true;
                    ModifierSegment = workingSelector[indexOfLastSlash..];
                    ModifierValue = workingSelector[(indexOfLastSlash + 1)..];

                    var valueTypePackage = SetCustomValueType(ModifierValue, AppState);

                    ModifierValueType = valueTypePackage.ValueType;
                    ModifierValue = valueTypePackage.Value;
                }
            }

            else if (indexOfLastSlash > 0)
		    {
                UsesModifier = true;
                ModifierSegment = rootSegment[indexOfLastSlash..];

                if (ArbitraryValue == string.Empty)
                    ModifierValue = rootSegment[(indexOfLastSlash + 1)..];

                rootSegment = rootSegment[..indexOfLastSlash];
                
                var valueTypePackage = SetCustomValueType(ModifierValue, AppState);

                ModifierValueType = valueTypePackage.ValueType;
                ModifierValue = valueTypePackage.Value;
            }
	    }
	    
	    #endregion

	    #region Process Variants

	    var indexOfLastColon = rootSegment.LastIndexOf(':');

	    if (indexOfLastColon == 0)
	    {
		    IsInvalid = true;
		    return;
	    }
	    
	    if (indexOfLastColon > 0)
	    {
		    VariantSegment = rootSegment[..(indexOfLastColon + 1)];
		    rootSegment = rootSegment[(indexOfLastColon + 1)..];
		    rightOfVariants = workingSelector[(indexOfLastColon + 1)..];
	    }
	    
	    if (VariantSegment.Length > 0)
	    {
		    var segments = VariantSegment.Split(':', StringSplitOptions.RemoveEmptyEntries);
		    
		    if (segments.Length > 0)
		    {
			    var lastType = string.Empty;

			    foreach (var breakpoint in AppState?.MediaQueryPrefixes.Where(p => segments.Contains(p.Prefix)).OrderBy(k => k.PrefixOrder) ?? Enumerable.Empty<CssMediaQuery>())
			    {
				    if (breakpoint.PrefixType == lastType)
					    continue;

				    MediaQueryVariants.Add(breakpoint.Prefix);

				    lastType = breakpoint.PrefixType;
			    }

			    foreach (var segment in segments)
			    {
				    if (AppState?.PseudoclassPrefixes.ContainsKey(segment) == false)
					    continue;

				    PseudoClassVariants.Add(segment);
			    }
                    
			    FixedSelector = string.Empty;

			    if (MediaQueryVariants.Count > 0)
				    FixedSelector += $"{string.Join(':', MediaQueryVariants)}:";

			    if (PseudoClassVariants.Count > 0)
				    FixedSelector += $"{string.Join(':', PseudoClassVariants)}:";

                foreach (var pseudoClass in PseudoClassVariants)
                {
	                PseudoclassPath += AppState?.PseudoclassPrefixes[pseudoClass];
                }
                
			    FixedSelector += rightOfVariants;
		    }
                
		    BuildVariantSortOrder();
	    }

	    #endregion
	    
        #region Process Important and Negative Overrides
	    
        while (rootSegment.StartsWith('!') || rootSegment.StartsWith('-'))
        {
            if (rootSegment.StartsWith('-'))
            {
                IsNegative = true;
                rootSegment = rootSegment.TrimStart('-');
            }
            
            else if (rootSegment.StartsWith('!'))
            {
                IsImportant = true;
                rootSegment = rootSegment.TrimStart('!');
            }
        }

        #endregion
        
	    if (IsArbitraryCss)
		    return;

	    if (AppState?.UtilityClassCollection.TryGetValue(rootSegment, out var scssUtilityClassGroup) ?? false)
		    ScssUtilityClassGroup = scssUtilityClassGroup;

	    if (ScssUtilityClassGroup is null)
	    {
		    IsInvalid = true;
		    return;
	    }

	    PrefixSegment = ScssUtilityClassGroup.SelectorPrefix;
	    CoreSegment = rootSegment.TrimStart(ScssUtilityClassGroup.SelectorPrefix)?.TrimStart('-') ?? string.Empty;
        
	    await Task.CompletedTask;
    }

    /// <summary>
    /// Escape the CSS class name to be used in a CSS selector.
    /// </summary>
    /// <returns></returns>
    private string EscapeCssClassName()
    {
        if (string.IsNullOrEmpty(Selector))
            return string.Empty;

        if (AppState is null)
            return Selector;
        
        var value = AppState.StringBuilderPool.Get();

        try
        {
            for (var i = 0; i < Selector.Length; i++)
            {
                var c = Selector[i];

                if ((i == 0 && char.IsDigit(c)) || (char.IsLetterOrDigit(c) == false && c != '-' && c != '_'))
                    value.Append('\\');

                value.Append(c);
            }

            var peerGroupSegment = IsPeerGroupSelector(Selector, "peer") ? "peer" : IsPeerGroupSelector(Selector, "group") ? "group" : string.Empty;

            if (string.IsNullOrEmpty(peerGroupSegment))
	            return value.ToString();
            
            if (string.IsNullOrEmpty(PeerGroupPrefix))
            {
	            IsInvalid = HasPeerGroupVariant(AppState, Selector, peerGroupSegment, out _, out var peerGroupPrefix) == false;

	            if (IsInvalid)
		            return value.ToString();
		            
	            PeerGroupPrefix = peerGroupPrefix;
            }

            value.Insert(
	            0,
	            peerGroupSegment == "peer" ? $"{PeerGroupPrefix}~." : $"{PeerGroupPrefix} .");

            return value.ToString();
        }

        catch
        {
            return string.Empty;
        }
        
        finally
        {
            AppState?.StringBuilderPool.Return(value);
        }
    }

    /// <summary>
    /// Create a sorting number from variants.
    /// </summary>
    public void BuildVariantSortOrder()
    {
	    VariantSortOrder = 0;

	    foreach (var breakpoint in AppState?.MediaQueryPrefixes ?? Array.Empty<CssMediaQuery>())
		    if (MediaQueryVariants.Contains(breakpoint.Prefix))
			    VariantSortOrder += breakpoint.Priority;    
    }
    
    /// <summary>
    /// Convenience method for getting styles from the utility class group.
    /// </summary>
    /// <returns></returns>
    public string GetStyles()
    {
	    if (IsArbitraryCss)
		    ScssMarkup = ArbitraryValue + (IsImportant ? " !important;" : ";");
	    else
	    {
		    ScssMarkup = ScssUtilityClassGroup?.GetStyles(this).Replace(";", IsImportant ? " !important;" : ";") ?? string.Empty;

		    if (ScssMarkup.Length == 0)
			    IsInvalid = true;
	    }

	    return ScssMarkup;
    }

    public static bool IsPeerGroupSelector(string selector, string segment)
    {
	    if (segment != "peer" && segment != "group")
		    return false;

	    return 
		    selector.StartsWith($"{segment}:", StringComparison.Ordinal) || 
		    selector.StartsWith($"{segment}-", StringComparison.Ordinal) || 
		    selector.Contains($":{segment}-", StringComparison.Ordinal) || 
		    selector.Contains($":{segment}:", StringComparison.Ordinal);	    
    }
    
    /// <summary>
    /// Determine if a selector has a peer/group variant.
    /// Output variable contains the pseudo class being used. 
    /// Output variable contains the CSS escaped peer class being targeted. 
    /// </summary>
    /// <param name="appState"></param>
    /// <param name="selector"></param>
    /// <param name="segment"></param>
    /// <param name="peerGroupBase"></param>
    /// <param name="peerGroupPrefix"></param>
    /// <returns></returns>
    public static bool HasPeerGroupVariant(SfumatoAppState? appState, string selector, string segment, out string peerGroupBase, out string peerGroupPrefix)
    {
	    peerGroupBase = string.Empty;
	    peerGroupPrefix = string.Empty;

	    if (segment != "peer" && segment != "group")
		    return false;

	    if (IsPeerGroupSelector(selector, segment) == false)
		    return false;
	    
	    var idxOfBracket = selector.IndexOf($"{segment}-[", StringComparison.Ordinal);
    
	    if (idxOfBracket > -1) // group-[.selected]:text-base/1, group-[:hover]:text-base/1
	    {
		    idxOfBracket += $"{segment}-[".Length;
		    
		    var idxOfClosingBracket = selector[idxOfBracket..].IndexOf(']', StringComparison.Ordinal);

		    if (idxOfClosingBracket < 2)
			    return false;

		    idxOfClosingBracket += idxOfBracket;
			    
		    var pseudoClass = selector.Substring(idxOfBracket, idxOfClosingBracket - idxOfBracket);
		    var pc = string.Empty;

		    if (pseudoClass.StartsWith(':'))
		    {
			    pc = peerGroupBase;
			    peerGroupBase = string.Empty;
		    }

		    var workingPseudoClass = string.Empty;
				    
		    for (var i = 0; i < pseudoClass.Length; i++)
		    {
			    var c = pseudoClass[i];

			    if ((i == 0 && char.IsDigit(c)) || (char.IsLetterOrDigit(c) == false && c != '-' && c != '_' && c != '.'))
				    workingPseudoClass += '\\';

			    workingPseudoClass += c;
		    }

		    pseudoClass = workingPseudoClass;
				    
		    var idxOfColon = selector[idxOfClosingBracket..].IndexOf(':', StringComparison.Ordinal);

		    if (idxOfColon > 0)
			    peerGroupBase = selector[(idxOfClosingBracket + idxOfColon + 1)..];
		    else
			    return false;

		    if (selector.Contains($":{segment}-", StringComparison.Ordinal))
			    peerGroupBase = selector[..(selector.IndexOf($":{segment}-", StringComparison.Ordinal) + 1)].Trim(':') + ':' + peerGroupBase;
				    
		    peerGroupPrefix = $"{segment}{pseudoClass}{pc}";

		    return true;
	    }
	    else  // group-hover:text-base/1, group-hover/edit:text-base/1
	    {
		    var pseudoClass = string.Empty;
		    var idxOfHyphen = selector.IndexOf($"{segment}-", StringComparison.Ordinal);

		    if (idxOfHyphen < 0)
			    return false;

		    idxOfHyphen += $"{segment}-".Length - 1;
		    
		    var idxOfSlash = selector.IndexOf('/', StringComparison.Ordinal);

		    if (idxOfHyphen > 0 && idxOfSlash > idxOfHyphen + 1) // group-hover/edit:text-base/1
		    {
			    if (idxOfSlash < selector.Length - 1)
			    {
				    var idxOfColon = selector[idxOfSlash..].IndexOf(':', StringComparison.Ordinal);

				    if (idxOfColon > 0)
				    {
					    idxOfColon += idxOfSlash;
						    
					    peerGroupBase = selector[(idxOfColon + 1)..];
					    pseudoClass = selector.Substring(idxOfHyphen + 1, idxOfSlash - idxOfHyphen - 1);
						
						// 01234567890123456
						// group-hover/edit:text-base/1

					    peerGroupPrefix =
						    $@"{segment}\/{selector.Substring(idxOfSlash + 1, idxOfColon - idxOfSlash - 1)}:{pseudoClass}";
					    
					    if (selector.Contains($":{segment}-", StringComparison.Ordinal))
						    peerGroupBase = selector[..(selector.IndexOf($":{segment}-", StringComparison.Ordinal) + 1)].Trim(':') + ':' + peerGroupBase;
				    }
				    else
				    {
					    return false;
				    }
			    }
				    
			    if (selector.Contains($":{segment}-", StringComparison.Ordinal))
				    peerGroupBase = selector[..(selector.IndexOf($":{segment}-", StringComparison.Ordinal) + 1)].Trim(':') + ':' + peerGroupBase;
		    }
		    else if (idxOfHyphen > 0) // group-hover:text-base
		    {
			    var idxOfColon = selector.LastIndexOf(':');

			    if (idxOfColon > idxOfHyphen + 1)
			    {
				    pseudoClass = selector.Substring(idxOfHyphen + 1, idxOfColon - idxOfHyphen - 1);
				    peerGroupBase = selector[(idxOfColon + 1)..];
				    peerGroupPrefix = $"{segment}:{pseudoClass}";
				    
				    if (selector.Contains($":{segment}-", StringComparison.Ordinal))
					    peerGroupBase = selector[..(selector.IndexOf($":{segment}-", StringComparison.Ordinal) + 1)].Trim(':') + ':' + peerGroupBase;
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

		    return appState?.PseudoclassPrefixes.ContainsKey(pseudoClass) ?? false;
	    }
    }
}

public sealed class ArbitraryValuePackage
{
	public string ValueType { get; set; } = string.Empty;
	public string Value { get; set; } = string.Empty;
}