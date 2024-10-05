using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class DecimalPositiveTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.DecimalPositive();

    actual.Should().BeInRange(decimal.Zero, decimal.MaxValue);
  }
}
