using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.MarkovStringRandomizationSourceExtensionsTests;

public class GetMarkovProbabilityTests
{
  public GetMarkovProbabilityTests(ITestOutputHelper testOutputHelper)
  {
    TestOutputHelper = testOutputHelper;
  }

  private ITestOutputHelper TestOutputHelper { get; }

  [Fact]
  public void GivenSingleItemCollection_ReturnsExpectedResults()
  {
    var expected =
      new Dictionary<IReadOnlyList<char>, MarkovStringRandomizationSourceExtensions.MarkovResult<char>>
      {
        {
          new[] { 'h', 'e' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            FirstItemCount = 1, Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { 'l', 1 } }
          }
        },
        {
          new[] { 'e', 'l' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { 'l', 1 } }
          }
        },
        {
          new[] { 'l', 'l' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { 'o', 1 } }
          }
        },
        {
          new[] { 'l', 'o' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char> { Occurrences = 1, LastItemCount = 1 }
        }
      };

    var sources = new[] { "hello".ToCharArray() };
    var depth = 2;

    var actual = MarkovStringRandomizationSourceExtensions.GetMarkovProbability(sources, depth);

    var expectedSerializable = expected.ToDictionary(kvp => string.Concat(kvp.Key), kvp => kvp.Value);
    var actualSerializable = actual.ToDictionary(kvp => string.Concat(kvp.Key), kvp => kvp.Value);
    var expectedJson = JsonSerializer.Serialize(expectedSerializable);
    var actualJson = JsonSerializer.Serialize(actualSerializable);

    TestOutputHelper.WriteLine(message: $"Expected:{Environment.NewLine}{expectedJson}");
    TestOutputHelper.WriteLine(message: $"Actual:{Environment.NewLine}{actualJson}");

    Assert.Equal(expectedJson, actualJson);
  }


  [Fact]
  public void GivenTwoItemCollection_ReturnsExpectedResults()
  {
    var expected =
      new Dictionary<IReadOnlyList<char>, MarkovStringRandomizationSourceExtensions.MarkovResult<char>>
      {
        {
          new[] { '1', '2' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            FirstItemCount = 1, Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '3', 1 } }
          }
        },
        {
          new[] { '2', '3' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '4', 1 } }
          }
        },
        {
          new[] { '3', '4' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char> { Occurrences = 1, LastItemCount = 1 }
        },
        {
          new[] { '4', '3' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            FirstItemCount = 1, Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '2', 1 } }
          }
        },
        {
          new[] { '3', '2' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '1', 1 } }
          }
        },
        {
          new[] { '2', '1' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char> { Occurrences = 1, LastItemCount = 1 }
        }
      };

    var sources = new[] { "1234".ToCharArray(), "4321".ToCharArray() };
    var depth = 2;

    var actual = MarkovStringRandomizationSourceExtensions.GetMarkovProbability(sources, depth);

    var expectedSerializable = expected.ToDictionary(kvp => string.Concat(kvp.Key), kvp => kvp.Value);
    var actualSerializable = actual.ToDictionary(kvp => string.Concat(kvp.Key), kvp => kvp.Value);
    var expectedJson = JsonSerializer.Serialize(expectedSerializable);
    var actualJson = JsonSerializer.Serialize(actualSerializable);

    TestOutputHelper.WriteLine(message: $"Expected:{Environment.NewLine}{expectedJson}");
    TestOutputHelper.WriteLine(message: $"Actual:{Environment.NewLine}{actualJson}");

    Assert.Equal(expectedJson, actualJson);
  }


  [Fact]
  public void GivenThreeItemCollection_ReturnsExpectedResults()
  {
    var expected =
      new Dictionary<IReadOnlyList<char>, MarkovStringRandomizationSourceExtensions.MarkovResult<char>>
      {
        {
          new[] { '1', '2' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            FirstItemCount = 1, Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '3', 1 } }
          }
        },
        {
          new[] { '2', '3' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '4', 1 } }
          }
        },
        {
          new[] { '3', '4' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char> { Occurrences = 1, LastItemCount = 1 }
        },
        {
          new[] { '4', '3' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            FirstItemCount = 1, Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '2', 1 } }
          }
        },
        {
          new[] { '3', '2' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            Occurrences = 2, FirstItemCount = 1, NextItemCounts = new Dictionary<char, int> { { '1', 1 }, { '4', 1 } }
          }
        },
        {
          new[] { '2', '1' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char> { Occurrences = 1, LastItemCount = 1 }
        },
        {
          new[] { '2', '4' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '1', 1 } }
          }
        },
        {
          new[] { '4', '1' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char>
          {
            Occurrences = 1, NextItemCounts = new Dictionary<char, int> { { '3', 1 } }
          }
        },
        {
          new[] { '1', '3' },
          new MarkovStringRandomizationSourceExtensions.MarkovResult<char> { Occurrences = 1, LastItemCount = 1 }
        }
      };
    var sources = new[] { "1234".ToCharArray(), "4321".ToCharArray(), "32413".ToCharArray() };
    var depth = 2;

    var actual = MarkovStringRandomizationSourceExtensions.GetMarkovProbability(sources, depth);

    var expectedSerializable = expected.ToDictionary(kvp => string.Concat(kvp.Key), kvp => kvp.Value);
    var expectedJson = JsonSerializer.Serialize(expectedSerializable);
    TestOutputHelper.WriteLine(message: $"Expected:{Environment.NewLine}{expectedJson}");

    var actualSerializable = actual.ToDictionary(kvp => string.Concat(kvp.Key), kvp => kvp.Value);
    var actualJson = JsonSerializer.Serialize(actualSerializable);
    TestOutputHelper.WriteLine(message: $"Actual:{Environment.NewLine}{actualJson}");

    Assert.Equal(expectedJson, actualJson);
  }

  [Fact]
  public void GivenThreeItemObjectCollection_ReturnsExpectedResults()
  {
    var expected1 = new EquatableExample { Value = "1" };
    var expected2 = new EquatableExample { Value = "2" };
    var expected3 = new EquatableExample { Value = "3" };
    var expected4 = new EquatableExample { Value = "4" };
    var expected =
      new
        Dictionary<IReadOnlyList<EquatableExample>,
          MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>>
        {
          {
            new[] { expected1, expected2 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              FirstItemCount = 1,
              Occurrences = 1,
              NextItemCounts = new Dictionary<EquatableExample, int> { { expected3, 1 } }
            }
          },
          {
            new[] { expected2, expected3 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              Occurrences = 1, NextItemCounts = new Dictionary<EquatableExample, int> { { expected4, 1 } }
            }
          },
          {
            new[] { expected3, expected4 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              Occurrences = 1, LastItemCount = 1
            }
          },
          {
            new[] { expected4, expected3 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              FirstItemCount = 1,
              Occurrences = 1,
              NextItemCounts = new Dictionary<EquatableExample, int> { { expected2, 1 } }
            }
          },
          {
            new[] { expected3, expected2 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              Occurrences = 2,
              FirstItemCount = 1,
              NextItemCounts = new Dictionary<EquatableExample, int> { { expected1, 1 }, { expected4, 1 } }
            }
          },
          {
            new[] { expected2, expected1 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              Occurrences = 1, LastItemCount = 1
            }
          },
          {
            new[] { expected2, expected4 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              Occurrences = 1, NextItemCounts = new Dictionary<EquatableExample, int> { { expected1, 1 } }
            }
          },
          {
            new[] { expected4, expected1 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              Occurrences = 1, NextItemCounts = new Dictionary<EquatableExample, int> { { expected3, 1 } }
            }
          },
          {
            new[] { expected1, expected3 },
            new MarkovStringRandomizationSourceExtensions.MarkovResult<EquatableExample>
            {
              Occurrences = 1, LastItemCount = 1
            }
          }
        };
    var sources = new[]
    {
      new[]
      {
        new EquatableExample { Value = "1" }, new EquatableExample { Value = "2" },
        new EquatableExample { Value = "3" }, new EquatableExample { Value = "4" }
      },
      new[]
      {
        new EquatableExample { Value = "4" }, new EquatableExample { Value = "3" },
        new EquatableExample { Value = "2" }, new EquatableExample { Value = "1" }
      },
      new[]
      {
        new EquatableExample { Value = "3" }, new EquatableExample { Value = "2" },
        new EquatableExample { Value = "4" }, new EquatableExample { Value = "1" },
        new EquatableExample { Value = "3" }
      }
    };
    var depth = 2;

    var actual = MarkovStringRandomizationSourceExtensions.GetMarkovProbability(sources, depth);

    var expectedSerializable = expected.ToDictionary(kvp => JsonSerializer.Serialize(kvp.Key),
      kvp => new
      {
        kvp.Value.Occurrences,
        kvp.Value.LastItemCount,
        kvp.Value.FirstItemCount,
        NextItemCounts =
          kvp.Value.NextItemCounts.ToDictionary(kvp => JsonSerializer.Serialize(kvp.Key), kvp => kvp.Value)
      });
    var expectedJson = JsonSerializer.Serialize(expectedSerializable);
    TestOutputHelper.WriteLine(message: $"Expected:{Environment.NewLine}{expectedJson}");

    var actualSerializable = actual.ToDictionary(kvp => JsonSerializer.Serialize(kvp.Key),
      kvp => new
      {
        kvp.Value.Occurrences,
        kvp.Value.LastItemCount,
        kvp.Value.FirstItemCount,
        NextItemCounts =
          kvp.Value.NextItemCounts.ToDictionary(kvp => JsonSerializer.Serialize(kvp.Key), kvp => kvp.Value)
      });
    var actualJson = JsonSerializer.Serialize(actualSerializable);
    TestOutputHelper.WriteLine(message: $"Actual:{Environment.NewLine}{actualJson}");

    Assert.Equal(expectedJson, actualJson);
  }
}
