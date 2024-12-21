namespace WordFinderDarioSalas
{
    public class WordFinder
    {
        private readonly char[,] _matrix;
        private readonly int _rows;
        private readonly int _cols;

        public WordFinder(IEnumerable<string> matrix)
        {
            // Initialize the matrix and its dimensions
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
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordSet = new HashSet<string>(wordstream);
            var foundWords = new HashSet<string>();     
            var wordCounts = new Dictionary<string, int>();

            foreach (var word in wordSet)
            {
                if (ExistsInMatrix(word))
                {
                    foundWords.Add(word);
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
            for (int i = 0; i < _rows; i++)
            {
                if (SearchHorizontally(i, word)) return true;
            }

            for (int j = 0; j < _cols; j++)
            {
                if (SearchVertically(j, word)) return true;
            }

            return false;
        }

        private bool SearchHorizontally(int row, string word)
        {
            var wordLength = word.Length;

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

            return false;
        }

        private bool SearchVertically(int col, string word)
        {
            var wordLength = word.Length;

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

            return false;
        }
    }
}
