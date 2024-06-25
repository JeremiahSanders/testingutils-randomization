namespace Jds.TestingUtils.Randomization;

/// <summary>
///   Methods extending <see cref="IRandomizationSource" /> to support <see cref="IStatefulRandomizationSource{TState}" />.
/// </summary>
public static class RandomizationSourceStatefulExtensions
{
  /// <summary>
  ///   Creates a <see cref="IStatefulRandomizationSource{TState}" /> from this <see cref="IRandomizationSource" />
  ///   having a <see cref="IStatefulRandomizationSource{TState}.State" /> of <paramref name="state" />.
  /// </summary>
  /// <param name="source">This <see cref="IRandomizationSource" />.</param>
  /// <param name="state">An initial state value.</param>
  /// <typeparam name="TState">A state object.</typeparam>
  /// <returns>A <see cref="IStatefulRandomizationSource{TState}" />.</returns>
  public static IStatefulRandomizationSource<TState> WithState<TState>(this IRandomizationSource source, TState state)
    where TState : notnull
  {
    return Randomizer.WithState(
      source is StatefulSource<TState> statefulSource
        ? statefulSource.RandomizationSource
        : source,
      state
    );
  }
}
