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

    items.Should().Contain(actual);
  }

  [Fact]
  public void GivenValuesAndExclusions_ItReturnsRandomDifferentItem()
  {
    const HasValues except = HasValues.Third;
    var items = Enum.GetValues<HasValues>();
    var filteredItems = items.Where(value => value != except).ToList();

    var actual = Randomizer.Shared.RandomEnumValue(except);

    filteredItems.Should().Contain(actual);
    actual.Should().NotBe(except);
  }

  [Fact]
  public void GivenExclusionsReducingToOneAvailableValue_ItReturnsRandomExpectedItem()
  {
    const HasValues expected = HasValues.Second;

    var actual = Randomizer.Shared.RandomEnumValue(HasValues.First, HasValues.Third);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GivenOneValue_ItReturnsOnlyItem()
  {
    var expected = OneValue.OnlyOne;

    var actual = Randomizer.Shared.RandomEnumValue<OneValue>();

    actual.Should().Be(expected);
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
