using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.ReadOnlyListExtensionsTests;

public static class GetWeightedRandomItemTests
{
  public enum TestEnum { Red, Green, Purple, Yellow }

  public class GivenDoubleSpecifiedRandomizer
  {
    private readonly RandomRandomizationSource _randomizer;
    private readonly IReadOnlyList<(TestEnum, double)> _weightedItems;

    public GivenDoubleSpecifiedRandomizer()
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
      Assert.Throws<ArgumentException>(() => new List<(TestEnum, double)>().GetWeightedRandomItem(_randomizer));
    }

    [Fact]
    public void GivenNoWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        new List<(TestEnum, double)> { (TestEnum.Purple, 0d) }.GetWeightedRandomItem(_randomizer)
      );
    }

    [Fact]
    public void GivenExcessiveWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        new List<(TestEnum, double)> { (TestEnum.Purple, double.MaxValue), (TestEnum.Green, double.MaxValue) }
          .GetWeightedRandomItem(_randomizer)
      );
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _weightedItems.GetWeightedRandomItem(_randomizer))
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

  public class GivenIntSpecifiedRandomizer
  {
    private readonly RandomRandomizationSource _randomizer;
    private readonly IReadOnlyList<(TestEnum, int)> _weightedItems;

    public GivenIntSpecifiedRandomizer()
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
      Assert.Throws<ArgumentException>(() => new List<(TestEnum, int)>().GetWeightedRandomItem(_randomizer));
    }

    [Fact]
    public void GivenNoWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        new List<(TestEnum, int)> { (TestEnum.Purple, 0) }.GetWeightedRandomItem(_randomizer));
    }

    [Fact]
    public void GivenExcessiveWeight_ThrowsOverflowException()
    {
      Assert.Throws<OverflowException>(() =>
        new List<(TestEnum, int)> { (TestEnum.Purple, int.MaxValue), (TestEnum.Green, int.MaxValue) }
          .GetWeightedRandomItem(_randomizer));
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _weightedItems.GetWeightedRandomItem(_randomizer))
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

  public class GivenDoubleNoRandomizer
  {
    private readonly IReadOnlyList<(TestEnum, double)> _weightedItems;

    public GivenDoubleNoRandomizer()
    {
      _weightedItems = new List<(TestEnum, double)>
      {
        (TestEnum.Red, 1.0), (TestEnum.Purple, 4.0), (TestEnum.Green, 10.0), (TestEnum.Yellow, 30.0)
      };
    }

    [Fact]
    public void GivenEmptyCollection_ThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => new List<(TestEnum, double)>().GetWeightedRandomItem());
    }

    [Fact]
    public void GivenNoWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        new List<(TestEnum, double)> { (TestEnum.Purple, 0d) }.GetWeightedRandomItem()
      );
    }

    [Fact]
    public void GivenExcessiveWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        new List<(TestEnum, double)> { (TestEnum.Purple, double.MaxValue), (TestEnum.Green, double.MaxValue) }
          .GetWeightedRandomItem()
      );
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _weightedItems.GetWeightedRandomItem())
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

  public class GivenIntNoRandomizer
  {
    private readonly IReadOnlyList<(TestEnum, int)> _weightedItems;

    public GivenIntNoRandomizer()
    {
      _weightedItems = new List<(TestEnum, int)>
      {
        (TestEnum.Red, 1), (TestEnum.Purple, 4), (TestEnum.Green, 10), (TestEnum.Yellow, 30)
      };
    }

    [Fact]
    public void GivenEmptyCollection_ThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => new List<(TestEnum, int)>().GetWeightedRandomItem());
    }

    [Fact]
    public void GivenNoWeight_ThrowsArgumentOutOfRangeException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        new List<(TestEnum, int)> { (TestEnum.Purple, 0) }.GetWeightedRandomItem());
    }

    [Fact]
    public void GivenExcessiveWeight_ThrowsOverflowException()
    {
      Assert.Throws<OverflowException>(() =>
        new List<(TestEnum, int)> { (TestEnum.Purple, int.MaxValue), (TestEnum.Green, int.MaxValue) }
          .GetWeightedRandomItem());
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _weightedItems.GetWeightedRandomItem())
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
