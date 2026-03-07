using System;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.DemographicsRandomizationSourceExtensionsTests;

public class DemographicsBirthDateTimeTests
{
  public DemographicsBirthDateTimeTests(ITestOutputHelper testOutputHelper)
  {
    TestOutputHelper = testOutputHelper;
  }

  private ITestOutputHelper TestOutputHelper { get; }

  [Fact]
  public void GivenNoRange_ReturnsDateBetween18And95YearsBeforeReference()
  {
    const int bufferDays = 5; // Extra days to extend the allowed range, to avoid random edge case failures.
    var referenceDay = new DateTime(Randomizer.Shared.IntInRange(2000, 2022),
      Randomizer.Shared.IntInRange(1, 13), Randomizer.Shared.IntInRange(1, 29));
    var youngest = referenceDay.Subtract(TimeSpan.FromDays(18.0 * 365.25)).AddDays(bufferDays);
    var oldest = referenceDay.Subtract(TimeSpan.FromDays(96.0 * 365.25)).AddDays(-bufferDays);

    var actual = Randomizer.Shared.DemographicsBirthDateTime(referenceDay);

    TestOutputHelper.WriteLine($"Expecting that {actual} is prior to {youngest} and after {oldest}");

    actual.Should().BeBefore(youngest);
    actual.Should().BeAfter(oldest);
  }

  [Fact]
  public void GivenNoRangeNoReferenceDate_ReturnsDateBetween18And95YearsAgo()
  {
    const int bufferDays = 5; // Extra days to extend the allowed range, to avoid random edge case failures.
    var youngest = DateTime.UtcNow.Subtract(TimeSpan.FromDays(18.0 * 365.25)).AddDays(bufferDays);
    var oldest = DateTime.UtcNow.Subtract(TimeSpan.FromDays(96.0 * 365.25)).AddDays(-bufferDays);

    var actual = Randomizer.Shared.DemographicsBirthDateTime();

    TestOutputHelper.WriteLine($"Expecting that {actual} is prior to {youngest} and after {oldest}");

    actual.Should().BeBefore(youngest);
    actual.Should().BeAfter(oldest);
  }

  [Fact]
  public void GivenRangeAndNoReference_ReturnsDateInRangeAgo()
  {
    const int bufferDays = 5; // Extra days to extend the allowed range, to avoid random edge case failures.
    var referenceDay = DateTime.UtcNow;
    var range = (MinAgeInYears: Randomizer.Shared.IntInRange(10, 30),
      MaxAgeInYearsExclusive: Randomizer.Shared.IntInRange(30, 80));
    var youngest = referenceDay.Subtract(TimeSpan.FromDays(range.MinAgeInYears * 365.25))
      .AddDays(bufferDays);
    var oldest = referenceDay.Subtract(TimeSpan.FromDays(range.MaxAgeInYearsExclusive * 365.25))
      .AddDays(-bufferDays);

    var actual = Randomizer.Shared.DemographicsBirthDateTime(range, referenceDay);

    TestOutputHelper.WriteLine($"Expecting that {actual} is prior to {youngest} and after {oldest}");

    actual.Should().BeBefore(youngest);
    actual.Should().BeAfter(oldest);
  }

  [Fact]
  public void GivenRangeAndReference_ReturnsDateInRangeBeforeReference()
  {
    const int bufferDays = 5; // Extra days to extend the allowed range, to avoid random edge case failures.
    var referenceDay = new DateTime(Randomizer.Shared.IntInRange(2000, 2022),
      Randomizer.Shared.IntInRange(1, 13), Randomizer.Shared.IntInRange(1, 29));
    var range = (MinAgeInYears: Randomizer.Shared.IntInRange(10, 30),
      MaxAgeInYearsExclusive: Randomizer.Shared.IntInRange(30, 80));
    var youngest = referenceDay.Subtract(TimeSpan.FromDays(range.MinAgeInYears * 365.25))
      .AddDays(bufferDays);
    var oldest = referenceDay.Subtract(TimeSpan.FromDays(range.MaxAgeInYearsExclusive * 365.25))
      .AddDays(-bufferDays);

    var actual = Randomizer.Shared.DemographicsBirthDateTime(range, referenceDay);

    TestOutputHelper.WriteLine($"Expecting that {actual} is prior to {youngest} and after {oldest}");

    actual.Should().BeBefore(youngest);
    actual.Should().BeAfter(oldest);
  }

  [Fact]
  public void GivenRangeBelow1_ThrowsArgumentOutOfRangeException()
  {
    Assert.Throws<ArgumentOutOfRangeException>(() =>
      Randomizer.Shared.DemographicsBirthDateTime((MinAgeInYears: 0, MaxAgeInYearsExclusive: 1)));
    Assert.Throws<ArgumentOutOfRangeException>(() =>
      Randomizer.Shared.DemographicsBirthDateTime((MinAgeInYears: 1, MaxAgeInYearsExclusive: 0)));
  }
}
