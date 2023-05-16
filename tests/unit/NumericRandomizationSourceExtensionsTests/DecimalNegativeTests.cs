using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class DecimalNegativeTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.DecimalNegative();

    Assert.InRange(actual, decimal.MinValue, decimal.Zero);
  }
}
