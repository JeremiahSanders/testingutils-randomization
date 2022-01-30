using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class LongTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.Long();

    Assert.True(condition: actual < long.MaxValue, userMessage: $"Actual expected to be < {long.MaxValue}");
  }
}
