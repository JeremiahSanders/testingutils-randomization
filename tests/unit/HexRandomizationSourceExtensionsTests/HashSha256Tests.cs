using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.HexRandomizationSourceExtensionsTests;

public class HashSha256Tests
{
  [Fact]
  public void ReturnsCorrectLength()
  {
    var value = Randomizer.Shared.HashSha256();

    value.Should().HaveLength(64);
  }

  [Fact]
  public void AllCharactersAreHex()
  {
    var value = Randomizer.Shared.HashSha256();

    value.ToCharArray().Should()
      .AllSatisfy(character => HexRandomizationSourceExtensions.HexChars.Should().Contain(character));
  }
}
