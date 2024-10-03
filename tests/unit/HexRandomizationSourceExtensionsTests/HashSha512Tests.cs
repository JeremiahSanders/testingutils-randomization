using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.HexRandomizationSourceExtensionsTests;

public class HashSha512Tests
{
  [Fact]
  public void ReturnsCorrectLength()
  {
    var value = Randomizer.Shared.HashSha512();

    Assert.Equal(128, value.Length);
  }

  [Fact]
  public void AllCharactersAreHex()
  {
    var value = Randomizer.Shared.HashSha512();

    Assert.All(value.ToCharArray(), character => Assert.Contains(character, HexRandomizationSourceExtensions.HexChars));
  }
}
