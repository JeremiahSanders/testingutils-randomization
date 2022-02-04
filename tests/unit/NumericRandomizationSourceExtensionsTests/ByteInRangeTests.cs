using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class ByteInRangeTests
{
  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] ToCaseArrangement(byte minInclusive, byte maxExclusive)
    {
      return new object[] { minInclusive, maxExclusive };
    }

    return new[]
    {
      ToCaseArrangement(byte.MinValue, byte.MaxValue),
      ToCaseArrangement(byte.MinValue, maxExclusive: byte.MaxValue / 2),
      ToCaseArrangement(minInclusive: byte.MaxValue / 2, byte.MaxValue),
      ToCaseArrangement(minInclusive: byte.MaxValue - 1, byte.MaxValue)
    };
  }

  [Theory]
  [MemberData(memberName: nameof(CreateTestCases))]
  public void ReturnsValueInRange(byte minInclusive, byte maxExclusive)
  {
    var actual = Randomizer.Shared.ByteInRange(minInclusive, maxExclusive);

    Assert.False(condition: actual < minInclusive, userMessage: $"Value was below {nameof(minInclusive)}");
    Assert.True(condition: actual < maxExclusive,
      userMessage: $"Value was equal to or greater than {nameof(maxExclusive)}"
    );
  }
}
