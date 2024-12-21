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

    public void PrintTrieNodes()
    {
        PrintTrieNodes(_root, "");
    }

    private void PrintTrieNodes(TrieNode node, string prefix)
    {
        // If the current node is the end of a word, print the word
        if (node.IsEndOfWord)
        {
            Console.WriteLine(prefix);
        }

        // Recursively print all the child nodes
        foreach (var child in node.Children)
        {
            PrintTrieNodes(child.Value, prefix + child.Key);
        }
    }
}
