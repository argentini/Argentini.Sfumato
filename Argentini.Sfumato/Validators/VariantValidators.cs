namespace Argentini.Sfumato.Validators;

public static class VariantValidators
{
    #region Identify Variants

    public static bool TryVariantIsMediaQuery(this string variant, AppRunner appRunner, out VariantMetadata? cssMediaQuery)
    {
        cssMediaQuery = null;

        if (appRunner.Library.MediaQueryPrefixes.TryGetValue(variant, out cssMediaQuery))
            return true;

        return appRunner.Library.SupportsQueryPrefixes.TryGetValue(variant, out cssMediaQuery);
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

        return appRunner.Library.PseudoclassPrefixes.TryGetValue(variant.StartsWith('d') && variant.StartsWith("data-", StringComparison.OrdinalIgnoreCase) ? "data-" : variant, out pseudoClass);
    }

    public static bool TryVariantIsGroup(this string variant, AppRunner appRunner, out VariantMetadata? group)
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

            if (string.IsNullOrEmpty(variantValue) || TryVariantIsPseudoClass(variantValue, appRunner, out var pseudoClass) == false)
                return false;

            if (pseudoClass is null)
                return false;
            
            group = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.group{pseudoClass.SelectorSuffix}) *)"
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
        else
        {
            return false;
        }

        return true;
    }
    
    public static bool TryVariantIsPeer(this string variant, AppRunner appRunner, out VariantMetadata? peer)
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

            if (string.IsNullOrEmpty(variantValue) || TryVariantIsPseudoClass(variantValue, appRunner, out var pseudoClass) == false)
                return false;

            if (pseudoClass is null)
                return false;
            
            peer = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = $":is(:where(.peer{pseudoClass.SelectorSuffix}) ~ *)"
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
        else
        {
            return false;
        }

        return true;
    }

    public static bool TryVariantIsNth(this string variant, out VariantMetadata? nth)
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

    public static bool TryVariantIsHas(this string variant, AppRunner appRunner, out VariantMetadata? has)
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

            if (TryVariantIsPseudoClass(variantValue, appRunner, out var pseudoClass) == false)
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

    public static bool TryVariantIsSupports(this string variant, AppRunner appRunner, out VariantMetadata? supports)
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
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = $"({variantValue.Replace('_', ' ')})"
            };
        }
        else if (variant.StartsWith("supports-"))
        {
            // supports-hover:

            var variantValue = variant.TrimStart("supports-");

            if (string.IsNullOrEmpty(variantValue))
                return false;

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
        else
        {
            return false;
        }

        return true;
    }
    
    public static bool TryVariantIsNotSupports(this string variant, AppRunner appRunner, out VariantMetadata? notSupports)
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
                PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
                Statement = $"not ({variantValue.Replace('_', ' ')})"
            };
        }
        else if (variant.StartsWith("not-supports-"))
        {
            // not-supports-hover:

            var variantValue = variant.TrimStart("not-supports-");

            if (string.IsNullOrEmpty(variantValue))
                return false;

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
        else
        {
            return false;
        }

        return true;
    }
    
    public static bool TryVariantIsData(this string variant, out VariantMetadata? data)
    {
        data = null;

        if (variant.StartsWith("data-", StringComparison.Ordinal) == false && variant.StartsWith("not-data-", StringComparison.Ordinal) == false)
            return false;

        if (variant.Contains("data-["))
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
        else if (variant.Contains("data-"))
        {
            // data-active:
            // not-data-active:

            data = new VariantMetadata
            {
                PrefixType = "pseudoclass",
                SelectorSuffix = variant.StartsWith("not-data-", StringComparison.Ordinal) ? $":not([{variant.TrimStart("not-")}])" : $"[{variant}]"
            };
        }
        else
        {
            return false;
        }

        return true;
    }
    
    public static bool TryVariantIsCustom(this string variant, AppRunner appRunner, out VariantMetadata? custom)
    {
        custom = null;

        var variantValue = variant[(variant.IndexOf('[') + 1)..(variant.IndexOf(']'))];

        if (variant.StartsWith("min-[", StringComparison.OrdinalIgnoreCase))
        {
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
    
    #endregion
}