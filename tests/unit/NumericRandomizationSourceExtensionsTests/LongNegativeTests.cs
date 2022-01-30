using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class LongNegativeTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.LongNegative();

    Assert.True(condition: actual < 0, userMessage: $"Actual ({actual}) expected to be < 0");
  }
}
