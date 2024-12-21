namespace WordFinderDarioSalas;

public class WordFinder
{
    private readonly char[,] _matrix;
    private readonly int _rows;
    private readonly int _cols;
    private readonly Trie _trie = new();

    public WordFinder(IEnumerable<string> matrix)
    {
        var matrixList = matrix.ToList();
        _rows = matrixList.Count;
        _cols = matrixList.FirstOrDefault()?.Length ?? 0;

        _matrix = new char[_rows, _cols];
        for (int i = 0; i < _rows; i++)
        {
            if (matrixList[i].Length != _cols)
                throw new ArgumentException("All rows must have the same number of characters.");

            for (int j = 0; j < _cols; j++)
            {
                _matrix[i, j] = matrixList[i][j];
            }
        }

        PopulateTrieWithMatrixWords();
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var wordCounts = new Dictionary<string, int>();

        foreach (var word in wordstream)
        {
            if (_trie.Search(word) && ExistsInMatrix(word))
            {
                wordCounts[word] = wordCounts.GetValueOrDefault(word, 0) + 1;
            }
        }

        return wordCounts
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .Take(10)
            .Select(kvp => kvp.Key);
    }

    private bool ExistsInMatrix(string word)
    {
        return SearchHorizontally(word) || SearchVertically(word);
    }

    private bool SearchHorizontally(string word)
    {
        var wordLength = word.Length;

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col <= _cols - wordLength; col++)
            {
                bool found = true;
                for (int k = 0; k < wordLength; k++)
                {
                    if (_matrix[row, col + k] != word[k])
                    {
                        found = false;
                        break;
                    }
                }

                if (found) return true;
            }
        }

        return false;
    }

    private bool SearchVertically(string word)
    {
        var wordLength = word.Length;

        for (int col = 0; col < _cols; col++)
        {
            for (int row = 0; row <= _rows - wordLength; row++)
            {
                bool found = true;
                for (int k = 0; k < wordLength; k++)
                {
                    if (_matrix[row + k, col] != word[k])
                    {
                        found = false;
                        break;
                    }
                }

                if (found) return true;
            }
        }

        return false;
    }

    private void PopulateTrieWithMatrixWords()
    {
        for (int i = 0; i < _rows; i++)
        {
            // Horizontal
            for (int j = 0; j < _cols; j++)
            {
                string word = "";
                for (int k = j; k < _cols; k++)
                {
                    word += _matrix[i, k];
                    _trie.Insert(word); // Insert on Trie
                }
            }
        }

        for (int j = 0; j < _cols; j++)
        {
            // Vertical
            for (int i = 0; i < _rows; i++)
            {
                string word = "";
                for (int k = i; k < _rows; k++)
                {
                    word += _matrix[k, j];
                    _trie.Insert(word); // Insert on Trie
                }
            }
        }
    }
}
