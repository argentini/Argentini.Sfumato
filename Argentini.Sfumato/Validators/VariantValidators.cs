namespace Argentini.Sfumato.Validators;

public static class VariantValidators
{
    public static bool TryVariantIsMediaQuery(this string variant, AppRunner appRunner, out VariantMetadata? cssMediaQuery)
    {
        cssMediaQuery = null;

        return appRunner.Library.MediaQueryPrefixes.TryGetValue(variant, out cssMediaQuery);
    }

    public static bool TryVariantIsStartingStyleQuery(this string variant, AppRunner appRunner, out VariantMetadata? cssMediaQuery)
    {
        cssMediaQuery = null;

        return appRunner.Library.StartingStyleQueryPrefixes.TryGetValue(variant, out cssMediaQuery);
    }

    public static bool TryVariantIsContainerQuery(this string variant, AppRunner appRunner, out VariantMetadata? cssMediaQuery)
    {
        cssMediaQuery = null;

        var variantValue = variant;
        var indexOfSlash = variant.LastIndexOf('/');

        if (indexOfSlash == 0 || indexOfSlash >= variantValue.Length)
            return false;

        if (indexOfSlash > 0)
            variantValue = variantValue[..indexOfSlash];

        return appRunner.Library.ContainerQueryPrefixes.TryGetValue(variantValue, out cssMediaQuery);
    }

    public static bool TryVariantIsPseudoClass(this string variant, AppRunner appRunner, out VariantMetadata? pseudoClass)
    {
        pseudoClass = null;

        if (appRunner.Library.PseudoclassPrefixes.TryGetValue(variant, out pseudoClass))
            return true;
        
        if (variant[0] == 'd' && variant.StartsWith("data-", StringComparison.OrdinalIgnoreCase))
            if (appRunner.Library.PseudoclassPrefixes.TryGetValue("data-", out pseudoClass))
                return true;
        
        var bracketIndex = variant.IndexOf('[');

        if (bracketIndex > 0 && appRunner.Library.PseudoclassPrefixes.TryGetValue(variant[..bracketIndex], out pseudoClass))
        {
            pseudoClass = pseudoClass.CreateNewPseudoClass(pseudoClass.SelectorSuffix.Replace("{0}", variant[(bracketIndex + 1)..^1].Replace('_', ' ')));
            return true;
        }
        
        var hasCustomValue = char.IsAsciiDigit(variant[^1]);

        if (hasCustomValue == false)
            return false;
        
        var lastHyphenIndex = variant.LastIndexOf('-');

        if (lastHyphenIndex < 0 || appRunner.Library.PseudoclassPrefixes.TryGetValue(variant[..(lastHyphenIndex + 1)], out pseudoClass) == false)
            return false;
        
        pseudoClass = pseudoClass.CreateNewPseudoClass(pseudoClass.SelectorSuffix.Replace("{0}", variant[(lastHyphenIndex + 1)..].Replace('_', ' ')));

        return true;
    }

    public static bool TryVariantIsGroup(this string variant, AppRunner appRunner, out VariantMetadata? group)
    {
        group = null;

        if (variant.StartsWith("group-", StringComparison.Ordinal) == false)
            return false;
        
        if (variant.Length > 12 && variant.StartsWith("group-has-[", StringComparison.Ordinal))
        {
            // group-has-[a]: or group-has-[p.my-class]: etc.

            var variantValue = variant[11..^1];
            
            group = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.group):has(:is({variantValue.Replace('_', ' ')})) *)",
                PrioritySort = 99
            };
        }
        else if (variant.Length > 11 && variant.StartsWith("group-aria-", StringComparison.Ordinal))
        {
            // group-aria-checked:

            var variantValue = variant[11..];

            if (string.IsNullOrEmpty(variantValue) || TryVariantIsPseudoClass(variantValue, appRunner, out var pseudoClass) == false)
                return false;

            if (pseudoClass is null)
                return false;
            
            group = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.group{pseudoClass.SelectorSuffix}) *)",
                PrioritySort = 99
            };
        }
        else if (variant.Length > 6)
        {
            // group-hover: group-focus/item: etc.

            var variantValue = variant[6..];

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

            if (variantValue[0] == '[' && variantValue[^1] == ']')
            {
                // group-[.is-published]:

                variantValue = variantValue[1..^1];

                if (string.IsNullOrEmpty(variantValue))
                    return false;
                        
                group = new VariantMetadata
                {
                    PrefixType = "prefix",
                    SelectorPrefix = $".group{slashValue.Replace("/", "\\/")}{variantValue.Replace('_', ' ')} ",
                };
            }
            else if (TryVariantIsPseudoClass(variantValue, appRunner, out var pseudoClass))
            {
                // group-hover:

                if (pseudoClass is null)
                    return false;

                group = new VariantMetadata
                {
                    PrefixType = "prefix",
                    SelectorPrefix = $".group{slashValue.Replace("/", "\\/")}{pseudoClass.SelectorSuffix} ",
                };
            }
            else
            {
                return false;
            }
        }

        return true;
    }
    
    public static bool TryVariantIsPeer(this string variant, AppRunner appRunner, out VariantMetadata? peer)
    {
        peer = null;

        if (variant.StartsWith("peer-", StringComparison.Ordinal) == false)
            return false;

        if (variant.Length > 11 && variant.StartsWith("peer-has-[", StringComparison.Ordinal))
        {
            // peer-has-[a]: or peer-has-[p.my-class]: etc.

            var variantValue = variant[10..^1];

            peer = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.peer):has(:is({variantValue.Replace('_', ' ')})) ~ *)",
                PrioritySort = 99
            };
        }
        else if (variant.Length > 10 && variant.StartsWith("peer-aria-", StringComparison.Ordinal))
        {
            // peer-aria-checked:

            var variantValue = variant[10..];

            if (TryVariantIsPseudoClass(variantValue, appRunner, out var pseudoClass) == false)
                return false;

            if (pseudoClass is null)
                return false;
            
            peer = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.peer{pseudoClass.SelectorSuffix}) ~ *)",
                PrioritySort = 99
            };
        }
        else if (variant.Length > 5)
        {
            // peer-hover: peer-focus: etc.

            var variantValue = variant[5..];
            var indexOfSlash = variantValue.LastIndexOf('/');
            var slashValue = string.Empty;

            if (indexOfSlash == 0 || indexOfSlash >= variantValue.Length)
                return false;

            if (indexOfSlash > 0)
            {
                slashValue = variantValue[indexOfSlash..];
                variantValue = variantValue[..indexOfSlash];
            }

            if (variantValue[0] == '[' && variantValue[^1] == ']')
            {
                // peer-[.is-published]:

                variantValue = variantValue[1..^1];

                peer = new VariantMetadata
                {
                    PrefixType = "prefix",
                    SelectorPrefix = $".peer{slashValue.Replace("/", "\\/")}{variantValue.Replace('_', ' ')} ~ ",
                };
            }
            else if (TryVariantIsPseudoClass(variantValue, appRunner, out var pseudoClass))
            {
                // peer-hover:

                if (pseudoClass is null)
                    return false;

                peer = new VariantMetadata
                {
                    PrefixType = "prefix",
                    SelectorPrefix = $".peer{slashValue.Replace("/", "\\/")}{pseudoClass.SelectorSuffix} ~ ",
                };
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public static bool TryVariantIsHas(this string variant, AppRunner appRunner, out VariantMetadata? has)
    {
        has = null;

        if (variant.StartsWith("has-", StringComparison.Ordinal) == false)
            return false;
        
        if (variant.Length > 6 && variant.StartsWith("has-[", StringComparison.Ordinal))
        {
            // has-[a]: or has-[a.link]: etc.

            var variantValue = variant[5..^1];

            has = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":has({variantValue.Replace('_', ' ')})"
            };
        }
        else if (variant.Length > 5)
        {
            // has-hover: has-focus: etc.

            var variantValue = variant[4..];

            if (TryVariantIsPseudoClass(variantValue, appRunner, out var pseudoClass) == false)
                return false;
            
            has = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                Statement = $":has({pseudoClass?.SelectorSuffix})"
            };
        }

        return true;
    }

    public static bool TryVariantIsSupports(this string variant, AppRunner appRunner, out VariantMetadata? supports)
    {
        supports = null;

        if (variant.StartsWith("supports-", StringComparison.Ordinal) == false)
            return false;

        if (appRunner.Library.SupportsQueryPrefixes.TryGetValue(variant, out supports))
            return true;

        if (variant.Length > 11 && variant.StartsWith("supports-[", StringComparison.Ordinal))
        {
            // supports-[display:grid]:

            var variantValue = variant[10..^1];

            supports = new VariantMetadata
            {
                PrefixType = "supports",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = $"({variantValue.Replace('_', ' ')})"
            };
        }
        else if (variant.Length > 9)
        {
            // supports-hover:

            var variantValue = variant[9..];

            var match = appRunner.Library.CssPropertyNamesWithColons.GetLongestMatchingPrefix($"{variantValue}:")?.TrimEnd(':');

            if (string.IsNullOrEmpty(match))
                return false;

            supports = new VariantMetadata
            {
                PrefixType = "supports",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = $"({match}: initial)"
            };
        }

        return true;
    }

    public static bool TryVariantIsNotSupports(this string variant, AppRunner appRunner, out VariantMetadata? notSupports)
    {
        notSupports = null;

        if (variant.StartsWith("not-supports-", StringComparison.Ordinal) == false)
            return false;

        if (variant.Length > 15 && variant.StartsWith("not-supports-[", StringComparison.Ordinal))
        {
            // not-supports-[display:grid]:

            var variantValue = variant[14..^1];

            notSupports = new VariantMetadata
            {
                PrefixType = "not-supports",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = $"not ({variantValue.Replace('_', ' ')})"
            };
        }
        else
        {
            // not-supports-hover:

            var variantValue = variant[13..];

            var match = appRunner.Library.CssPropertyNamesWithColons.GetLongestMatchingPrefix($"{variantValue}:")?.TrimEnd(':');

            if (string.IsNullOrEmpty(match))
                return false;

            notSupports = new VariantMetadata
            {
                PrefixType = "not-supports",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = $"not ({match}: initial)"
            };
        }

        return true;
    }
    
    public static bool TryVariantIsData(this string variant, out VariantMetadata? data)
    {
        data = null;

        if (variant.Contains("data-", StringComparison.Ordinal) == false)
            return false;

        if (variant.Contains("data-[", StringComparison.Ordinal))
        {
            // data-[size=large]:
            // not-data-[size=large]:

            var variantValue = variant[variant.IndexOf('[')..];

            if (string.IsNullOrEmpty(variantValue) || variantValue.StartsWith('[') == false || variantValue.EndsWith(']') == false)
                return false;

            data = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = variant.StartsWith("not-data-", StringComparison.Ordinal) ? $":not({variantValue.Replace('_', ' ')})" : variantValue.Replace('_', ' ')
            };
        }
        else
        {
            // data-active:
            // not-data-active:

            data = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = variant.StartsWith("not-data-", StringComparison.Ordinal) ? $":not([{variant.TrimStart("not-")}])" : $"[{variant}]"
            };
        }

        return true;
    }
    
    public static bool TryVariantIsCustom(this string variant, AppRunner appRunner, out VariantMetadata? custom)
    {
        custom = null;

        var bracketIndex = variant.IndexOf('[');
        var closingBracketIndex = variant.LastIndexOf(']');
        var variantValue = bracketIndex > -1 && closingBracketIndex > -1 ? variant[(bracketIndex + 1)..closingBracketIndex] : variant;
        
        if (variant.StartsWith("min-[", StringComparison.OrdinalIgnoreCase))
        {
            // min-[600px]:
            
            custom = new VariantMetadata
            {
                PrefixType = "wrapper",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = $"@media (width >= {variantValue.Replace('_', ' ')})"
            };

            return true;
        }

        if (variant.StartsWith("max-[", StringComparison.OrdinalIgnoreCase))
        {
            // max-[600px]:

            custom = new VariantMetadata
            {
                PrefixType = "wrapper",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = $"@media (width < {variantValue.Replace('_', ' ')})"
            };

            return true;
        }

        if (string.IsNullOrEmpty(variantValue) || variantValue.Length == variant.Length)
            return false;

        if (variantValue.StartsWith('@'))
        {
            // [@supports_(display:grid)]

            custom = new VariantMetadata
            {
                PrefixType = "wrapper",
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
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
}