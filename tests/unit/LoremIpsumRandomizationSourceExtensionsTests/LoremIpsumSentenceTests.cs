using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.LoremIpsumRandomizationSourceExtensionsTests;

public class LoremIpsumSentenceTests
{
  private readonly ITestOutputHelper _testOutputHelper;

  public LoremIpsumSentenceTests(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  [Fact]
  public void GeneratesSentencesWithExpectedWordCount()
  {
    const int sentenceCount = 50;

    var sentences = Enumerable.Range(1, sentenceCount).Select(_ =>
    {
      var maxLength = Randomizer.Shared.IntInRange(1, 20);
      var wordCount = Randomizer.Shared.IntInRange(1, 20);
      var Sentence = Randomizer.Shared.LoremIpsumSentence(sentenceParameters: (wordCount, maxLength));
      return (wordCount, maxLength, Sentence);
    }).ToList();

    _testOutputHelper.WriteLine(message: string.Join(Environment.NewLine,
      values: sentences.Select(tuple => $"{tuple.Sentence}")));

    foreach (var (wordCount, maxLength, sentence) in sentences)
    {
      var words = sentence.Split(separator: " ");

      Assert.Equal(wordCount, words.Length);
    }
  }

  [Theory]
  [InlineData(0, 1)]
  [InlineData(1, 0)]
  [InlineData(-1, 1)]
  [InlineData(1, -1)]
  [InlineData(1, int.MinValue)]
  [InlineData(int.MinValue, 1)]
  public void GivenLengthBelowOne_ThrowsArgumentOutOfRangeException(int wordCount, int maxWordLength)
  {
    Assert.Throws<ArgumentOutOfRangeException>(() =>
      Randomizer.Shared.LoremIpsumSentence(sentenceParameters: (wordCount, maxWordLength))
    );
  }
}
