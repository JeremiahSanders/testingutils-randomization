using System;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.DemographicsRandomizationSourceExtensionsTests;

public class DemographicsForenameUsaTests
{
  private readonly ITestOutputHelper _testOutputHelper;

  public DemographicsForenameUsaTests(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  [Fact]
  public void GeneratesNamesWithinSpecifiedLimit()
  {
    const int wordCount = 50;

    var words = Enumerable.Range(1, wordCount).Select(_ =>
    {
      var maxLength = Randomizer.Shared.IntInRange(1, 30);
      var word = Randomizer.Shared.DemographicsForenameUsa(maxLength);
      return (maxLength, word);
    }).ToList();

    _testOutputHelper.WriteLine(message: string.Join(Environment.NewLine,
      values: words.Select(tuple => $"{tuple.word}")));

    foreach (var (maxLength, word) in words)
    {
      word.Should().NotBeNullOrWhiteSpace();
      word.Length.Should().BeLessThanOrEqualTo(maxLength);
    }
  }

  [Fact]
  public void GeneratesNamesInLatinLowercaseWithFirstCharacterCapitalized()
  {
    const int wordCount = 50;
    var lowercaseRegex = new Regex(pattern: "^([A-Z]){1}([a-z])*$");

    var names = Enumerable.Range(1, wordCount).Select(_ =>
    {
      var maxLength = Randomizer.Shared.IntInRange(3, 30);
      var name = Randomizer.Shared.DemographicsForenameUsa(maxLength);
      return (maxLength, word: name);
    }).ToList();

    _testOutputHelper.WriteLine(message: string.Join(Environment.NewLine,
      values: names.Select(tuple => $"{tuple.word}")));

    foreach (var (_, word) in names)
    {
      Assert.Matches(lowercaseRegex, word);
    }
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  [InlineData(int.MinValue)]
  public void GivenLengthBelowOne_ThrowsArgumentOutOfRangeException(int outOfRange)
  {
    Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.Shared.DemographicsForenameUsa(outOfRange));
  }
}
