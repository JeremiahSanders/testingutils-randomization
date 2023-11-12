using System;
using System.Linq;
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
    Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.Shared.Enumerable(Generate, min, max));
  }

  [Fact]
  public void GivenRange1_ReturnsOneItem()
  {
    var _ = Randomizer.Shared.Enumerable(GenerateFromIndex, 1, 2).SingleOrDefault();
  }

  [Fact]
  public void ReturnsCountInRange()
  {
    var min = 3;
    var max = 7;
    var exclusiveMax = max + 1;
    Randomizer.Shared.Enumerable(Guid.NewGuid, 3, 7);
    var actualCount = Randomizer.Shared.Enumerable(GenerateFromIndex, min, exclusiveMax).Count();

    Assert.True(actualCount >= min, $"Expected {actualCount} to be >= {min}");
    Assert.True(actualCount <= max, $"Expected {actualCount} to be <= {max}");
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
