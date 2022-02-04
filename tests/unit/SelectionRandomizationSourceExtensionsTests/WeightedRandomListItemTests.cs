using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.SelectionRandomizationSourceExtensionsTests;

public static class WeightedRandomListItemTests
{
  public enum TestEnum { Red, Green, Purple, Yellow }

  public class GivenDouble
  {
    private readonly RandomRandomizationSource _randomizer;
    private readonly IReadOnlyList<(TestEnum, double)> _weightedItems;

    public GivenDouble()
    {
      _randomizer = new RandomRandomizationSource(new Random());
      _weightedItems = new List<(TestEnum, double)>
      {
        (TestEnum.Red, 1.0), (TestEnum.Purple, 4.0), (TestEnum.Green, 10.0), (TestEnum.Yellow, 30.0)
      };
    }

    [Fact]
    public void GivenEmptyCollection_ThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => _randomizer.WeightedRandomListItem(new List<(TestEnum, double)>()));
    }

    [Fact]
    public void GivenNoWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        _randomizer.WeightedRandomListItem(new List<(TestEnum, double)> { (TestEnum.Purple, 0d) })
      );
    }

    [Fact]
    public void GivenExcessiveWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        _randomizer.WeightedRandomListItem(new List<(TestEnum, double)>
        {
          (TestEnum.Purple, double.MaxValue), (TestEnum.Green, double.MaxValue)
        })
      );
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _randomizer.WeightedRandomListItem(_weightedItems))
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

      Assert.Equal(expected, sortedResults);
    }
  }

  public class GivenInt
  {
    private readonly RandomRandomizationSource _randomizer;
    private readonly IReadOnlyList<(TestEnum, int)> _weightedItems;

    public GivenInt()
    {
      _randomizer = new RandomRandomizationSource(new Random());
      _weightedItems = new List<(TestEnum, int)>
      {
        (TestEnum.Red, 1), (TestEnum.Purple, 4), (TestEnum.Green, 10), (TestEnum.Yellow, 30)
      };
    }

    [Fact]
    public void GivenEmptyCollection_ThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => _randomizer.WeightedRandomListItem(new List<(TestEnum, int)>()));
    }

    [Fact]
    public void GivenNoWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        _randomizer.WeightedRandomListItem(new List<(TestEnum, int)> { (TestEnum.Purple, 0) }));
    }

    [Fact]
    public void GivenExcessiveWeight_ThrowsOverflowException()
    {
      Assert.Throws<OverflowException>(() =>
        _randomizer.WeightedRandomListItem(new List<(TestEnum, int)>
        {
          (TestEnum.Purple, int.MaxValue), (TestEnum.Green, int.MaxValue)
        }));
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _randomizer.WeightedRandomListItem(_weightedItems))
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

      Assert.Equal(expected, sortedResults);
    }
  }
}
