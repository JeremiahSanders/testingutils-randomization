namespace Jds.TestingUtils.Randomization;

public static class NumericRandomizationSourceExtensions
{
  /// <summary>
  ///   Gets a pseudo-random <see cref="bool" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   A <see cref="bool" />.
  /// </returns>
  public static bool Boolean(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextIntInRange(1, 3) % 2 == 0;
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="byte" />, greater than or equal to <see cref="byte.MinValue" />, and less than
  ///   <see cref="byte.MaxValue" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   A <see cref="byte" />, greater than or equal to <see cref="byte.MinValue" />, and less than
  ///   <see cref="byte.MaxValue" />.
  /// </returns>
  public static byte Byte(this IRandomizationSource randomizationSource)
  {
    return (byte)randomizationSource.NextIntInRange(byte.MinValue, byte.MaxValue);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="byte" />, greater than or equal to <paramref name="minInclusive" />, and less than
  ///   <paramref name="maxExclusive" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="minInclusive">A minimum limit.</param>
  /// <param name="maxExclusive">An exclusive upper limit.</param>
  /// <returns>
  ///   A <see cref="byte" />, greater than or equal to <paramref name="minInclusive" />, and less than
  ///   <paramref name="maxExclusive" />.
  /// </returns>
  public static byte ByteInRange(this IRandomizationSource randomizationSource, byte minInclusive, byte maxExclusive)
  {
    return (byte)randomizationSource.NextIntInRange(minInclusive, maxExclusive);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="decimal" /> between <see cref="decimal.MinValue" /> and
  ///   <see cref="decimal.MaxValue" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   A <see cref="decimal" /> between <see cref="decimal.MinValue" /> and
  ///   <see cref="decimal.MaxValue" />.
  /// </returns>
  public static decimal Decimal(this IRandomizationSource randomizationSource)
  {
    return decimal.MaxValue * Convert.ToDecimal(randomizationSource.NextDouble() - 1d / 2d);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="decimal" /> between <paramref name="minInclusive" /> and
  ///   <paramref name="maxExclusive" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="minInclusive">A minimum limit.</param>
  /// <param name="maxExclusive">An exclusive upper limit.</param>
  /// <returns>
  ///   A <see cref="decimal" /> between <paramref name="minInclusive" /> and
  ///   <paramref name="maxExclusive" />.
  /// </returns>
  public static decimal DecimalInRange(this IRandomizationSource randomizationSource,
    decimal minInclusive,
    decimal maxExclusive
  )
  {
    if (maxExclusive <= minInclusive)
    {
      throw new ArgumentOutOfRangeException(nameof(maxExclusive), "Max must be greater than min.");
    }

    if (minInclusive < decimal.Zero && maxExclusive > decimal.Zero)
    {
      // Since the range could exceed Decimal, create the below-zero and above-zero portion of the range independently and sum. 
      return Convert.ToDecimal(randomizationSource.NextDouble()) * minInclusive +
             Convert.ToDecimal(randomizationSource.NextDouble()) * maxExclusive;
    }

    var multiplier = Convert.ToDecimal(randomizationSource.NextDouble());
    var sum = minInclusive + (maxExclusive - minInclusive) * multiplier;

    return sum < maxExclusive ? sum : minInclusive;
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="decimal" /> between <see cref="decimal.Zero" /> and
  ///   <see cref="decimal.MaxValue" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   A <see cref="decimal" /> between <see cref="decimal.MinValue" /> and
  ///   <see cref="decimal.MaxValue" />.
  /// </returns>
  public static decimal DecimalPositive(this IRandomizationSource randomizationSource)
  {
    return Convert.ToDecimal(randomizationSource.NextDouble()) * decimal.MaxValue;
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="decimal" /> between <see cref="decimal.MinValue" /> and
  ///   <see cref="decimal.Zero" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   A <see cref="decimal" /> between <see cref="decimal.MinValue" /> and
  ///   <see cref="decimal.MaxValue" />.
  /// </returns>
  public static decimal DecimalNegative(this IRandomizationSource randomizationSource)
  {
    return Convert.ToDecimal(randomizationSource.NextDouble()) * decimal.MinValue;
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="double" />, using <see cref="IRandomizationSource.NextDouble" />.
  ///   The value should be greater than or equal to 0.0, and less than 1.0.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
  public static double Double(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextDouble();
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="float" />, using <see cref="IRandomizationSource.NextFloat" />.
  ///   The value should be greater than or equal to 0.0, and less than 1.0.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
  public static float Float(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextFloat();
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="int" />, using <see cref="IRandomizationSource.NextIntInRange" />.
  ///   The value should be greater than or equal to <see cref="int.MinValue" />, and less than <see cref="int.MaxValue" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   An <see cref="int" /> greater than or equal to <see cref="int.MinValue" />, and less than
  ///   <see cref="int.MaxValue" />.
  /// </returns>
  public static int Int(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextIntInRange(int.MinValue, int.MaxValue);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="int" />, using <see cref="IRandomizationSource.NextIntInRange" />.
  ///   The value should be greater than or equal to <paramref name="minInclusive" />, and less than
  ///   <paramref name="maxExclusive" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="minInclusive">A minimum limit.</param>
  /// <param name="maxExclusive">An exclusive upper limit.</param>
  /// <returns>
  ///   An <see cref="int" /> greater than or equal to <paramref name="minInclusive" />, and less than
  ///   <paramref name="maxExclusive" />.
  /// </returns>
  public static int IntInRange(this IRandomizationSource randomizationSource, int minInclusive, int maxExclusive)
  {
    return randomizationSource.NextIntInRange(minInclusive, maxExclusive);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="int" />, using <see cref="IRandomizationSource.NextIntInRange" />.
  ///   The value should be greater than or equal to <see cref="int.MinValue" />, and less than 0.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   An <see cref="int" /> greater than or equal to <see cref="int.MinValue" />, and less than 0.
  /// </returns>
  public static int IntNegative(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextIntInRange(int.MinValue, 0);
  }


  /// <summary>
  ///   Gets a pseudo-random <see cref="int" />, using <see cref="IRandomizationSource.NextIntInRange" />.
  ///   The value should be greater than or equal to 0, and less than <see cref="int.MaxValue" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   An <see cref="int" /> greater than or equal to 0, and less than <see cref="int.MaxValue" />.
  /// </returns>
  public static int IntPositive(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextIntInRange(0, int.MaxValue);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="long" />, using <see cref="IRandomizationSource.NextLongInRange" />.
  ///   The value should be greater than or equal to <see cref="long.MinValue" />, and less than
  ///   <see cref="long.MaxValue" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   An <see cref="long" /> greater than or equal to <see cref="long.MinValue" />, and less than
  ///   <see cref="long.MaxValue" />.
  /// </returns>
  public static long Long(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextLongInRange(long.MinValue, long.MaxValue);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="long" />, using <see cref="IRandomizationSource.NextLongInRange" />.
  ///   The value should be greater than or equal to <paramref name="minInclusive" />, and less than
  ///   <paramref name="maxExclusive" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="minInclusive">A minimum limit.</param>
  /// <param name="maxExclusive">An exclusive upper limit.</param>
  /// <returns>
  ///   An <see cref="long" /> greater than or equal to <see cref="long.MinValue" />, and less than
  ///   <see cref="long.MaxValue" />.
  /// </returns>
  public static long LongInRange(this IRandomizationSource randomizationSource, long minInclusive, long maxExclusive)
  {
    return randomizationSource.NextLongInRange(minInclusive, maxExclusive);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="long" />, using <see cref="IRandomizationSource.NextLongInRange" />.
  ///   The value should be greater than or equal to <see cref="long.MinValue" />, and less than 0.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   An <see cref="long" /> greater than or equal to <see cref="long.MinValue" />, and less than 0.
  /// </returns>
  public static long LongNegative(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextLongInRange(long.MinValue, 0L);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="long" />, using <see cref="IRandomizationSource.NextLongInRange" />.
  ///   The value should be greater than or equal to 0, and less than <see cref="long.MaxValue" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>
  ///   An <see cref="long" /> greater than or equal to 0, and less than <see cref="long.MaxValue" />.
  /// </returns>
  public static long LongPositive(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.NextLongInRange(0L, long.MaxValue);
  }

  /// <summary>
  ///   Gets a pseudo-random <see cref="ushort" />, using <see cref="IRandomizationSource.NextIntInRange" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>A <see cref="ushort" />.</returns>
  public static ushort UShort(this IRandomizationSource randomizationSource)
  {
    const int maxExclusive = ushort.MaxValue + 1;
    return (ushort)randomizationSource.NextIntInRange(0, maxExclusive);
  }
}
