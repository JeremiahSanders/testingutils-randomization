using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class ByteInRangeTests
{
  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] ToCaseArrangement(byte minInclusive, byte maxExclusive)
    {
      return new object[] {minInclusive, maxExclusive};
    }

    return new[]
    {
      ToCaseArrangement(byte.MinValue, byte.MaxValue), ToCaseArrangement(byte.MinValue, byte.MaxValue / 2),
      ToCaseArrangement(byte.MaxValue / 2, byte.MaxValue), ToCaseArrangement(byte.MaxValue - 1, byte.MaxValue)
    };
  }

  [Theory]
  [MemberData(nameof(CreateTestCases))]
  public void ReturnsValueInRange(byte minInclusive, byte maxExclusive)
  {
    var actual = Randomizer.Shared.ByteInRange(minInclusive, maxExclusive);

    actual.Should().BeGreaterThanOrEqualTo(minInclusive);
    actual.Should().BeLessThan(maxExclusive);
  }
}
