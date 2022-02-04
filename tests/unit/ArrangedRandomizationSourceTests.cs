using System;
using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit;

public class ArrangedRandomizationSourceTests
{
  public class GivenNoParameters
  {
    [Fact]
    public void CanCreate()
    {
      var _ = new ArrangedRandomizationSource();
    }

    [Fact]
    public void NextFloat_Throws()
    {
      Assert.Throws<InvalidOperationException>(() => new ArrangedRandomizationSource().NextFloat());
    }

    [Fact]
    public void NextDouble_Throws()
    {
      Assert.Throws<InvalidOperationException>(() => new ArrangedRandomizationSource().NextDouble());
    }

    [Fact]
    public void NextIntInRange_Throws()
    {
      Assert.Throws<InvalidOperationException>(() =>
        new ArrangedRandomizationSource().NextIntInRange(int.MinValue, int.MaxValue)
      );
    }

    [Fact]
    public void NextLongInRange_Throws()
    {
      Assert.Throws<InvalidOperationException>(() =>
        new ArrangedRandomizationSource().NextLongInRange(long.MinValue, long.MaxValue)
      );
    }
  }

  public class GivenParameters
  {
    private const float expectedFloat = 42f;
    private const double expectedDouble = 42d;
    private readonly ArrangedRandomizationSource _source;
    private int doubleDelegateExecutions;
    private int floatDelegateExecutions;
    private int intDelegateExecutions;
    private int longDelegateExecutions;

    public GivenParameters()
    {
      // ReSharper disable ArgumentsStyleNamedExpression
      _source = new ArrangedRandomizationSource(
        getNextDoubleEnumerable: DoubleDelegate,
        getNextFloatEnumerable: FloatDelegate,
        getNextIntEnumerable: IntDelegate,
        getNextLongEnumerable: LongDelegate
      );
      // ReSharper restore ArgumentsStyleNamedExpression
    }

    private IEnumerable<long> LongDelegate(long minInclusive, long maxExclusive)
    {
      longDelegateExecutions++;
      return new[] { Random.Shared.NextInt64(minInclusive, maxExclusive) };
    }

    private IEnumerable<int> IntDelegate(int minInclusive, int maxExclusive)
    {
      intDelegateExecutions++;
      return new[] { Random.Shared.Next(minInclusive, maxExclusive) };
    }

    private IEnumerable<float> FloatDelegate()
    {
      floatDelegateExecutions++;
      return new[] { expectedFloat };
    }

    private IEnumerable<double> DoubleDelegate()
    {
      doubleDelegateExecutions++;
      return new[] { expectedDouble };
    }

    [Fact]
    public void NextFloat_ExecutesDelegate()
    {
      var _ = _source.NextFloat();

      Assert.Equal(1, floatDelegateExecutions);
    }

    [Fact]
    public void NextFloat_ReturnsExpectedValue()
    {
      var actual = _source.NextFloat();

      Assert.Equal(expectedFloat, actual);
    }

    [Fact]
    public void NextDouble_ExecutesDelegate()
    {
      var _ = _source.NextDouble();

      Assert.Equal(1, doubleDelegateExecutions);
    }

    [Fact]
    public void NextDouble_ReturnsExpectedValue()
    {
      var actual = _source.NextDouble();

      Assert.Equal(expectedDouble, actual);
    }

    [Fact]
    public void NextIntInRange_ExecutesDelegate()
    {
      var _ = _source.NextIntInRange(int.MinValue, int.MaxValue);

      Assert.Equal(1, intDelegateExecutions);
    }

    [Fact]
    public void NextIntInRange_ReturnsValueInRange()
    {
      var minInclusive = Random.Shared.Next(-5000, -500);
      var maxExclusive = Random.Shared.Next(100, 10000);
      var actual = _source.NextIntInRange(minInclusive, maxExclusive);

      Assert.False(condition: actual < minInclusive, userMessage: $"Int value was below {minInclusive}");
      Assert.True(condition: actual < maxExclusive,
        userMessage: $"Int value was equal to or greater than {maxExclusive}");
    }

    [Fact]
    public void NextLongInRange_ExecutesDelegate()
    {
      var _ = _source.NextLongInRange(long.MinValue, long.MaxValue);

      Assert.Equal(1, longDelegateExecutions);
    }

    [Fact]
    public void NextLongInRange_ReturnsValueInRange()
    {
      var minInclusive = Random.Shared.NextInt64(-5000, -500);
      var maxExclusive = Random.Shared.NextInt64(100, 10000);
      var actual = _source.NextLongInRange(minInclusive, maxExclusive);

      Assert.False(condition: actual < minInclusive, userMessage: $"Long value was below {minInclusive}");
      Assert.True(condition: actual < maxExclusive,
        userMessage: $"Long value was equal to or greater than {maxExclusive}");
    }
  }
}
