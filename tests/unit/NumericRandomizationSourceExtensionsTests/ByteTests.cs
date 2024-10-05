using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class ByteTests
{
  [Fact]
  public void ReturnsSuccessfully()
  {
    var actual = Randomizer.Shared.Byte();

    actual.Should().BeInRange(byte.MinValue, byte.MaxValue);
  }
}
