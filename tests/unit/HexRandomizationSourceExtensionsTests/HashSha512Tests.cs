using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.HexRandomizationSourceExtensionsTests;

public class HashSha512Tests
{
  [Fact]
  public void AllCharactersAreHex()
  {
    var value = Randomizer.Shared.HashSha512();

    value.ToCharArray().Should()
      .AllSatisfy(character => HexRandomizationSourceExtensions.HexChars.Should().Contain(character));
  }

  [Fact]
  public void ReturnsCorrectLength()
  {
    var value = Randomizer.Shared.HashSha512();

    value.Should().HaveLength(128);
  }
}
