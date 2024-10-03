using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.HexRandomizationSourceExtensionsTests;

public class HexadecimalTests
{
  [Fact]
  public void ReturnsRequestedLength()
  {
    var length = Randomizer.Shared.IntInRange(5, 200);
    var value = Randomizer.Shared.Hexadecimal(length);

    Assert.Equal(length, value.Length);
  }

  [Fact]
  public void AllCharactersAreHex()
  {
    var length = Randomizer.Shared.IntInRange(5, 200);
    var value = Randomizer.Shared.Hexadecimal(length);

    Assert.All(value.ToCharArray(), character => Assert.Contains(character, HexRandomizationSourceExtensions.HexChars));
  }
}
