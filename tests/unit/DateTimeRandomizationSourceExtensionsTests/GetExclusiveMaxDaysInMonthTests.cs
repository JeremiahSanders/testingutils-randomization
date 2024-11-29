using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.DateTimeRandomizationSourceExtensionsTests;

public class GetExclusiveMaxDaysInMonthTests
{
  [Theory]
  [InlineData(1, 31)]
  [InlineData(2, 28)]
  [InlineData(3, 31)]
  [InlineData(4, 30)]
  [InlineData(5, 31)]
  [InlineData(6, 30)]
  [InlineData(7, 31)]
  [InlineData(8, 31)]
  [InlineData(9, 30)]
  [InlineData(10, 31)]
  [InlineData(11, 30)]
  [InlineData(12, 31)]
  [InlineData(13, 28)]
  [InlineData(-1, 28)]
  public void ReturnsExpectedExclusiveMaxDaysInMonth(int month, int daysInMonth)
  {
    var expected = daysInMonth + 1;
    DateTimeRandomizationSourceExtensions.GetExclusiveMaxDaysInMonth(month).Should().Be(expected);
  }
}
