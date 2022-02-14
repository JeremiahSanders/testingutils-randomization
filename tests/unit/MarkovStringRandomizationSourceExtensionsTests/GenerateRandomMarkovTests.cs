using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.MarkovStringRandomizationSourceExtensionsTests;

public class GenerateRandomMarkovTests
{
  private readonly IReadOnlyList<string> _sourceWords;

  public GenerateRandomMarkovTests(ITestOutputHelper testOutputHelper)
  {
    TestOutputHelper = testOutputHelper;
    _sourceWords = SampleEnumerations.Fruit;
  }

  public ITestOutputHelper TestOutputHelper { get; }

  [Theory]
  [InlineData(1, 10)]
  [InlineData(2, 10)]
  [InlineData(3, 10)]
  [InlineData(4, 10)]
  public void GeneratesNonEmptyWords(int depth, int maxLength)
  {
    var word = Randomizer.Shared.GenerateRandomMarkov(_sourceWords, maxLength, depth);

    TestOutputHelper.WriteLine(message: $"Generated word: {word}");

    Assert.False(condition: string.IsNullOrWhiteSpace(word));
  }

  [Theory]
  [InlineData(1, 10)]
  [InlineData(2, 10)]
  [InlineData(3, 10)]
  [InlineData(4, 10)]
  public void FirstCharacterFromInputWords(int depth, int maxLength)
  {
    var firstChars = _sourceWords.Select(word => word.ToCharArray().First());

    var word = Randomizer.Shared.GenerateRandomMarkov(_sourceWords, maxLength, depth);
    var actual = word.ToCharArray().First();
    TestOutputHelper.WriteLine(message: $"Generated word: {word}");

    Assert.Contains(actual, firstChars);
  }

  [Theory]
  [InlineData(25, 1, 15)]
  [InlineData(25, 2, 15)]
  [InlineData(25, 3, 15)]
  [InlineData(25, 4, 15)]
  public void GeneratesWordsWithinMaxLength(int count, int depth, int maxLength)
  {
    var words = Enumerable.Range(0, count)
      .Select(_ => Randomizer.Shared.GenerateRandomMarkov(_sourceWords, maxLength, depth))
      .ToArray();

    TestOutputHelper.WriteLine(message: $"Generated words: {JsonSerializer.Serialize(words)}");

    Assert.All(words, word => Assert.True(condition: word.Length <= maxLength));
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  [InlineData(int.MinValue)]
  public void GivenChainLengthBelow1_ThrowsArgumentOutOfRangeException(int chainLength)
  {
    const int maxLength = 10;
    Assert.Throws<ArgumentOutOfRangeException>(() =>
      Randomizer.Shared.GenerateRandomMarkov(_sourceWords, maxLength, chainLength)
    );
  }

  [Fact]
  public void GivenEmptySources_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() =>
      Randomizer.Shared.GenerateRandomMarkov(sources: Array.Empty<string>())
    );
  }

  [Fact]
  public void GivenEmptySourceLists_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() =>
      Randomizer.Shared.GenerateRandomMarkov(sources: new[] { string.Empty })
    );
  }

  [Fact]
  public void GivenChainLengthOverSourceLengths_ThrowsArgumentException()
  {
    const int exceptionalLength = 15; // A length greater than any source.
    Assert.Throws<ArgumentException>(() =>
      Randomizer.Shared.GenerateRandomMarkov(sources: new[] { new[] { 1, 2, 3 } }, chainLength: exceptionalLength)
    );
  }
}
