using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.HexRandomizationSourceExtensionsTests;

public class HashSha384Tests
{
  [Fact]
  public void ReturnsCorrectLength()
  {
    var value = Randomizer.Shared.HashSha384();

    Assert.Equal(96, value.Length);
  }

  [Fact]
  public void AllCharactersAreHex()
  {
    var value = Randomizer.Shared.HashSha384();

    Assert.All(value.ToCharArray(), character => Assert.Contains(character, HexRandomizationSourceExtensions.HexChars));
  }
}
