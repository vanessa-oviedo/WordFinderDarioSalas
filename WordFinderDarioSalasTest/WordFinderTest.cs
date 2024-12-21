using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordFinderDarioSalas;

namespace WordFinderTests
{
    [TestClass]
    public partial class WordFinderDarioSalasTest
    {
        [TestMethod]
        public void Find_ReturnsEmpty_WhenNoWordsAreFound()
        {
            // Arrange
            var matrix = new List<string>
            {
                "aaaa",
                "bbbb",
                "cccc",
                "dddd"
            };
            var wordStream = new List<string> { "word", "test", "matrix" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void Find_ReturnsWordsInMatrix()
        {
            // Arrange
            var matrix = new List<string>
            {
                "chill",
                "coldo",
                "windy",
                "heart",
                "storm"
            };
            var wordStream = new List<string> { "chill", "cold", "wind", "rain" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            result.Should().BeEquivalentTo(new[] { "chill", "cold", "wind" });
        }

        [TestMethod]
        public void Find_IgnoresDuplicateWordsInStream()
        {
            // Arrange
            var matrix = new List<string>
            {
                "chill",
                "coldo",
                "windy",
                "heart",
                "storm"
            };
            var wordStream = new List<string> { "chill", "chill", "wind", "wind", "cold", "cold" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            result.Should().BeEquivalentTo(new[] { "chill", "cold", "wind" });
        }

        [TestMethod]
        public void Find_ReturnsTop10Words()
        {
            // Arrange
            var matrix = new List<string>
            {
                "alpha",
                "bravo",
                "charl",
                "delta",
                "echoo"
            };
            var wordStream = new List<string>
            {
                "alpha", "bravo", "charl", "delta", "echoo",
                "foxtrot", "golf", "hotel", "india", "juliet",
                "kilo", "lima", "mike"
            };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            result.Should().BeEquivalentTo(new[]
            {
                "alpha", "bravo", "charl", "delta", "echoo"
            });
        }

        [TestMethod]
        public void Find_FindsWordsHorizontally()
        {
            // Arrange
            var matrix = new List<string>
            {
                "worda",
                "testb",
                "matrx"
            };
            var wordStream = new List<string> { "word", "test", "matrix" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            result.Should().BeEquivalentTo(new[] { "word", "test" });
        }

        [TestMethod]
        public void Find_FindsWordsVertically()
        {
            // Arrange
            var matrix = new List<string>
            {
                "cat",
                "dog",
                "bat"
            };
            var wordStream = new List<string> { "cd", "ob", "at" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            result.Should().BeEquivalentTo(new[] { "cd", "at" });
        }
    }
}
