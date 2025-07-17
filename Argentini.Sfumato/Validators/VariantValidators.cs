namespace Argentini.Sfumato.Validators;

public static class VariantValidators
{
    private static string? GetBracketValue(string variant)
    {
        var bracketIndex = variant.IndexOf('[');

        return bracketIndex > -1 ? variant[(bracketIndex + 1)..^1].Replace('_', ' ') : null;
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
            return variant[(bracketIndex + 1)..^1].Replace('_', ' ');

        if (parenIndex > -1)
            return $"var{variant[parenIndex..]}";

        return null;
    }
    
    public static bool TryGetVariant(this string variant, AppRunner appRunner, out VariantMetadata? variantMetadata)
    {
        variantMetadata = null;
        
        var indexOfSlash = variant.LastIndexOf('/');
        var indexOfAt = variant.IndexOf('@'); // if zero a container query

        if (appRunner.Library.AllVariants.TryGetLongestMatchingPrefix(indexOfSlash > -1 ? variant[..indexOfSlash] : variant, out var prefix, out variantMetadata))
        {
            if (variant.Length == prefix!.Length)
                return true;
        }
        else
        {
            if (variant[0] != '[' && indexOfAt != 0)
                return false;
        }

        if (variantMetadata?.SpecialCase == false)
        {
            var customValue = GetCustomValue(variant);
            
            if (customValue is not null) // Custom value variants
            {
                if (variantMetadata.SelectorSuffix.Length > 0)
                    variantMetadata = variantMetadata.CreateNewVariant(suffix: variantMetadata.SelectorSuffix.Replace("{0}", customValue));
                else
                    variantMetadata = variantMetadata.CreateNewVariant(statement: variantMetadata.Statement.Replace("{0}", customValue));
                
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
                    variantMetadata = variantMetadata.CreateNewVariant(suffix: variantMetadata.SelectorSuffix.Replace("{0}", variant[(lastHyphenIndex + 1)..].Replace('_', ' ')));
                else
                    variantMetadata = variantMetadata.CreateNewVariant(suffix: variantMetadata.Statement.Replace("{0}", variant[(lastHyphenIndex + 1)..].Replace('_', ' ')));

                return true;
            }
        }
        
        if (variantMetadata?.SpecialCase == true)
        {
            #region Container queries

            if (indexOfAt == 0)
            {
                var variantValue = indexOfSlash > 0 ? variant[..indexOfSlash] : variant;

                return appRunner.Library.ContainerQueryPrefixes.TryGetValue(variantValue, out variantMetadata);
            }

            #endregion
            
            #region Min/max breakpoints
            
            if (variant.Length > 6 && (variant.StartsWith("min-[", StringComparison.Ordinal) || variant.StartsWith("max-[", StringComparison.Ordinal)))
            {
                var customValue = GetCustomValue(variant);
                
                if (customValue is not null) // Custom value variants
                {
                    variantMetadata = variantMetadata.CreateNewVariant("wrapper", statement: $"@media {variantMetadata.Statement.Replace("{0}", customValue)}");
                    
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

                    variantMetadata = variantMetadata.CreateNewVariant(suffix: $"[{customValue}]");
                }
                else
                {
                    // data-active:

                    variantMetadata = variantMetadata.CreateNewVariant(suffix: $"[{variant}]");
                }

                return true;
            }
        
            if (variant.Length > 9 && variant.StartsWith("not-data-", StringComparison.Ordinal))
            {
                var customValue = GetBracketValue(variant);

                if (customValue is not null) // Custom value variants
                {
                    // not-data-[size=large]:

                    variantMetadata = variantMetadata.CreateNewVariant(suffix: $":not({customValue.Replace('_', ' ')})");
                }
                else
                {
                    // not-data-active:

                    variantMetadata = variantMetadata.CreateNewVariant(suffix: $":not([{variant.TrimStart("not-")}])");
                }
            
                return true;
            }
            
            #endregion
            
            #region Groups
            
            if (variant.Length > 12 && variant.StartsWith("group-has-[", StringComparison.Ordinal))
            {
                // group-has-[a]: or group-has-[p.my-class]: etc.

                variantMetadata = variantMetadata.CreateNewVariant(suffix: $":is(:where(.group):has(:is({variant[11..^1].Replace('_', ' ')})) *)", prioritySort: 99);

                return true;
            }
            
            if (variant.Length > 11 && variant.StartsWith("group-aria-", StringComparison.Ordinal))
            {
                // group-aria-checked:

                var variantValue = variant[11..];

                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass) == false)
                    return false;

                variantMetadata = variantMetadata.CreateNewVariant(suffix: $":is(:where(.group{pseudoClass.SelectorSuffix}) *)", prioritySort: 99);
                
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
                            
                    variantMetadata = variantMetadata.CreateNewVariant("prefix", prefix: $".group{slashValue.Replace("/", "\\/")}{variantValue.Replace('_', ' ')} ");

                    return true;
                }

                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass))
                {
                    // group-hover:

                    variantMetadata = variantMetadata.CreateNewVariant("prefix", prefix: $".group{slashValue.Replace("/", "\\/")}{pseudoClass.SelectorSuffix} ");
                
                    return true;
                }

                return false;
            }
            
            #endregion

            #region Peers
            
            if (variant.Length > 11 && variant.StartsWith("peer-has-[", StringComparison.Ordinal))
            {
                // peer-has-[a]: or peer-has-[p.my-class]: etc.

                variantMetadata = variantMetadata.CreateNewVariant(suffix: $":is(:where(.peer):has(:is({variant[10..^1].Replace('_', ' ')})) ~ *)", prioritySort: 99);

                return true;
            }
            
            if (variant.Length > 10 && variant.StartsWith("peer-aria-", StringComparison.Ordinal))
            {
                // peer-aria-checked:

                var variantValue = variant[10..];

                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass) == false)
                    return false;

                variantMetadata = variantMetadata.CreateNewVariant(suffix: $":is(:where(.peer{pseudoClass.SelectorSuffix}) ~ *)", prioritySort: 99);

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

                    variantMetadata = variantMetadata.CreateNewVariant("prefix", prefix: $".peer{slashValue.Replace("/", "\\/")}{variantValue[1..^1].Replace('_', ' ')} ~ ");

                    return true;
                }
                
                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass))
                {
                    // peer-hover:

                    variantMetadata = variantMetadata.CreateNewVariant("prefix", prefix: $".peer{slashValue.Replace("/", "\\/")}{pseudoClass.SelectorSuffix} ~ ");

                    return true;
                }

                return false;
            }
            
            #endregion

            #region Has
            
            if (variant.Length > 6 && variant.StartsWith("has-[", StringComparison.Ordinal))
            {
                // has-[a]: or has-[a.link]: etc.

                variantMetadata = variantMetadata.CreateNewVariant(suffix: $":has({variant[5..^1].Replace('_', ' ')})");

                return true;
            }

            if (variant.Length > 5 && variant.StartsWith("has-", StringComparison.Ordinal))
            {
                // has-hover: has-focus: etc.

                var variantValue = variant[4..];

                if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variantValue, out var pseudoClass) == false)
                    return false;

                variantMetadata = variantMetadata.CreateNewVariant(suffix: $":has({pseudoClass.SelectorSuffix})");

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
                    variantMetadata = variantMetadata.CreateNewVariant(prefixOrder: appRunner.Library.SupportsQueryPrefixes.Count + 1, statement: $"({variantValue.Replace('_', ' ')})");                    

                    return true;
                }

                appRunner.Library.CssPropertyNamesWithColons.TryGetLongestMatchingPrefix($"{variantValue}:", out var match, out _);

                if (string.IsNullOrEmpty(match) == false)
                {
                    variantMetadata = variantMetadata.CreateNewVariant(prefixOrder: appRunner.Library.SupportsQueryPrefixes.Count + 1, statement: $"({match.TrimEnd(':')}: initial)");                    

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
                    variantMetadata = variantMetadata.CreateNewVariant(prefixOrder: appRunner.Library.SupportsQueryPrefixes.Count + 1, statement: $"not ({variantValue.Replace('_', ' ')})");                    

                    return true;
                }

                appRunner.Library.CssPropertyNamesWithColons.TryGetLongestMatchingPrefix($"{variantValue}:", out var match, out _);

                if (string.IsNullOrEmpty(match) == false)
                {
                    variantMetadata = variantMetadata.CreateNewVariant(prefixOrder: appRunner.Library.SupportsQueryPrefixes.Count + 1, statement: $"not ({match.TrimEnd(':')}: initial)");                    

                    return true;
                }

                return false;
            }

            #endregion
        }
        
        #region Custom variants

        if (variant[0] != '[')
            return false;
        
        var customVariantValue = GetBracketValue(variant);

        if (customVariantValue is null || customVariantValue.Length != variant.Length - 2)
            return false;
        
        if (customVariantValue.StartsWith('@'))
        {
            // [@supports_(display:grid)]

            variantMetadata = new VariantMetadata
            {
                PrefixType = "wrapper",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = customVariantValue
            };

            return true;
        }

        if (customVariantValue.StartsWith('&'))
        {
            // [&.active]:

            variantMetadata = new VariantMetadata
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