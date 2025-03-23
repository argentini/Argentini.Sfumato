namespace Argentini.Sfumato.Entities;

public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; } = new();
    public bool IsWordEnd { get; set; }
}