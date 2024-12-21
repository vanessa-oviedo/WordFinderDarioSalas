namespace WordFinderDarioSalas;

public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; } = [];

    public bool IsEndOfWord { get; set; }
}
