using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.NumericRandomizationSourceExtensionsTests;

public class BooleanTests
{
  [Fact]
  public void Given1000Executions_ReturnsAtLeast300False()
  {
    const int testCount = 1000;
    const int minExpected = 300;

    var results = Enumerable.Range(1, testCount).Select(_ => Randomizer.Shared.Boolean()).ToArray();
    var falses = results.Count(item => !item);
    Assert.True(falses > minExpected, $"Expected at least {minExpected}; actual false count: {falses}");
  }
}
