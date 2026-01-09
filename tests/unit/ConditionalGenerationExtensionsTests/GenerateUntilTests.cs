using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.ConditionalGenerationExtensionsTests;

public class GenerateUntilTests
{
  [Fact]
  public void ReturnsValue()
  {
    var multipleOfThree = Randomizer.Shared.GenerateUntil(
      static source => source.IntPositive(),
      static value => value > 0 && value % 3 == 0
    );

    var remainderOfThree = multipleOfThree % 3;
    Assert.Equal(0, remainderOfThree);
  }

  [Fact]
  public void RetriesOnFalse()
  {
    var callCount = 0;
    const int plannedFailures = 3;
    const int expectedMiniumCalls = plannedFailures + 1;
    var multipleOfThree = Randomizer.Shared.GenerateUntil(
      static source => source.IntPositive(),
      value =>
      {
        callCount++;
        var isMatch = callCount > plannedFailures && value > 0 && value % 3 == 0;
        return isMatch;
      });

    var remainderOfThree = multipleOfThree % 3;
    Assert.Equal(0, remainderOfThree);
    Assert.True(callCount >= expectedMiniumCalls);
  }
}
