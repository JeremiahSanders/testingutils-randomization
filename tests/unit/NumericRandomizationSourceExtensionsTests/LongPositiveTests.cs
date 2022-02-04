using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class LongPositiveTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.LongPositive();

    Assert.True(condition: actual < long.MaxValue, userMessage: $"Actual expected to be < {long.MaxValue}");
    Assert.True(condition: actual >= 0, userMessage: $"Actual {actual} expected to be >=0");
  }
}
