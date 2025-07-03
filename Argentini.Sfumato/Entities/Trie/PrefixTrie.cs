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
        var lastValidIndex = -1;

        for (var i = 0; i < input.Length; i++)
        {
            // walk the trie
            if (node.Children.TryGetValue(input[i], out node) == false)
                break;

            if (node.IsWordEnd == false)
                continue;
            
            // candidate prefix: input[0‥i]
            var endsWithDash = input[i] == '-';
            var atEndOfInput = i == input.Length - 1;
            var nextIsDash = atEndOfInput == false && input[i + 1] == '-';

            // accept the candidate when it ends with '-' OR is followed by '-' OR is the whole input
            if (endsWithDash || nextIsDash || atEndOfInput)
                lastValidIndex = i;
        }

        return lastValidIndex >= 0 ? input[..(lastValidIndex + 1)] : null;
    }
    
    public List<string> GetAllWords()
    {
        var results = new List<string>();
        CollectWords(_root, "", results);
        return results;
    }

    private static void CollectWords(TrieNode node, string prefix, List<string> output)
    {
        if (node.IsWordEnd)
            output.Add(prefix);

        foreach ((var ch, TrieNode child) in node.Children)
            CollectWords(child, prefix + ch, output);
    }    
}