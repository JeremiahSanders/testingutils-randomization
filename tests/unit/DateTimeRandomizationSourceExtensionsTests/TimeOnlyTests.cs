using System;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.DateTimeRandomizationSourceExtensionsTests;

public class TimeOnlyTests
{
  [Fact]
  public void AcceptsParameters()
  {
    var hour = Randomizer.Shared.IntInRange(0, 24);
    var minute = Randomizer.Shared.IntInRange(0, 59);
    var second = Randomizer.Shared.IntInRange(0, 59);
    var expected = new TimeOnly(hour, minute, second);

    var actual = Randomizer.Shared.TimeOnly(second: second, minute: minute, hour: hour);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GivenNoParameters_ReturnsTimeOnly()
  {
    var actual = Randomizer.Shared.TimeOnly();
    actual.Should().BeOnOrAfter(TimeOnly.MinValue);
    actual.Should().BeOnOrBefore(TimeOnly.MaxValue);
  }
}
