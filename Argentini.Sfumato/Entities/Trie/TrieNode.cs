namespace Argentini.Sfumato.Entities.Trie;

public sealed class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; } = new();
    public bool IsWordEnd { get; set; }
}