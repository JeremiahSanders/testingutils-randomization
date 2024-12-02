using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.StatefulTests;

public static class RandomizationSourceStatefulExtensions
{
  public class WithStateTests
  {
    [Fact]
    [SuppressMessage("ReSharper", "UnusedParameter.Local", Justification = "Included for clarity of intent.")]
    public void MaintainsRandomizer()
    {
      const int value = 42;
      var arranged =
        new ArrangedRandomizationSource(getNextIntEnumerable: (lower, upper) => [value, value, value]);
      var withState = arranged.WithState(Guid.NewGuid());

      var actualNextInt = withState.NextIntInRange(int.MinValue, int.MaxValue);

      Assert.Equal(value, actualNextInt);
    }
  }
}
