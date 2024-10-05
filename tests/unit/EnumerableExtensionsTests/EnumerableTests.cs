using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.EnumerableExtensionsTests;

public class EnumerableTests
{
  [Theory]
  [InlineData(-1, 10)]
  [InlineData(0, -1)]
  [InlineData(5, 2)]
  public void GivenInvalidRange_ThrowsException(int min, int max)
  {
    Randomizer.Shared.Invoking(r => r.Enumerable(Generate, min, max)).Should().Throw<ArgumentOutOfRangeException>();
  }

  [Fact]
  public void GivenRange1_ReturnsOneItem()
  {
    Randomizer.Shared.Enumerable(GenerateFromIndex, 1, 2).Should().HaveCount(1);
  }

  [Fact]
  public void ReturnsCountInRange()
  {
    var min = 3;
    var max = 7;
    var exclusiveMax = max + 1;
    Randomizer.Shared.Enumerable(Guid.NewGuid, 3, 7);
    var actualCount = Randomizer.Shared.Enumerable(GenerateFromIndex, min, exclusiveMax).Count();

    actualCount.Should().BeGreaterOrEqualTo(min);
    actualCount.Should().BeLessThanOrEqualTo(max);
  }

  private static string? Generate()
  {
    return null;
  }

  private static string GenerateFromIndex(int index)
  {
    return index.ToString();
  }
}
