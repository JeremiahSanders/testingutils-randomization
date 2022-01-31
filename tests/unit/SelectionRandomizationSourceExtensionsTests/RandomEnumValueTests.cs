using System;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.SelectionRandomizationSourceExtensionsTests;

public class RandomEnumValueTests
{
  [Fact]
  public void GivenValues_ItReturnsRandomItem()
  {
    var items = Enum.GetValues<HasValues>();

    var actual = Randomizer.Shared.RandomEnumValue<HasValues>();

    Assert.Contains(actual, items);
  }

  [Fact]
  public void GivenOneValue_ItReturnsOnlyItem()
  {
    var expected = OneValue.OnlyOne;

    var actual = Randomizer.Shared.RandomEnumValue<OneValue>();

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void GivenNoValues_ItThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => Randomizer.Shared.RandomEnumValue<NoValues>());
  }

  private enum HasValues { First = 1, Second = 2, Third = 3 }

  private enum OneValue { OnlyOne }

  private enum NoValues { }
}
