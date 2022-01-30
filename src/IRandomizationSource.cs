namespace Jds.TestingUtils.Randomization;

/// <summary>
///   A random value provider.
/// </summary>
public interface IRandomizationSource
{
  /// <summary>
  ///   Gets the next <see cref="double" /> that is greater than or equal to 0.0, and less than 1.0.
  /// </summary>
  /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
  double NextDouble();

  /// <summary>
  ///   Gets the next <see cref="float" /> that is greater than or equal to 0.0, and less than 1.0.
  /// </summary>
  /// <returns>A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
  float NextFloat();

  /// <summary>
  ///   Gets the next <see cref="int" />, constrained to a range.
  /// </summary>
  /// <param name="minInclusive">A minimum value limit.</param>
  /// <param name="maxExclusive">An exclusive maximum value limit.</param>
  /// <returns>An <see cref="int" />.</returns>
  int NextIntInRange(int minInclusive, int maxExclusive);

  /// <summary>
  ///   Gets the next <see cref="long" />, constrained to a range.
  /// </summary>
  /// <param name="minInclusive">A minimum value limit.</param>
  /// <param name="maxExclusive">An exclusive maximum value limit.</param>
  /// <returns>A <see cref="long" />.</returns>
  long NextLongInRange(long minInclusive, long maxExclusive);
}
