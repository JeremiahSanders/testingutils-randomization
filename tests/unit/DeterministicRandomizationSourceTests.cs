using System;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit;

public class DeterministicRandomizationSourceTests
{
  [Fact]
  public void NextDouble_ReturnsExpectedValues()
  {
    var doubles = new[] { 42.0d, double.MinValue, double.MaxValue, 0.0d };
    var source = new DeterministicRandomizationSource(doubles);

    foreach (var expected in doubles)
    {
      var actual = source.NextDouble();
      Assert.Equal(expected, actual);
    }
  }

  [Fact]
  public void NextDouble_ThrowsWhenEnumerationEnds()
  {
    var doubles = new[] { 42.0d, double.MinValue, double.MaxValue, 0.0d };
    var source = new DeterministicRandomizationSource(doubles);

    foreach (var _ in doubles)
    {
      var __ = source.NextDouble();
    }

    Assert.Throws<InvalidOperationException>(() => source.NextDouble());
  }

  [Fact]
  public void NextFloat_ReturnsExpectedValues()
  {
    var floats = new[] { 42.0f, float.MinValue, float.MaxValue, 0.0f };
    var source = new DeterministicRandomizationSource(floats: floats);

    foreach (var expected in floats)
    {
      var actual = source.NextFloat();
      Assert.Equal(expected, actual);
    }
  }

  [Fact]
  public void NextFloat_ThrowsWhenEnumerationEnds()
  {
    var floats = new[] { 42.0f, float.MinValue, float.MaxValue, 0.0f };
    var source = new DeterministicRandomizationSource(floats: floats);

    foreach (var _ in floats)
    {
      var __ = source.Float();
    }

    Assert.Throws<InvalidOperationException>(() => source.Float());
  }

  [Fact]
  public void NextIntInRange_GivenValuesInRange_ReturnsExpectedValues()
  {
    const int minInclusive = 100;
    const int maxExclusive = 10000;

    var integers = new[] { -5000, 0, -500, 250, -250, 8000 };
    var source = new DeterministicRandomizationSource(ints: integers);

    foreach (var expected in integers.Where(value => value is >= minInclusive and < maxExclusive))
    {
      var actual = source.NextIntInRange(minInclusive, maxExclusive);
      Assert.Equal(expected, actual);
    }
  }

  [Fact]
  public void NextIntInRange_GivenNoValuesInRange_Throws()
  {
    const int minInclusive = 10000;
    const int maxExclusive = int.MaxValue;

    var integers = new[] { -5000, 0, -500, 250, -250, 8000 };
    var source = new DeterministicRandomizationSource(ints: integers);

    Assert.Throws<InvalidOperationException>(() => source.NextIntInRange(minInclusive, maxExclusive));
  }


  [Fact]
  public void NextLongInRange_GivenValuesInRange_ReturnsExpectedValues()
  {
    const long minInclusive = 5L;
    const long maxExclusive = 50000L;

    var longs = new[] { -5000L, 0L, -500L, 250L, -250L, 8000L, int.MaxValue - 5L };
    var source = new DeterministicRandomizationSource(longs: longs);

    foreach (var expected in longs.Where(value => value is >= minInclusive and < maxExclusive))
    {
      var actual = source.NextLongInRange(minInclusive, maxExclusive);
      Assert.Equal(expected, actual);
    }
  }

  [Fact]
  public void NextLongInRange_GivenNoValuesInRange_Throws()
  {
    const long minInclusive = int.MaxValue;
    const long maxExclusive = int.MaxValue + 500L;

    var longs = new[] { -5000L, 0L, -500L, 250L, -250L, 8000L, int.MaxValue - 5L };
    var source = new DeterministicRandomizationSource(longs: longs);

    Assert.Throws<InvalidOperationException>(() => source.NextLongInRange(minInclusive, maxExclusive));
  }
}
