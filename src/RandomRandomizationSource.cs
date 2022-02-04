namespace Jds.TestingUtils.Randomization;

/// <summary>
///   A <see cref="IRandomizationSource" /> supported by a <see cref="System.Random" />.
/// </summary>
public class RandomRandomizationSource : IRandomizationSource
{
  private readonly Random _random;

  /// <summary>
  ///   Initializes a new instance of <see cref="RandomRandomizationSource" />, using <paramref name="random" />.
  /// </summary>
  /// <param name="random">A pseudo random number generator.</param>
  public RandomRandomizationSource(Random random)
  {
    _random = random;
  }

  /// <summary>
  ///   Initializes a new instance of <see cref="RandomRandomizationSource" />, using <see cref="System.Random.Shared" />.
  /// </summary>
  public RandomRandomizationSource() : this(Random.Shared)
  {
  }

  /// <summary>
  ///   Gets a thread-safe, static instance, supported by <see cref="System.Random.Shared" />.
  /// </summary>
  public static RandomRandomizationSource Shared { get; } = new(Random.Shared);

  /// <inheritdoc cref="IRandomizationSource.NextDouble" />
  public double NextDouble()
  {
    return _random.NextDouble();
  }

  /// <inheritdoc cref="IRandomizationSource.NextLongInRange" />
  public long NextLongInRange(long minInclusive, long maxExclusive)
  {
    return _random.NextInt64(minInclusive, maxExclusive);
  }

  /// <inheritdoc cref="IRandomizationSource.NextIntInRange" />
  public int NextIntInRange(int minInclusive, int maxExclusive)
  {
    return _random.Next(minInclusive, maxExclusive);
  }

  /// <inheritdoc cref="IRandomizationSource.NextFloat" />
  public float NextFloat()
  {
    return _random.NextSingle();
  }
}
