using System;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.DateTimeRandomizationSourceExtensionsTests;

public class DateTimeUtcTests
{
  [Fact]
  public void AcceptsParameters()
  {
    var year = Randomizer.Shared.IntInRange(1, 10000);
    var month = Randomizer.Shared.IntInRange(1, 13);
    var day = Randomizer.Shared.IntInRange(1, 29);
    var hour = Randomizer.Shared.IntInRange(0, 24);
    var minute = Randomizer.Shared.IntInRange(0, 59);
    var second = Randomizer.Shared.IntInRange(0, 59);

    var expected = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);

    var actual = Randomizer.Shared.DateTimeUtc(year, month, day, hour, minute, second);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GivenNoParameters_ReturnsDateTimeInUtc()
  {
    var actual = Randomizer.Shared.DateTimeUtc();

    actual.Kind.Should().Be(DateTimeKind.Utc);
  }
}
