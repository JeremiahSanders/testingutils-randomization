using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.HexRandomizationSourceExtensionsTests;

public class HexadecimalTests
{
  [Fact]
  public void ReturnsRequestedLength()
  {
    var length = Randomizer.Shared.IntInRange(5, 200);
    var value = Randomizer.Shared.Hexadecimal(length);

    value.Should().HaveLength(length);
  }

  [Fact]
  public void AllCharactersAreHex()
  {
    var length = Randomizer.Shared.IntInRange(5, 200);
    var value = Randomizer.Shared.Hexadecimal(length);

    value.ToCharArray().Should()
      .AllSatisfy(character => HexRandomizationSourceExtensions.HexChars.Should().Contain(character));
  }
}
