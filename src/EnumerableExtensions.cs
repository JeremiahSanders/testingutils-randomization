namespace Jds.TestingUtils.Randomization;

public static class EnumerableExtensions
{
  /// <summary>
  ///   Creates an <see cref="IEnumerable{T}" /> with a randomly-determined count of
  ///   <paramref name="factory" />-provided values.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="factory">A factory method which returns new <see cref="T" /> instances.</param>
  /// <param name="inclusiveMinCount">A minimum count.</param>
  /// <param name="exclusiveMaxCount">An exclusive maximum count.</param>
  /// <typeparam name="T">A generated type.</typeparam>
  /// <returns>A randomly-generated <see cref="IEnumerable{T}" />.</returns>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when <paramref name="exclusiveMaxCount" /> is less than <c>1</c>,
  ///   when <paramref name="inclusiveMinCount" /> is less than <c>0</c>,
  ///   or when <paramref name="inclusiveMinCount" /> is <c>&gt;=</c> <paramref name="exclusiveMaxCount" />.
  /// </exception>
  public static IEnumerable<T> Enumerable<T>(this IRandomizationSource randomizationSource, Func<T> factory,
    int inclusiveMinCount, int exclusiveMaxCount)
  {
    return randomizationSource.Enumerable(_ => factory(), inclusiveMinCount, exclusiveMaxCount);
  }

  /// <summary>
  ///   Creates an <see cref="IEnumerable{T}" /> with a randomly-determined count of
  ///   <paramref name="factory" />-provided values.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="factory">
  ///   A factory method which receives the current item index and returns a new
  ///   <typeparamref name="T" />.
  /// </param>
  /// <param name="inclusiveMinCount">A minimum count.</param>
  /// <param name="exclusiveMaxCount">An exclusive maximum count.</param>
  /// <typeparam name="T">A generated type.</typeparam>
  /// <returns>A randomly-generated <see cref="IEnumerable{T}" />.</returns>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when <paramref name="exclusiveMaxCount" /> is less than <c>1</c>,
  ///   when <paramref name="inclusiveMinCount" /> is less than <c>0</c>,
  ///   or when <paramref name="inclusiveMinCount" /> is <c>&gt;=</c> <paramref name="exclusiveMaxCount" />.
  /// </exception>
  public static IEnumerable<T> Enumerable<T>(this IRandomizationSource randomizationSource, Func<int, T> factory,
    int inclusiveMinCount, int exclusiveMaxCount)
  {
    if (exclusiveMaxCount < 1)
    {
      throw new ArgumentOutOfRangeException(nameof(exclusiveMaxCount), "Exclusive max must be greater than 0.");
    }

    if (inclusiveMinCount >= exclusiveMaxCount)
    {
      throw new ArgumentOutOfRangeException(nameof(inclusiveMinCount),
        "Inclusive min must be less than exclusive max.");
    }

    if (inclusiveMinCount < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(inclusiveMinCount),
        "Inclusive min must be greater than or equal to 0."
      );
    }

    var count = randomizationSource.NextIntInRange(inclusiveMinCount, exclusiveMaxCount);

    return System.Linq.Enumerable.Range(0, count).Select(factory);
  }
}
