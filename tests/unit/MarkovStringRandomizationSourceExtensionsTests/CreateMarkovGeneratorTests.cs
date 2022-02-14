using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.MarkovStringRandomizationSourceExtensionsTests;

public class CreateMarkovGeneratorTests
{
  private readonly IReadOnlyCollection<IReadOnlyList<EquatableExample>> _sourceComplexObjects;
  private readonly IReadOnlyList<string> _sourceWords;

  public CreateMarkovGeneratorTests(ITestOutputHelper testOutputHelper)
  {
    TestOutputHelper = testOutputHelper;
    _sourceWords = SampleEnumerations.Fruit;
    _sourceComplexObjects = new[]
    {
      new[]
      {
        new EquatableExample { Value = "1" }, new EquatableExample { Value = "2" },
        new EquatableExample { Value = "3" }
      },
      new[]
      {
        new EquatableExample { Value = "1" }, new EquatableExample { Value = "2" },
        new EquatableExample { Value = "3" }, new EquatableExample { Value = "4" },
        new EquatableExample { Value = "5" }
      },
      new[]
      {
        new EquatableExample { Value = "1" }, new EquatableExample { Value = "2" },
        new EquatableExample { Value = "3" }, new EquatableExample { Value = "2" },
        new EquatableExample { Value = "1" }
      }
    };
  }

  public ITestOutputHelper TestOutputHelper { get; }

  [Theory]
  [InlineData(1, 10)]
  [InlineData(2, 10)]
  [InlineData(3, 10)]
  [InlineData(4, 10)]
  public void GivenStrings_GeneratesNonEmptyWords(int depth, int maxLength)
  {
    var generator = Randomizer.Shared.CreateMarkovGenerator(_sourceWords, depth);
    var word = generator(maxLength);

    TestOutputHelper.WriteLine(message: $"Generated word: {word}");

    Assert.False(condition: string.IsNullOrWhiteSpace(word));
  }

  [Theory]
  [InlineData(1, 10)]
  [InlineData(2, 10)]
  [InlineData(3, 10)]
  [InlineData(4, 10)]
  public void GivenStrings_FirstCharacterFromInputWords(int depth, int maxLength)
  {
    var firstChars = _sourceWords.Select(word => word.ToCharArray().First());
    var generator = Randomizer.Shared.CreateMarkovGenerator(_sourceWords, depth);

    var word = generator(maxLength);
    var actual = word.ToCharArray().First();
    TestOutputHelper.WriteLine(message: $"Generated word: {word}");

    Assert.Contains(actual, firstChars);
  }

  [Theory]
  [InlineData(25, 1, 15)]
  [InlineData(25, 2, 15)]
  [InlineData(25, 3, 15)]
  [InlineData(25, 4, 15)]
  public void GivenStrings_GeneratesWordsWithinMaxLength(int count, int depth, int maxLength)
  {
    var generator = Randomizer.Shared.CreateMarkovGenerator(_sourceWords, depth);
    var words = Enumerable.Range(0, count)
      .Select(_ => generator(maxLength))
      .ToArray();

    TestOutputHelper.WriteLine(message: $"Generated words: {JsonSerializer.Serialize(words)}");

    Assert.All(words, word => Assert.True(condition: word.Length <= maxLength));
  }

  [Theory]
  [InlineData(25, 1, 15)]
  [InlineData(25, 2, 15)]
  [InlineData(25, 3, 15)]
  [InlineData(25, 4, 15)]
  public void GivenObjects_GeneratesSequencesWithinMaxLength(int count, int depth, int maxLength)
  {
    var generator = Randomizer.Shared.CreateMarkovGenerator(_sourceComplexObjects, depth);
    var words = Enumerable.Range(0, count)
      .Select(_ => generator(maxLength))
      .ToArray();

    TestOutputHelper.WriteLine(message: $"Generated words: {JsonSerializer.Serialize(words)}");

    Assert.All(words, word => Assert.True(condition: word.Count <= maxLength));
  }
}
