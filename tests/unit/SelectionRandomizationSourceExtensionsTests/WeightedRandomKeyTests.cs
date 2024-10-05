using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.SelectionRandomizationSourceExtensionsTests;

public static class WeightedRandomKeyTests
{
  public enum TestEnum { Red, Green, Purple, Yellow }

  public class GivenDouble
  {
    private readonly Dictionary<TestEnum, double> _dictionary;
    private readonly RandomRandomizationSource _randomizer;

    public GivenDouble()
    {
      _randomizer = new RandomRandomizationSource(new Random());
      _dictionary = new Dictionary<TestEnum, double>
      {
        { TestEnum.Red, 1.0 }, { TestEnum.Purple, 4.0 }, { TestEnum.Green, 10.0 }, { TestEnum.Yellow, 30.0 }
      };
    }

    [Fact]
    public void GivenEmptyCollection_ThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => _randomizer.WeightedRandomKey(new Dictionary<TestEnum, double>()));
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _randomizer.WeightedRandomKey(_dictionary))
        .Aggregate(
          new Dictionary<TestEnum, int>
          {
            { TestEnum.Red, 0 }, { TestEnum.Purple, 0 }, { TestEnum.Green, 0 }, { TestEnum.Yellow, 0 }
          },
          (agg, value) =>
          {
            if (!agg.ContainsKey(value))
            {
              agg.Add(value, 0);
            }

            agg[value] += 1;
            return agg;
          });
      var sortedResults = results.OrderByDescending(kvp => kvp.Value)
        .Select(kvp => kvp.Key)
        .ToArray();

      sortedResults.Should().BeEquivalentTo(expected);
    }
  }

  public class GivenInt
  {
    private readonly Dictionary<TestEnum, int> _dictionary;
    private readonly RandomRandomizationSource _randomizer;

    public GivenInt()
    {
      _randomizer = new RandomRandomizationSource(new Random());
      _dictionary = new Dictionary<TestEnum, int>
      {
        { TestEnum.Red, 1 }, { TestEnum.Purple, 4 }, { TestEnum.Green, 10 }, { TestEnum.Yellow, 30 }
      };
    }

    [Fact]
    public void GivenEmptyCollection_ThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => _randomizer.WeightedRandomKey(new Dictionary<TestEnum, int>()));
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _randomizer.WeightedRandomKey(_dictionary))
        .Aggregate(
          new Dictionary<TestEnum, int>
          {
            { TestEnum.Red, 0 }, { TestEnum.Purple, 0 }, { TestEnum.Green, 0 }, { TestEnum.Yellow, 0 }
          },
          (agg, value) =>
          {
            if (!agg.ContainsKey(value))
            {
              agg.Add(value, 0);
            }

            agg[value] += 1;
            return agg;
          });
      var sortedResults = results.OrderByDescending(kvp => kvp.Value)
        .Select(kvp => kvp.Key)
        .ToArray();

      sortedResults.Should().BeEquivalentTo(expected);
    }
  }
}
