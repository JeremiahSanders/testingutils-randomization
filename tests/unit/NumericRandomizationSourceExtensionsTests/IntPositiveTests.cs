using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class IntPositiveTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.IntPositive();

    Assert.True(condition: actual >= 0, userMessage: $"Actual < 0 ({actual})");
  }
}
