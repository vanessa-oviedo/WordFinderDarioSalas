using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using WordFinderDarioSalas;

namespace WordFinderTests
{
    public partial class WordFinderDarioSalasTest
    {
        [TestClass]
        public class WordFinderPerformanceTests
        {
            public TestContext TestContext { get; set; }

            [TestMethod]
            public void PerformanceTest_LargeMatrixAndWordStream()
            {
                // Arrange
                var random = new Random();
                const int matrixSize = 64;
                const int wordStreamSize = 1000;

                // Generate a random 64x64 matrix
                var matrix = new List<string>();
                for (int i = 0; i < matrixSize; i++)
                {
                    var row = new char[matrixSize];
                    for (int j = 0; j < matrixSize; j++)
                    {
                        row[j] = (char)('a' + random.Next(0, 26));
                    }
                    matrix.Add(new string(row));
                }

                // Generate a random word stream of 1000 words
                var wordStream = new List<string>();
                for (int i = 0; i < wordStreamSize; i++)
                {
                    var wordLength = random.Next(3, 8); // Random word length between 3 and 7
                    var word = new char[wordLength];
                    for (int j = 0; j < wordLength; j++)
                    {
                        word[j] = (char)('a' + random.Next(0, 26));
                    }
                    wordStream.Add(new string(word));
                }

                var wordFinder = new WordFinder(matrix);

                // Act
                var stopwatch = Stopwatch.StartNew();
                var result = wordFinder.Find(wordStream);
                stopwatch.Stop();

                // Log the elapsed time
                TestContext.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

                // Assert
                Assert.IsNotNull(result); // Ensure we get a result back
            }
        }
    }
}
