using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class LongInRangeTests
{
  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] ToCaseArrangement(long minInclusive, long maxExclusive)
    {
      return new object[] { minInclusive, maxExclusive };
    }

    return new[]
    {
      ToCaseArrangement(long.MinValue, long.MaxValue),
      ToCaseArrangement(long.MinValue, maxExclusive: long.MaxValue / 2),
      ToCaseArrangement(minInclusive: long.MaxValue / 2, long.MaxValue),
      ToCaseArrangement(minInclusive: long.MaxValue - 1, long.MaxValue)
    };
  }

  [Theory]
  [MemberData(memberName: nameof(CreateTestCases))]
  public void ReturnsValueInRange(long minInclusive, long maxExclusive)
  {
    var actual = Randomizer.Shared.LongInRange(minInclusive, maxExclusive);

    Assert.False(condition: actual < minInclusive, userMessage: $"Value was below {nameof(minInclusive)}");
    Assert.True(condition: actual < maxExclusive,
      userMessage: $"Value was equal to or greater than {nameof(maxExclusive)}"
    );
  }
}
