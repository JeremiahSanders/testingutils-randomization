using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.ReadOnlyDictionaryExtensionsTests;

public static class GetWeightedRandomKeyTests
{
  public enum TestEnum { Red, Green, Purple, Yellow }

  public class GivenDoubleSpecifiedRandomizer
  {
    private readonly Dictionary<TestEnum, double> _dictionary;
    private readonly RandomRandomizationSource _randomizer;

    public GivenDoubleSpecifiedRandomizer()
    {
      _randomizer = new RandomRandomizationSource(new Random());
      _dictionary = new Dictionary<TestEnum, double>
      {
        { TestEnum.Red, 1.0 }, { TestEnum.Purple, 4.0 }, { TestEnum.Green, 10.0 }, { TestEnum.Yellow, 30.0 }
      };
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _dictionary.GetWeightedRandomKey(_randomizer))
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
    private readonly Dictionary<TestEnum, double> _dictionary;

    public GivenDoubleNoRandomizer()
    {
      _dictionary = new Dictionary<TestEnum, double>
      {
        { TestEnum.Red, 1.0 }, { TestEnum.Purple, 4.0 }, { TestEnum.Green, 10.0 }, { TestEnum.Yellow, 30.0 }
      };
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _dictionary.GetWeightedRandomKey())
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
    private readonly Dictionary<TestEnum, int> _dictionary;

    public GivenIntNoRandomizer()
    {
      _dictionary = new Dictionary<TestEnum, int>
      {
        { TestEnum.Red, 1 }, { TestEnum.Purple, 4 }, { TestEnum.Green, 10 }, { TestEnum.Yellow, 30 }
      };
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _dictionary.GetWeightedRandomKey())
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
    private readonly Dictionary<TestEnum, int> _dictionary;
    private readonly RandomRandomizationSource _randomizer;

    public GivenIntSpecifiedRandomizer()
    {
      _randomizer = new RandomRandomizationSource(new Random());
      _dictionary = new Dictionary<TestEnum, int>
      {
        { TestEnum.Red, 1 }, { TestEnum.Purple, 4 }, { TestEnum.Green, 10 }, { TestEnum.Yellow, 30 }
      };
    }

    [Fact]
    public void Given1000Executions_ReturnsExpectedDistribution()
    {
      var expected = new[] { TestEnum.Yellow, TestEnum.Green, TestEnum.Purple, TestEnum.Red };

      var results = Enumerable.Range(1, 1000)
        .Select(_ => _dictionary.GetWeightedRandomKey(_randomizer))
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
