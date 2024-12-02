using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.HexRandomizationSourceExtensionsTests;

public class HashSha384Tests
{
  [Fact]
  public void ReturnsCorrectLength()
  {
    var value = Randomizer.Shared.HashSha384();

    value.Should().HaveLength(96);
  }

  [Fact]
  public void AllCharactersAreHex()
  {
    var value = Randomizer.Shared.HashSha384();

    value.ToCharArray().Should()
      .AllSatisfy(character => HexRandomizationSourceExtensions.HexChars.Should().Contain(character));
  }
}
