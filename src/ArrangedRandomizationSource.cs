namespace Jds.TestingUtils.Randomization;

/// <summary>
///   A <see cref="IRandomizationSource" /> which uses delegates to provide values.
/// </summary>
/// <remarks>
///   This source is useful for creating pseudo-random values from a custom algorithm.
/// </remarks>
public class ArrangedRandomizationSource : IRandomizationSource
{
  private readonly Func<IEnumerable<double>> _getNextDoubleEnumerable;
  private readonly Func<IEnumerable<float>> _getNextFloatEnumerable;
  private readonly Func<int, int, IEnumerable<int>> _getNextIntEnumerable;
  private readonly Func<long, long, IEnumerable<long>> _getNextLongEnumerable;

  public ArrangedRandomizationSource(
    Func<IEnumerable<double>>? getNextDoubleEnumerable = null,
    Func<IEnumerable<float>>? getNextFloatEnumerable = null,
    Func<int, int, IEnumerable<int>>? getNextIntEnumerable = null,
    Func<long, long, IEnumerable<long>>? getNextLongEnumerable = null
  )
  {
    _getNextDoubleEnumerable = getNextDoubleEnumerable ?? (() =>
        throw new InvalidOperationException(message: $"No {nameof(getNextDoubleEnumerable)} provided")
      );
    _getNextFloatEnumerable = getNextFloatEnumerable ?? (() =>
        throw new InvalidOperationException(message: $"No {nameof(getNextFloatEnumerable)} provided")
      );
    _getNextIntEnumerable = getNextIntEnumerable ?? ((int _, int _) =>
        throw new InvalidOperationException(message: $"No {nameof(getNextIntEnumerable)} provided")
      );
    _getNextLongEnumerable = getNextLongEnumerable ?? ((long _, long _) =>
        throw new InvalidOperationException(message: $"No {nameof(getNextLongEnumerable)} provided")
      );
  }

  public double NextDouble()
  {
    return _getNextDoubleEnumerable().First();
  }

  public int NextIntInRange(int minInclusive, int maxExclusive)
  {
    return _getNextIntEnumerable(minInclusive, maxExclusive).First();
  }

  public long NextLongInRange(long minInclusive, long maxExclusive)
  {
    return _getNextLongEnumerable(minInclusive, maxExclusive).First();
  }

  public float NextFloat()
  {
    return _getNextFloatEnumerable().First();
  }
}
