using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class IntInRangeTests
{
  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] ToCaseArrangement(int minInclusive, int maxExclusive)
    {
      return new object[] { minInclusive, maxExclusive };
    }

    return new[]
    {
      ToCaseArrangement(int.MinValue, int.MaxValue), ToCaseArrangement(int.MinValue, maxExclusive: int.MaxValue / 2),
      ToCaseArrangement(minInclusive: int.MaxValue / 2, int.MaxValue),
      ToCaseArrangement(minInclusive: int.MaxValue - 1, int.MaxValue)
    };
  }

  [Theory]
  [MemberData(memberName: nameof(CreateTestCases))]
  public void ReturnsValueInRange(int minInclusive, int maxExclusive)
  {
    var actual = Randomizer.Shared.IntInRange(minInclusive, maxExclusive);

    Assert.False(condition: actual < minInclusive, userMessage: $"Value was below {nameof(minInclusive)}");
    Assert.True(condition: actual < maxExclusive,
      userMessage: $"Value was equal to or greater than {nameof(maxExclusive)}"
    );
  }
}
