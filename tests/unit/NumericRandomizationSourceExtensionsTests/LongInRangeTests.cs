using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class LongInRangeTests
{
  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] ToCaseArrangement(long minInclusive, long maxExclusive)
    {
      return new object[] {minInclusive, maxExclusive};
    }

    return new[]
    {
      ToCaseArrangement(long.MinValue, long.MaxValue), ToCaseArrangement(long.MinValue, long.MaxValue / 2),
      ToCaseArrangement(long.MaxValue / 2, long.MaxValue), ToCaseArrangement(long.MaxValue - 1, long.MaxValue)
    };
  }

  [Theory]
  [MemberData(nameof(CreateTestCases))]
  public void ReturnsValueInRange(long minInclusive, long maxExclusive)
  {
    var actual = Randomizer.Shared.LongInRange(minInclusive, maxExclusive);

    actual.Should().BeGreaterOrEqualTo(minInclusive);
    actual.Should().BeLessThan(maxExclusive);
  }
}
