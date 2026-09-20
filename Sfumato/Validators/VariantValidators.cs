using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Runners;

namespace Sfumato.Validators;

public static class VariantValidators
{
    private static string? GetBracketValue(string variant)
    {
        var bracketIndex = variant.IndexOf('[');

        return bracketIndex > -1 ? variant[(bracketIndex + 1)..^1].ProcessUnderscores() : null;
    }

    private static string? GetCustomValue(string variant)
    {
        var bracketIndex = variant.IndexOf('[');
        var parenIndex = variant.IndexOf('(');

        if (bracketIndex > -1 && parenIndex > bracketIndex)
            parenIndex = -1;

        if (parenIndex > -1 && bracketIndex > parenIndex)
            bracketIndex = -1;
        
        if (bracketIndex > -1)
            return variant[(bracketIndex + 1)..^1].ProcessUnderscores();

        if (parenIndex > -1)
            return $"var{variant[parenIndex..]}";

        return null;
    }

    /// <summary>
    /// Resolves the value portion of an aria variant into its attribute selector suffix.
    /// Accepts a standard boolean name (e.g. "checked") or an arbitrary value
    /// (e.g. "[sort=ascending]", "[current]"). Returns null when the value is invalid.
    /// </summary>
    private static string? GetAriaSelectorSuffix(AppRunner appRunner, string ariaValue)
    {
        if (ariaValue.Length == 0)
            return null;

        // Arbitrary value: [sort=ascending], [current], etc.
        if (ariaValue[0] == '[' && ariaValue[^1] == ']')
        {
            var inner = ariaValue[1..^1].ProcessUnderscores();

            if (inner.Length == 0)
                return null;

            var eqIndex = inner.IndexOf('=');

            // [sort=ascending] -> [aria-sort="ascending"] ; [current] -> [aria-current]
            return eqIndex >= 0
                ? $"[aria-{inner[..eqIndex]}=\"{inner[(eqIndex + 1)..]}\"]"
                : $"[aria-{inner}]";
        }

        // Standard boolean aria variant: look up "aria-{value}" (e.g. aria-checked).
        if (appRunner.Library.PseudoclassPrefixes.TryGetValue($"aria-{ariaValue}", out var pseudoClass))
            return pseudoClass.SelectorSuffix;

        return null;
    }

    private static string? NegateArbitraryAtRule(string value)
    {
        // value is a processed at-rule like "@media print", "@supports (display: grid)",
        // or "@container card style(--c)". Returns the negated at-rule statement, or
        // null when the at-rule cannot be negated.

        if (value.StartsWith("@media", StringComparison.Ordinal))
        {
            var condition = value[6..].Trim();

            return condition.Length > 0 ? $"@media {AppRunnerExtensions.NegateConditionalStatement(condition)}" : null;
        }

        if (value.StartsWith("@supports", StringComparison.Ordinal))
        {
            var condition = value[9..].Trim();

            return condition.Length > 0 ? $"@supports {AppRunnerExtensions.NegateConditionalStatement(condition)}" : null;
        }

        if (value.StartsWith("@container", StringComparison.Ordinal))
        {
            var condition = value[10..].Trim();

            return condition.Length > 0 ? $"@container {AppRunnerExtensions.NegateContainerStatement(condition)}" : null;
        }

        return null;
    }

    private static string NormalizeNotSelectorParts(string selector)
    {
        var trailingComma = EndsWithTopLevelComma(selector);

        var parts = 0;

        foreach (var _ in selector.SplitByTopLevel(','))
            parts++;

        // The splitter drops a trailing empty part (e.g. "a," yields one
        // part); Tailwind keeps it, so account for a top-level trailing comma.

        if (trailingComma)
            parts++;

        if (parts == 1)
            return NormalizeNotSelectorPart(selector);

        var sb = new StringBuilder(selector.Length + parts);
        var appendedParts = 0;

        foreach (var part in selector.SplitByTopLevel(','))
        {
            if (appendedParts++ > 0)
                sb.Append(", ");

            sb.Append(NormalizeNotSelectorPart(part.ToString()));
        }

        // Append the trailing empty part dropped by the splitter.

        if (trailingComma)
            sb.Append(", ");

        return sb.ToString();
    }

    private static bool EndsWithTopLevelComma(string selector)
    {
        if (selector.Length == 0 || selector[^1] != ',')
            return false;

        var bracketDepth = 0;
        var parenDepth = 0;

        for (var i = 0; i < selector.Length - 1; i++)
        {
            var c = selector[i];

            if (c == '[') bracketDepth++;
            else if (c == ']') bracketDepth--;
            else if (c == '(') parenDepth++;
            else if (c == ')') parenDepth--;
        }

        return bracketDepth == 0 && parenDepth == 0;
    }

    private static string NormalizeNotSelectorPart(string part)
    {
        var span = part.AsSpan();

        // A universal selector directly followed by a pseudo-class, class, id
        // or attribute is dropped (lightningcss normalization). Leading
        // whitespace does not prevent the drop.

        var k = 0;

        while (k < span.Length && char.IsWhiteSpace(span[k]))
            k++;

        if (span.Length - k > 1 && span[k] == '*' && span[k + 1] is ':' or '.' or '#' or '[')
            span = span[(k + 1)..];

        // Normalize top-level combinator spacing to a single space on each side
        // and collapse whitespace runs (lightningcss serialization).

        var sb = new StringBuilder(span.Length + 4);
        var bracketDepth = 0;
        var parenDepth = 0;

        for (var i = 0; i < span.Length; i++)
        {
            var c = span[i];

            if (c == '[') bracketDepth++;
            else if (c == ']') bracketDepth--;
            else if (c == '(') parenDepth++;
            else if (c == ')') parenDepth--;

            // Combinators and whitespace inside brackets or parentheses are part
            // of an attribute value or pseudo-class argument and are left
            // untouched.

            if (bracketDepth > 0 || parenDepth > 0)
            {
                sb.Append(c);

                continue;
            }

            if (c is '>' or '+' or '~')
            {
                while (sb.Length > 0 && char.IsWhiteSpace(sb[^1]))
                    sb.Length--;

                var j = i + 1;

                while (j < span.Length && char.IsWhiteSpace(span[j]))
                    j++;

                sb.Append(' ').Append(c).Append(' ');

                i = j - 1;

                continue;
            }

            if (char.IsWhiteSpace(c))
            {
                var j = i + 1;

                while (j < span.Length && char.IsWhiteSpace(span[j]))
                    j++;

                if (sb.Length > 0)
                    sb.Append(' ');

                i = j - 1;

                continue;
            }

            sb.Append(c);
        }

        return sb.ToString();
    }

    public static bool TryGetVariant(this string variant, AppRunner appRunner, out VariantMetadata? metadata)
    {
        metadata = null;
        
        var indexOfSlash = variant.LastIndexOf('/');
        var isContainerQuery = variant[0] == '@'; // true if a container query

        if (appRunner.Library.AllVariants.TryGetLongestMatchingPrefix(indexOfSlash > -1 ? variant[..indexOfSlash] : variant, out var prefix, out var variantMetadata))
        {
            if (variant.Length == prefix!.Length)
            {
                metadata = variantMetadata;
                return true;
            }
        }
        else
        {
            if (variant[0] != '[' && isContainerQuery == false)
                return false;
        }

        metadata = variantMetadata?.CreateNewVariant();
        
        #region Standard variants
        
        if (variantMetadata?.SpecialCase == false)
        {
            var customValue = GetCustomValue(variant);
            
            if (customValue is not null) // Custom value variants
            {
                if (variantMetadata.SelectorSuffix.Length > 0)
                    metadata!.SelectorSuffix = variantMetadata.SelectorSuffix.Replace("{0}", customValue);
                else
                    metadata!.Statement = variantMetadata.Statement.Replace("{0}", customValue);
                
                return true;
            }

            if (variantMetadata.CanHaveNumericSuffix) // Uses custom numeric suffix
            {
                var hasCustomNumericValue = char.IsAsciiDigit(variant[^1]);

                if (hasCustomNumericValue == false)
                    return false;
                
                var lastHyphenIndex = variant.LastIndexOf('-');

                if (lastHyphenIndex < 0 || appRunner.Library.PseudoclassPrefixes.TryGetValue(variant[..(lastHyphenIndex + 1)], out variantMetadata) == false)
                    return false;
                
                if (variantMetadata.SelectorSuffix.Length > 0)
                    metadata!.SelectorSuffix = variantMetadata.SelectorSuffix.Replace("{0}", variant[(lastHyphenIndex + 1)..].ProcessUnderscores());
                else
                    metadata!.SelectorSuffix = variantMetadata.Statement.Replace("{0}", variant[(lastHyphenIndex + 1)..].ProcessUnderscores());

                return true;
            }
        }
        
        #endregion
        
        #region Special case variants
        
        if (variantMetadata?.SpecialCase == true)
        {
            #region Container queries

            if (isContainerQuery)
            {
                var variantValue = indexOfSlash > 0 ? variant[..indexOfSlash] : variant;

                return appRunner.Library.ContainerQueryPrefixes.TryGetValue(variantValue, out metadata);
            }

            #endregion
            
            #region Min/max breakpoints
            
            if (variant.Length > 6 && (variant.StartsWith("min-[", StringComparison.Ordinal) || variant.StartsWith("max-[", StringComparison.Ordinal)))
            {
                var customValue = GetCustomValue(variant);
                
                if (customValue is not null) // Custom value variants
                {
                    metadata!.PrefixType = "wrapper";
                    metadata.Statement = $"@media {variantMetadata.Statement.Replace("{0}", customValue)}";
                    return true;
                }
            }            

            #endregion
            
            #region Data attributes
            
            if (variant.Length > 5 && variant.StartsWith("data-", StringComparison.Ordinal))
            {
                var customValue = GetBracketValue(variant);
                
                if (customValue is not null) // Custom value variants
                {
                    // data-[size=large]:

                    metadata!.SelectorSuffix = $"[{customValue}]";
                }
                else
                {
                    // data-active:

                    metadata!.SelectorSuffix = $"[{variant}]";
                }

                return true;
            }
        
            if (variant.Length > 9 && variant.StartsWith("not-data-", StringComparison.Ordinal))
            {
                var customValue = GetBracketValue(variant);

                if (customValue is not null) // Custom value variants
                {
                    // not-data-[size=large]:

                    metadata!.SelectorSuffix = $":not({customValue.ProcessUnderscores()})";
                }
                else
                {
                    // not-data-active:

                    metadata!.SelectorSuffix = $":not([{variant.TrimStart("not-")}])";
                }
            
                return true;
            }
            
            #endregion
            
            #region ARIA attributes

            if (variant.Length > 9 && variant.StartsWith("not-aria-", StringComparison.Ordinal))
            {
                // not-aria-[sort=ascending]: (standard not-aria-* are auto-generated in Library)

                var ariaSelector = GetAriaSelectorSuffix(appRunner, variant[9..]);

                if (ariaSelector is null)
                    return false;

                metadata!.SelectorSuffix = $":not({ariaSelector})";

                return true;
            }

            if (variant.Length > 5 && variant.StartsWith("aria-", StringComparison.Ordinal))
            {
                // aria-[sort=ascending]: or aria-[current]: (standard aria-* are exact trie matches)

                var ariaSelector = GetAriaSelectorSuffix(appRunner, variant[5..]);

                if (ariaSelector is null)
                    return false;

                metadata!.SelectorSuffix = ariaSelector;

                return true;
            }

            #endregion
            
            #region Not arbitrary (not-[...])

            if (variant.Length > 6 && variant.StartsWith("not-[", StringComparison.Ordinal))
            {
                // not-* variants do not accept modifiers (Tailwind emits nothing for them).

                if (indexOfSlash > variant.LastIndexOf(']'))
                    return false;

                var customValue = GetBracketValue(variant);

                if (customValue is null || customValue.Trim().Length < 1)
                    return false;

                if (customValue[0] == '@')
                {
                    // not-[@media_print]:, not-[@supports(display:grid)]:, not-[@container_style(--a)]:
                    // Negate the at-rule condition and emit it as a wrapper.

                    var negatedStatement = NegateArbitraryAtRule(customValue);

                    if (negatedStatement is null)
                        return false;

                    metadata!.PrefixType = "wrapper";
                    metadata.PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1;
                    metadata.Statement = negatedStatement;

                    return true;
                }

                // not-[:checked]:, not-[.group]:, not-[&:hover]: etc.
                // Negate an arbitrary selector by wrapping it in :not(...).

                var selector = customValue;

                // Pseudo-elements cannot be negated (Tailwind emits nothing for them).
                if (selector.Contains("::", StringComparison.Ordinal))
                    return false;

                // Relative combinators cannot be negated (Tailwind emits nothing for them).
                if (selector[0] is '>' or '+' or '~')
                    return false;

                var hasAmpersand = selector.Contains('&', StringComparison.Ordinal);

                // Each & refers to the element itself (*).
                if (hasAmpersand)
                    selector = selector.Replace("&", "*", StringComparison.Ordinal);

                // Normalize top-level comma-separated parts (Tailwind serializes selector
                // lists with ", ") and drop a universal selector that is directly followed
                // by a pseudo-class, class, id or attribute (lightningcss normalization).

                var normalized = NormalizeNotSelectorParts(selector);

                // Values without & are wrapped in :is() (Tailwind behavior).
                metadata!.SelectorSuffix = hasAmpersand ? $":not({normalized})" : $":not(:is({normalized}))";

                return true;
            }

            #endregion
            
            #region Groups
            
            if (variant.Length > 12 && variant.StartsWith("group-has-[", StringComparison.Ordinal))
            {
                // group-has-[a]: or group-has-[p.my-class]: etc.

                metadata!.SelectorSuffix = $":is(:where(.group):has(:is({variant[11..^1].ProcessUnderscores()})) *)";
                metadata.PrioritySort = 99;

                return true;
            }
            
            if (variant.Length > 11 && variant.StartsWith("group-aria-", StringComparison.Ordinal))
            {
                // group-aria-checked: or group-aria-[sort=ascending]: etc.

                var ariaSelector = GetAriaSelectorSuffix(appRunner, variant[11..]);

                if (ariaSelector is null)
                    return false;

                metadata!.SelectorSuffix = $":is(:where(.group){ariaSelector} *)";
                metadata.PrioritySort = 99;

                return true;
            }
            
            if (variant.Length > 6 && variant.StartsWith("group-", StringComparison.Ordinal))
            {
                // group-hover: group-focus/item: etc.

                var variantValue = indexOfSlash > 0 ? variant[6..indexOfSlash] : variant[6..];
                var slashValue = indexOfSlash > 0 ? variant[indexOfSlash..] : string.Empty;

                if (variantValue[0] == '[' && variantValue[^1] == ']')
                {
                    // group-[.is-published]:

                    variantValue = variantValue[1..^1];

                    if (string.IsNullOrEmpty(variantValue))
                        return false;

                    metadata!.PrefixType = "prefix";
                    metadata.SelectorPrefix = $".group{slashValue.Replace("/", "\\/")}{variantValue.ProcessUnderscores()} ";

                    return true;
                }

                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass))
                {
                    // group-hover:

                    metadata!.PrefixType = "prefix";
                    metadata.SelectorPrefix = $".group{slashValue.Replace("/", "\\/")}{pseudoClass.SelectorSuffix} ";
                
                    return true;
                }

                return false;
            }
            
            #endregion

            #region Peers
            
            if (variant.Length > 11 && variant.StartsWith("peer-has-[", StringComparison.Ordinal))
            {
                // peer-has-[a]: or peer-has-[p.my-class]: etc.

                metadata!.SelectorSuffix = $":is(:where(.peer):has(:is({variant[10..^1].ProcessUnderscores()})) ~ *)";
                metadata.PrioritySort = 99;

                return true;
            }
            
            if (variant.Length > 10 && variant.StartsWith("peer-aria-", StringComparison.Ordinal))
            {
                // peer-aria-checked: or peer-aria-[sort=descending]: etc.

                var ariaSelector = GetAriaSelectorSuffix(appRunner, variant[10..]);

                if (ariaSelector is null)
                    return false;

                metadata!.SelectorSuffix = $":is(:where(.peer){ariaSelector} ~ *)";
                metadata.PrioritySort = 99;

                return true;
            }
            
            if (variant.Length > 5 && variant.StartsWith("peer-", StringComparison.Ordinal))
            {
                // peer-hover: peer-focus: etc.

                var variantValue = indexOfSlash > 0 ? variant[5..indexOfSlash] : variant[5..];
                var slashValue = indexOfSlash > 0 ? variant[indexOfSlash..] : string.Empty;

                if (variantValue[0] == '[' && variantValue[^1] == ']')
                {
                    // peer-[.is-published]:

                    metadata!.PrefixType = "prefix";
                    metadata.SelectorPrefix = $".peer{slashValue.Replace("/", "\\/")}{variantValue[1..^1].ProcessUnderscores()} ~ ";

                    return true;
                }
                
                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass))
                {
                    // peer-hover:

                    metadata!.PrefixType = "prefix";
                    metadata.SelectorPrefix = $".peer{slashValue.Replace("/", "\\/")}{pseudoClass.SelectorSuffix} ~ ";

                    return true;
                }

                return false;
            }
            
            #endregion
            
            #region Implicit group (in-*)

            if (variant.Length > 3 && variant.StartsWith("in-", StringComparison.Ordinal))
            {
                // in-hover: in-focus: etc. — implicit group variants (no .group class required)

                var innerVariant = variant[3..];

                // in-not-* is not supported (Tailwind emits nothing for it)
                if (innerVariant.StartsWith("not-", StringComparison.Ordinal))
                    return false;

                // Resolve the inner variant to obtain its selector suffix.
                if (innerVariant.TryGetVariant(appRunner, out var innerMetadata) == false || innerMetadata is null)
                    return false;

                // Only selector-based (pseudo-class / attribute) variants are compatible with in-*.
                if (innerMetadata.PrefixType != "pseudoclass" || innerMetadata.SelectorSuffix.Length == 0)
                    return false;

                // Pseudo-elements (::before, ::after, ...) are not compatible (Tailwind emits nothing for them).
                if (innerMetadata.SelectorSuffix.StartsWith("::", StringComparison.Ordinal))
                    return false;

                metadata!.PrefixType = "prefix";
                metadata.SelectorPrefix = $":where({innerMetadata.SelectorSuffix}) ";

                return true;
            }

            #endregion

            #region Has
            
            if (variant.Length > 6 && variant.StartsWith("has-[", StringComparison.Ordinal))
            {
                // has-[a]: or has-[a.link]: etc.

                metadata!.SelectorSuffix = $":has({variant[5..^1].ProcessUnderscores()})";

                return true;
            }

            if (variant.Length > 5 && variant.StartsWith("has-", StringComparison.Ordinal))
            {
                // has-hover: has-focus: etc.

                var variantValue = variant[4..];

                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass) == false)
                    return false;

                metadata!.SelectorSuffix = $":has({pseudoClass.SelectorSuffix})";

                return true;
            }
            
            #endregion
            
            #region Supports

            if (variant.Length > 9 && variant.StartsWith("supports-", StringComparison.Ordinal))
            {
                var indexOfBracket = variant.IndexOf('[');
                var variantValue = indexOfBracket > 0 ? variant[(indexOfBracket + 1)..^1] : variant[9..];
        
                if (indexOfBracket > 0)
                {
                    metadata!.PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1;
                    metadata.Statement = $"({variantValue.ProcessUnderscores()})";                    

                    return true;
                }

                appRunner.Library.CssPropertyNamesWithColons.TryGetLongestMatchingPrefix($"{variantValue}:", out var match, out _);

                if (string.IsNullOrEmpty(match) == false)
                {
                    metadata!.PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1;
                    metadata.Statement = $"({match.TrimEnd(':')}: initial)";                    

                    return true;
                }

                return false;
            }

            if (variant.Length > 13 && variant.StartsWith("not-supports-", StringComparison.Ordinal))
            {
                var indexOfBracket = variant.IndexOf('[');
                var variantValue = indexOfBracket > 0 ? variant[(indexOfBracket + 1)..^1] : variant[13..];

                if (indexOfBracket > 0)
                {
                    metadata!.PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1;
                    metadata.Statement = $"not ({variantValue.ProcessUnderscores()})";                    

                    return true;
                }

                appRunner.Library.CssPropertyNamesWithColons.TryGetLongestMatchingPrefix($"{variantValue}:", out var match, out _);

                if (string.IsNullOrEmpty(match) == false)
                {
                    metadata!.PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1;
                    metadata.Statement = $"not ({match.TrimEnd(':')}: initial)";                    

                    return true;
                }

                return false;
            }

            #endregion
        }
        
        #endregion
        
        #region Arbitrary CSS variants

        if (variant[0] != '[')
            return false;
        
        var customVariantValue = GetBracketValue(variant);

        if (customVariantValue is null || customVariantValue.Length != variant.Length - 2 || customVariantValue.Length < 2)
            return false;
        
        if (customVariantValue[0] == '@')
        {
            // [@supports_(display:grid)]

            var usesRazorSyntax = customVariantValue[1] == '@';
            
            metadata = new VariantMetadata
            {
                PrefixType = "wrapper",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = usesRazorSyntax ? customVariantValue[1..] : customVariantValue,
                IsRazorSyntax = usesRazorSyntax
            };

            return true;
        }

        if (customVariantValue[0] == '&')
        {
            // [&.active]:

            metadata = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                PrefixOrder = 1,
                SelectorSuffix = $"{customVariantValue[1..]}"
            };

            return true;
        }

        #endregion

        return false;
    }
}