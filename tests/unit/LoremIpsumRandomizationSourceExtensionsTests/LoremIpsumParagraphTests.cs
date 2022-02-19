using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.LoremIpsumRandomizationSourceExtensionsTests;

public class LoremIpsumParagraphTests
{
  private readonly ITestOutputHelper _testOutputHelper;

  public LoremIpsumParagraphTests(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  [Fact]
  public void GeneratesParagraphsWithExpectedWordCount()
  {
    const int paragraphCount = 50;

    var paragraphs = Enumerable.Range(1, paragraphCount).Select(_ =>
    {
      var maxLength = Randomizer.Shared.IntInRange(1, 20);
      var wordCount = Randomizer.Shared.IntInRange(1, 40);
      var paragraph =
        Randomizer.Shared.LoremIpsumParagraph(paragraphParameters: (wordCount, maxLength));
      return (wordCount, maxLength, paragraph);
    }).ToList();

    _testOutputHelper.WriteLine(message: string.Join(Environment.NewLine,
      values: paragraphs.Select(tuple => $"{tuple.paragraph}")));

    foreach (var (wordCount, maxLength, paragraph) in paragraphs)
    {
      var words = paragraph.Split(separator: " ").Where(word => !string.IsNullOrWhiteSpace(word));

      Assert.Equal(wordCount, actual: words.Count());
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
      Randomizer.Shared.LoremIpsumParagraph(paragraphParameters: (wordCount, maxWordLength))
    );
  }
}
