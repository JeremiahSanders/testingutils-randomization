using System;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.SelectionRandomizationSourceExtensionsTests;

public class RandomListItemTests
{
  [Fact]
  public void GivenItems_ItReturnsRandomItem()
  {
    var items = Enumerable.Range(1, 20).ToArray();

    var actual = Randomizer.Shared.RandomListItem(items);

    items.Should().Contain(actual);
  }

  [Fact]
  public void GivenOneItem_ItReturnsOnlyItem()
  {
    var expected = Randomizer.Shared.Int();

    var actual = Randomizer.Shared.RandomListItem(new[] {expected});

    actual.Should().Be(expected);
  }

  [Fact]
  public void GivenNoItems_ItThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => Randomizer.Shared.RandomListItem(Array.Empty<int>()));
  }
}
