namespace WordFinderDarioSalas
{
    public class WordFinder
    {
        private readonly char[,] _matrix;
        private readonly int _rows;
        private readonly int _cols;

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
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            // Contador para contar las repeticiones de cada palabra
            var wordCounts = new Dictionary<string, int>();

            foreach (var word in wordstream)
            {
                // Verifica si la palabra existe en la matriz
                if (ExistsInMatrix(word))
                {
                    // Si la palabra ya está en el diccionario, aumenta el contador
                    if (!wordCounts.ContainsKey(word))
                    {
                        wordCounts[word] = 0;
                    }

                    wordCounts[word]++;
                }
            }

            // Devuelve las 10 palabras más repetidas, ordenadas por cantidad de repeticiones y alfabéticamente
            return wordCounts
                .OrderByDescending(kvp => kvp.Value)   // Ordenar por cantidad de repeticiones
                .ThenBy(kvp => kvp.Key)                // Ordenar alfabéticamente en caso de empate
                .Take(10)                              // Toma solo las 10 más repetidas
                .Select(kvp => kvp.Key);
        }

        private bool ExistsInMatrix(string word)
        {
            // Recorre las filas horizontalmente
            for (int i = 0; i < _rows; i++)
            {
                if (SearchHorizontally(i, word)) return true;
            }

            // Recorre las columnas verticalmente
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
