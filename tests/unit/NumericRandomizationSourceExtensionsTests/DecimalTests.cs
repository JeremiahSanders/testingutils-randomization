using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class DecimalTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.Decimal();

    actual.Should().BeInRange(decimal.MinValue, decimal.MaxValue);
  }
}
