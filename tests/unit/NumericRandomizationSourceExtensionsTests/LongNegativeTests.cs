using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class LongNegativeTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.LongNegative();

    actual.Should().BeNegative();
  }
}
