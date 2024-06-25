using System;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.StatefulTests;

public static class RandomizationSourceStatefulExtensions
{
  public class WithStateTests
  {
    [Fact]
    public void MaintainsRandomizer()
    {
      const int value = 42;
      var arranged =
        new ArrangedRandomizationSource(getNextIntEnumerable: (lower, upper) => new[] { value, value, value });
      var withState = arranged.WithState(Guid.NewGuid());

      var actualNextInt = withState.NextIntInRange(int.MinValue, int.MaxValue);

      Assert.Equal(value, actualNextInt);
    }
  }
}
