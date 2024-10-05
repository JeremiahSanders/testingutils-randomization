using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class IntInRangeTests
{
  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] ToCaseArrangement(int minInclusive, int maxExclusive)
    {
      return new object[] {minInclusive, maxExclusive};
    }

    return new[]
    {
      ToCaseArrangement(int.MinValue, int.MaxValue), ToCaseArrangement(int.MinValue, int.MaxValue / 2),
      ToCaseArrangement(int.MaxValue / 2, int.MaxValue), ToCaseArrangement(int.MaxValue - 1, int.MaxValue)
    };
  }

  [Theory]
  [MemberData(nameof(CreateTestCases))]
  public void ReturnsValueInRange(int minInclusive, int maxExclusive)
  {
    var actual = Randomizer.Shared.IntInRange(minInclusive, maxExclusive);

    actual.Should().BeGreaterOrEqualTo(minInclusive);
    actual.Should().BeLessThan(maxExclusive);
  }
}
