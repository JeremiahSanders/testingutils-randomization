using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.LoremIpsumRandomizationSourceExtensionsTests;

public class LoremIpsumWordTests
{
  private readonly ITestOutputHelper _testOutputHelper;

  public LoremIpsumWordTests(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  [Fact]
  public void GeneratesWordsInLatinLowercase()
  {
    const int wordCount = 50;
    var lowercaseRegex = new Regex("^([a-z])*$");

    var words = Enumerable.Range(1, wordCount).Select(_ =>
    {
      var maxLength = Randomizer.Shared.IntInRange(5, 20);
      var word = Randomizer.Shared.LoremIpsumWord(maxLength);
      return (maxLength, word);
    }).ToList();

    _testOutputHelper.WriteLine(string.Join(Environment.NewLine,
      words.Select(tuple => $"{tuple.word}")));

    foreach (var (_, word) in words)
    {
      Assert.Matches(lowercaseRegex, word);
    }
  }

  [Fact]
  public void GeneratesWordsWithinSpecifiedLimit()
  {
    const int wordCount = 50;

    var words = Enumerable.Range(1, wordCount).Select(_ =>
    {
      var maxLength = Randomizer.Shared.IntInRange(1, 20);
      var word = Randomizer.Shared.LoremIpsumWord(maxLength);
      return (maxLength, word);
    }).ToList();

    _testOutputHelper.WriteLine(string.Join(Environment.NewLine,
      words.Select(tuple => $"{tuple.word}")));

    foreach (var (maxLength, word) in words)
    {
      Assert.True(word.Length <= maxLength);
      Assert.False(string.IsNullOrWhiteSpace(word));
    }
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  [InlineData(int.MinValue)]
  public void GivenLengthBelowOne_ThrowsArgumentOutOfRangeException(int outOfRange)
  {
    Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.Shared.LoremIpsumWord(outOfRange));
  }
}
