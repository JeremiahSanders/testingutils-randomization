using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class DecimalTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.Decimal();

    Assert.InRange(actual, decimal.MinValue, decimal.MaxValue);
  }
}
