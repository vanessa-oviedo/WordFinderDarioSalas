namespace WordFinderDarioSalas;

public class Trie
{
    private readonly TrieNode _root = new TrieNode();

    public void Insert(string word)
    {
        var currentNode = _root;

        foreach (var ch in word)
        {
            if (!currentNode.Children.TryGetValue(ch, out TrieNode? value))
            {
                value = new TrieNode();
                currentNode.Children[ch] = value;
            }
            currentNode = value;
        }

        currentNode.IsEndOfWord = true;
    }

    public bool Search(string word)
    {
        var currentNode = _root;

        foreach (var ch in word)
        {
            if (!currentNode.Children.TryGetValue(ch, out TrieNode? value))
            {
                return false;
            }
            currentNode = value;
        }

        return currentNode.IsEndOfWord;
    }
}
