using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class DecimalNegativeTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.DecimalNegative();

    actual.Should().BeInRange(decimal.MinValue, decimal.Zero);
  }
}
