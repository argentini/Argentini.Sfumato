using System.Collections;

namespace Sfumato.Entities.Trie;

public sealed class PrefixTrie<TValue> : IEnumerable<KeyValuePair<string, TValue>>
{
    private TrieNode _root = new();
    private int _count;

    /// <summary>
    /// Number of key→value pairs currently in the trie.
    /// </summary>
    public int Count => _count;
    
    /// <summary>
    /// Gets or sets the TValue associated with <paramref name="key"/>.
    /// Setting always inserts or overwrites.
    /// </summary>
    public TValue this[string key]
    {
        get
        {
            if (TryGetValue(key, out var val))
                return val;

            throw new KeyNotFoundException($"The key '{key}' was not found in the trie.");
        }
        
        set => Insert(key, value);
    }
    
    /// <summary>
    /// Drops the entire old trie and replaces it with a fresh root.
    /// Absolutely O(1) (just a reference swap).
    /// </summary>
    public void Clear()
    {
        _root = new TrieNode();
        _count = 0;
    }

    /// <summary>
    /// Inserts a key and associates it with <paramref name="value"/>.
    /// If the key already existed, its value is overwritten.
    /// </summary>
    public void Insert(string key, TValue value)
    {
        var node = _root;
        
        foreach (var ch in key)
        {
            if (node.Children.TryGetValue(ch, out var next) == false)
            {
                next = new TrieNode();
                node.Children[ch] = next;
            }
            
            node = next;
        }
        
        if (node.IsWordEnd == false)
        {
            node.IsWordEnd = true;
            _count++;
        }
        
        node.Value = value;
    }

    public void Add(string key, TValue value)
    {
        Insert(key, value);
    }

    /// <summary>
    /// Add an entry unless it already exists.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryAdd(string key, TValue value)
    {
        if (TryGetValue(key, out _))
            return false;

        Insert(key, value);
        
        return true;
    }
    
    public void AddRange(IEnumerable<KeyValuePair<string, TValue>> range)
    {
        foreach (var (key, value) in range)
            Insert(key, value);
    }
    
    /// <summary>
    /// Return true if the key exists and returns its associated value.
    /// </summary>
    public bool TryGetValue(string key, out TValue value)
    {
        var node = _root;
        
        foreach (var ch in key)
        {
            if (node.Children.TryGetValue(ch, out node) == false)
            {
                value = default!;
                return false;
            }
        }

        if (node.IsWordEnd)
        {
            value = node.Value!;
            return true;
        }

        value = default!;

        return false;
    }

    /// <summary>
    /// Return true if the key exists.
    /// </summary>
    public bool ContainsKey(string key)
    {
        var node = _root;
        
        foreach (var ch in key)
        {
            if (node.Children.TryGetValue(ch, out node) == false)
                return false;
        }

        if (node.IsWordEnd)
            return true;

        return false;
    }

    /// <summary>
    /// Removes the given key (and its associated value) from the trie.
    /// Returns true if the key was present and removed; false otherwise.
    /// </summary>
    public bool Remove(string key)
    {
        if (string.IsNullOrEmpty(key))
            return false;

        // 1) Walk down, recording each node along the path
        var node = _root;
        var nodes = new List<TrieNode> { _root };
        
        foreach (var ch in key)
        {
            if (node.Children.TryGetValue(ch, out node) == false)
                return false;
            
            nodes.Add(node);
        }

        // 2) If it wasn't a terminal node, nothing to remove
        if (node.IsWordEnd == false)
            return false;

        // 3) Un-mark it and clear its value
        node.IsWordEnd = false;
        node.Value     = default!;
        _count--;

        // 4) Prune any leaf branches that are no longer needed
        //    (walk back up the path; stop as soon as a node is still needed)
        for (var i = nodes.Count - 1; i > 0; i--)
        {
            var curr   = nodes[i];
            var parent = nodes[i - 1];
            var ch     = key[i - 1];

            if (curr.Children.Count == 0 && !curr.IsWordEnd)
                parent.Children.Remove(ch);
            else
                break;
        }

        return true;
    }

    /// <summary>
    /// Unchanged: just tells you if any inserted word is a prefix of `input`.
    /// </summary>
    public bool HasPrefixIn(string input)
    {
        var node = _root;

        foreach (var ch in input)
        {
            if (node.IsWordEnd)
                return true;

            if (!node.Children.TryGetValue(ch, out node))
                return false;
        }

        return node.IsWordEnd;
    }

    /// <summary>
    /// Unchanged behavior, but this overload also hands back the stored object
    /// for the longest matching prefix.
    /// </summary>
    public bool TryGetLongestMatchingPrefix(string input, out string? prefix, out TValue? value)
    {
        prefix = null;
        value = default;
        
        var node = _root;
        var lastValidIndex = -1;
        TrieNode? lastValidNode = null;

        for (var i = 0; i < input.Length; i++)
        {
            if (node.Children.TryGetValue(input[i], out node) == false)
                break;

            if (!node.IsWordEnd)
                continue;

            var endsWithDash = input[i] == '-';
            var atEnd       = i == input.Length - 1;
            var nextIsDash  = !atEnd && input[i + 1] == '-';

            if (endsWithDash || nextIsDash || atEnd)
            {
                lastValidIndex = i;
                lastValidNode  = node;
            }
        }

        if (lastValidIndex >= 0 && lastValidNode is not null)
        {
            prefix = input[..(lastValidIndex + 1)];
            value  = lastValidNode.Value;
            
            return true;
        }

        return false;
    }

    /// <summary>
    /// Just like before, but returns (word, value) pairs.
    /// </summary>
    public List<(string Word, TValue Value)> GetAllWordsWithValues()
    {
        var output = new List<(string, TValue)>();
        CollectWords(_root, "", output);
        return output;
    }

    private static void CollectWords(TrieNode node, string current, List<(string, TValue)> output)
    {
        if (node.IsWordEnd)
            output.Add((current, node.Value!));

        foreach (var kv in node.Children)
            CollectWords(kv.Value, current + kv.Key, output);
    }
    
    /// <summary>
    /// Enumerate all (key, value) pairs in the trie.
    /// </summary>
    public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
    {
        return Traverse(_root, "").GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Recursively walks the trie, yielding each terminal node as a key/value pair.
    /// </summary>
    private IEnumerable<KeyValuePair<string, TValue>> Traverse(TrieNode node, string prefix)
    {
        if (node.IsWordEnd)
            yield return new KeyValuePair<string, TValue>(prefix, node.Value!);

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var (nextChar, child) in node.Children)
        {
            foreach (var pair in Traverse(child, prefix + nextChar))
                yield return pair;
        }
    }
    
    private sealed class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new();
        public bool IsWordEnd { get; set; }
        public TValue? Value { get; set; }
    }
}
