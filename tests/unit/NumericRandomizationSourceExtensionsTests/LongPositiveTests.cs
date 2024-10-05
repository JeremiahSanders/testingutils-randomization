using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class LongPositiveTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.LongPositive();

    actual.Should().BePositive();
    actual.Should().BeLessThan(long.MaxValue);
  }
}
