using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.HexRandomizationSourceExtensionsTests;

public class HashSha256Tests
{
  [Fact]
  public void ReturnsCorrectLength()
  {
    var value = Randomizer.Shared.HashSha256();

    Assert.Equal(64, value.Length);
  }

  [Fact]
  public void AllCharactersAreHex()
  {
    var value = Randomizer.Shared.HashSha256();

    Assert.All(value.ToCharArray(), character => Assert.Contains(character, HexRandomizationSourceExtensions.HexChars));
  }
}
