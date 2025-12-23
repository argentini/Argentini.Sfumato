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
                // group-aria-checked:

                var variantValue = variant[11..];

                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass) == false)
                    return false;

                metadata!.SelectorSuffix = $":is(:where(.group{pseudoClass.SelectorSuffix}) *)";
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
                // peer-aria-checked:

                var variantValue = variant[10..];

                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass) == false)
                    return false;

                metadata!.SelectorSuffix = $":is(:where(.peer{pseudoClass.SelectorSuffix}) ~ *)";
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