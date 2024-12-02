using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class UShortTests
{
  [Fact]
  [SuppressMessage("ReSharper", "UnusedParameter.Local", Justification = "Included for clarity of intent.")]
  public void CanGenerateUShortMaxValue()
  {
    IRandomizationSource fixedSource = new ArrangedRandomizationSource(
      getNextIntEnumerable: (minInclusive, maxExclusive) => Enumerable.Repeat(maxExclusive - 1, int.MaxValue));

    const ushort expected = ushort.MaxValue;

    var actual = fixedSource.UShort();

    actual.Should().Be(expected);
  }

  [Fact]
  public void GivenSharedSource_GeneratesExpectedValues()
  {
    var actual = Randomizer.Shared.UShort();

    actual.Should().BeInRange(ushort.MinValue, ushort.MaxValue);
  }
}
