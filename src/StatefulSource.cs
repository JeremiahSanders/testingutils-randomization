namespace Jds.TestingUtils.Randomization;

/// <summary>
///   Default implementation of <see cref="IStatefulRandomizationSource{TState}" /> which simply wraps
///   a <see cref="IRandomizationSource" />.
/// </summary>
/// <typeparam name="TState">A state type.</typeparam>
internal record StatefulSource<TState> : IStatefulRandomizationSource<TState> where TState : notnull
{
  public StatefulSource(IRandomizationSource randomizationSource, TState state)
  {
    State = state;
    RandomizationSource = randomizationSource;
  }

  /// <summary>
  ///   Gets the randomization source implementation.
  /// </summary>
  internal IRandomizationSource RandomizationSource { get; }

  /// <inheritdoc cref="IRandomizationSource.NextDouble" />
  public double NextDouble()
  {
    return RandomizationSource.NextDouble();
  }

  /// <inheritdoc cref="IRandomizationSource.NextFloat" />
  public float NextFloat()
  {
    return RandomizationSource.NextFloat();
  }

  /// <inheritdoc cref="IRandomizationSource.NextIntInRange" />
  public int NextIntInRange(int minInclusive, int maxExclusive)
  {
    return RandomizationSource.NextIntInRange(minInclusive, maxExclusive);
  }

  /// <inheritdoc cref="IRandomizationSource.NextLongInRange" />
  public long NextLongInRange(long minInclusive, long maxExclusive)
  {
    return RandomizationSource.NextLongInRange(minInclusive, maxExclusive);
  }

  /// <inheritdoc cref="IStatefulRandomizationSource{TState}.State" />
  public TState State { get; }
}
