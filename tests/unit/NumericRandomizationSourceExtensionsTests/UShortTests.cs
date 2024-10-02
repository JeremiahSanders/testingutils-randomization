using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class UShortTests
{
  [Fact]
  public void CanGenerateUShortMaxValue()
  {
    IRandomizationSource fixedSource = new ArrangedRandomizationSource(
      getNextIntEnumerable: (minInclusive, maxExclusive) => Enumerable.Repeat(maxExclusive - 1, int.MaxValue));

    const ushort expected = ushort.MaxValue;

    var actual = fixedSource.UShort();

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void GivenSharedSource_GeneratesExpectedValues()
  {
    var actual = Randomizer.Shared.UShort();

    Assert.InRange(actual, ushort.MinValue, ushort.MaxValue);
  }
}
