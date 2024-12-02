using System;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.ReadOnlyListExtensionsTests;

public static class GetRandomItemTests
{
  public class ParameterlessTests
  {
    [Fact]
    public void GivenItems_ItReturnsRandomItem()
    {
      var items = Enumerable.Range(1, 20).ToArray();

      var actual = items.GetRandomItem();

      items.Should().Contain(actual);
    }

    [Fact]
    public void GivenOneItem_ItReturnsOnlyItem()
    {
      var expected = Randomizer.Shared.Int();
      var items = new[] { expected };

      var actual = items.GetRandomItem();

      actual.Should().Be(expected);
    }

    [Fact]
    public void GivenNoItems_ItThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => Array.Empty<int>().GetRandomItem());
    }
  }

  public class GivenRandomizationProviderTests
  {
    private readonly IRandomizationSource _randomizationSource;

    public GivenRandomizationProviderTests()
    {
      _randomizationSource = new RandomRandomizationSource(new Random());
    }

    [Fact]
    public void GivenItems_ItReturnsRandomItem()
    {
      var items = Enumerable.Range(1, 20).ToArray();

      var actual = items.GetRandomItem(_randomizationSource);

      items.Should().Contain(actual);
    }

    [Fact]
    public void GivenOneItem_ItReturnsOnlyItem()
    {
      var expected = Randomizer.Shared.Int();
      var items = new[] { expected };

      var actual = items.GetRandomItem(_randomizationSource);

      actual.Should().Be(expected);
    }

    [Fact]
    public void GivenNoItems_ItThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => Array.Empty<int>().GetRandomItem(_randomizationSource));
    }
  }
}
