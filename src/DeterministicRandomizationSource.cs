namespace Jds.TestingUtils.Randomization;

/// <summary>
///   A <see cref="IRandomizationSource" /> which returns sequential values from <see cref="IEnumerable{T}" /> sources.
/// </summary>
public class DeterministicRandomizationSource : IRandomizationSource
{
  private readonly Lazy<IEnumerator<double>> _doubleEnumerator;
  private readonly Lazy<IEnumerator<float>> _floatEnumerator;
  private readonly Lazy<IEnumerator<int>> _intEnumerator;
  private readonly Lazy<IEnumerator<long>> _longEnumerator;

  /// <summary>
  ///   Initializes a new instance of <see cref="DeterministicRandomizationSource" />.
  /// </summary>
  /// <param name="doubles">A source of <see cref="double" /> values.</param>
  /// <param name="floats">A source of <see cref="float" /> values.</param>
  /// <param name="ints">A source of <see cref="int" /> values.</param>
  /// <param name="longs">A source of <see cref="long" /> values.</param>
  public DeterministicRandomizationSource(
    IEnumerable<double>? doubles = null,
    IEnumerable<float>? floats = null,
    IEnumerable<int>? ints = null,
    IEnumerable<long>? longs = null
  )
  {
    doubles ??= Array.Empty<double>();
    floats ??= Array.Empty<float>();
    ints ??= Array.Empty<int>();
    longs ??= Array.Empty<long>();

    _doubleEnumerator = new Lazy<IEnumerator<double>>(doubles.GetEnumerator);
    _floatEnumerator = new Lazy<IEnumerator<float>>(floats.GetEnumerator);
    _intEnumerator = new Lazy<IEnumerator<int>>(ints.GetEnumerator);
    _longEnumerator = new Lazy<IEnumerator<long>>(longs.GetEnumerator);
  }

  /// <inheritdoc cref="IRandomizationSource.NextDouble" />
  public double NextDouble()
  {
    return !_doubleEnumerator.Value.MoveNext()
      ? throw new InvalidOperationException(message: "Provided doubles failed to return next value")
      : _doubleEnumerator.Value.Current;
  }

  /// <inheritdoc cref="IRandomizationSource.NextFloat" />
  public float NextFloat()
  {
    return !_floatEnumerator.Value.MoveNext()
      ? throw new InvalidOperationException(message: "Provided floats failed to return next value")
      : _floatEnumerator.Value.Current;
  }

  /// <inheritdoc cref="IRandomizationSource.NextIntInRange" />
  public int NextIntInRange(int minInclusive, int maxExclusive)
  {
    // Keep iterating through until we find a value in range.
    do
    {
      if (!_intEnumerator.Value.MoveNext())
      {
        throw new InvalidOperationException(message: "Provided ints failed to return next value");
      }

      if (_intEnumerator.Value.Current >= minInclusive && _intEnumerator.Value.Current < maxExclusive)
      {
        return _intEnumerator.Value.Current;
      }
    } while (true);
  }

  /// <inheritdoc cref="IRandomizationSource.NextLongInRange" />
  public long NextLongInRange(long minInclusive, long maxExclusive)
  {
    // Keep iterating through until we find a value in range.
    do
    {
      if (!_longEnumerator.Value.MoveNext())
      {
        throw new InvalidOperationException(message: "Provided longs failed to return next value");
      }

      if (_longEnumerator.Value.Current >= minInclusive && _longEnumerator.Value.Current < maxExclusive)
      {
        return _longEnumerator.Value.Current;
      }
    } while (true);
  }
}
