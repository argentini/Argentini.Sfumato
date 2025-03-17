namespace Argentini.Sfumato.Entities.Utilities;

public class BaseUtility
{
    public virtual string Prefix { get; set; } = string.Empty;
    
    /// <summary>
    /// Is a simple utility with static properties (e.g. "antialiased");
    /// is used as-is and doesn't have any custom properties.
    /// </summary>
    public virtual bool IsSimpleUtility { get; set; }

    public List<string> UsesCssCustomProperties { get; set; } = [];
    public int SelectorSort { get; protected set; }
    
    
    
    
    
    
}