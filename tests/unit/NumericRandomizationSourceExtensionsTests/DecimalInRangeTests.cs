using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class DecimalInRangeTests
{
  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] ToCaseArrangement(decimal minInclusive, decimal maxExclusive)
    {
      return new object[] {minInclusive, maxExclusive};
    }

    return new[]
    {
      ToCaseArrangement(decimal.MinValue, decimal.MaxValue),
      ToCaseArrangement(decimal.MinValue, decimal.MaxValue / 2),
      ToCaseArrangement(decimal.MaxValue / 2, decimal.MaxValue),
      ToCaseArrangement(decimal.MaxValue - 1, decimal.MaxValue), ToCaseArrangement(-10M, 10M),
      ToCaseArrangement(100M, 10000M), ToCaseArrangement(-5000M, -250M)
    };
  }

  [Theory]
  [MemberData(nameof(CreateTestCases))]
  public void ReturnsValueInRange(decimal minInclusive, decimal maxExclusive)
  {
    var actual = Randomizer.Shared.DecimalInRange(minInclusive, maxExclusive);

    Assert.False(actual < minInclusive, $"Value was below {nameof(minInclusive)}. Actual: {actual}");
    Assert.True(actual < maxExclusive,
      $"Value was equal to or greater than {nameof(maxExclusive)}. Actual: {actual}"
    );
  }
}
