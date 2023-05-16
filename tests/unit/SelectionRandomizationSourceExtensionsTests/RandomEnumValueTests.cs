using System;
using System.Linq;
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
  public void GivenValuesAndExclusions_ItReturnsRandomDifferentItem()
  {
    const HasValues except = HasValues.Third;
    var items = Enum.GetValues<HasValues>();
    var filteredItems = items.Where(value => value != except).ToList();

    var actual = Randomizer.Shared.RandomEnumValue(except);

    Assert.Contains(actual, filteredItems);
    Assert.NotEqual(except, actual);
  }

  [Fact]
  public void GivenExclusionsReducingToOneAvailableValue_ItReturnsRandomExpectedItem()
  {
    const HasValues expected = HasValues.Second;

    var actual = Randomizer.Shared.RandomEnumValue(HasValues.First, HasValues.Third);

    Assert.Equal(expected, actual);
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

  [Fact]
  public void GivenNoValuesAndExcept_ItThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => Randomizer.Shared.RandomEnumValue(default(NoValues)));
  }

  [Fact]
  public void GivenAllValuesExcluded_ItThrowsInvalidOperationException()
  {
    Assert.Throws<InvalidOperationException>(() => Randomizer.Shared.RandomEnumValue(OneValue.OnlyOne));
  }

  private enum HasValues { First = 1, Second = 2, Third = 3 }

  private enum OneValue { OnlyOne }

  private enum NoValues { }
}
