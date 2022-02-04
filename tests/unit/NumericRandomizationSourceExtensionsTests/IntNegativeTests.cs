using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class IntNegativeTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.IntNegative();

    Assert.True(condition: actual < 0, userMessage: $"Actual >= 0 ({actual})");
  }
}
