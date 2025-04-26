namespace Argentini.Sfumato.Entities.Trie;

public sealed class PrefixTrie
{
    private readonly TrieNode _root = new();

    public void Insert(string word)
    {
        var node = _root;
        
        foreach (var ch in word)
        {
            if (node.Children.ContainsKey(ch) == false)
                node.Children[ch] = new TrieNode();
            
            node = node.Children[ch];
        }
        
        node.IsWordEnd = true;
    }

    public bool HasPrefixIn(string input)
    {
        var node = _root;
    
        foreach (var ch in input)
        {
            if (node.IsWordEnd)
                return true;

            if (node.Children.TryGetValue(ch, out node) == false)
                return false;
        }

        return node.IsWordEnd;
    }
    
    public string? GetLongestMatchingPrefix(string input)
    {
        var node = _root;
        var longestMatch = -1;

        for (var i = 0; i < input.Length; i++)
        {
            if (node.Children.TryGetValue(input[i], out node) == false)
                break;

            if (node.IsWordEnd)
                longestMatch = i;
        }

        return longestMatch >= 0 ? input[..(longestMatch + 1)] : null;
    }
}