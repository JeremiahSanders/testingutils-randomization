using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class IntPositiveTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.IntPositive();

    actual.Should().BePositive();
  }
}
