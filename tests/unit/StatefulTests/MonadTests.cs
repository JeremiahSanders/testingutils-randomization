using System;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.StatefulTests;

public static class MonadTests
{
  public class BindTests
  {
    [Fact]
    public void ReplacesInstance()
    {
      const int initialState = 42;
      var initialSource = new ArrangedRandomizationSource(
        () => throw new NotSupportedException(),
        () => throw new NotSupportedException(),
        (lower, upper) => throw new NotSupportedException(),
        (lower, upper) => throw new NotSupportedException()
      );
      var replacementSource = Randomizer.Shared;

      var initialStatefulSource = Randomizer.OfState(initialSource, initialState);

      var afterBind = initialStatefulSource.Bind(value => Randomizer.OfState(replacementSource, value));

      Assert.Equal(initialState, afterBind.State);
      afterBind.NextDouble(); // would throw with initial source
      afterBind.NextFloat(); // would throw with initial source
      afterBind.NextLongInRange(1, 100); // would throw with initial source
      afterBind.NextIntInRange(1, 100); // would throw with initial source
    }
  }

  public class MapTests
  {
    [Fact]
    public void ModifiesState()
    {
      const int initialState = 42;
      var expectedState = initialState.ToString();
      var initialSource = Randomizer.OfState(initialState);

      var actual = initialSource.Map(Stringifier);

      Assert.Equal(expectedState, actual.State);
      return;

      static string Stringifier(int currentState)
      {
        return currentState.ToString();
      }
    }
  }
}
