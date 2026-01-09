using System;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.DateTimeRandomizationSourceExtensionsTests;

public class DateOnlyTests
{
  [Fact]
  public void AcceptsParameters()
  {
    var year = Randomizer.Shared.IntInRange(1, 10000);
    var month = Randomizer.Shared.IntInRange(1, 13);
    var day = Randomizer.Shared.IntInRange(1, 29);
    var expected = new DateOnly(year, month, day);

    var actual = Randomizer.Shared.DateOnly(day: day, month: month, year: year);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GivenNoParameters_ReturnsDateOnly()
  {
    var actual = Randomizer.Shared.DateOnly();
    actual.Should().BeOnOrAfter(DateOnly.MinValue);
    actual.Should().BeOnOrBefore(DateOnly.MaxValue);
  }
}
