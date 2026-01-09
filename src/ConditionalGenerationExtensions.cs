namespace Jds.TestingUtils.Randomization;

public static class ConditionalGenerationExtensions
{
  /// <summary>
  ///   Continuously generates values using the provided generator function until a value satisfies the given condition.
  /// </summary>
  /// <param name="randomizationSource">The source of randomization.</param>
  /// <param name="generator">
  ///   A function that generates values of type <typeparamref name="TResult" />.
  ///   This function receives this <see cref="IRandomizationSource" /> as a parameter.
  /// </param>
  /// <param name="condition">
  ///   <para>
  ///     A predicate function that determines whether the generated value satisfies a condition.
  ///     The first value which satisfies the condition is returned.
  ///   </para>
  ///   <para>Use caution when specifying the <paramref name="condition" /> to ensure it does not lead to infinite loops.</para>
  /// </param>
  /// <typeparam name="TResult">The type of the values being generated.</typeparam>
  /// <returns>A value of type <typeparamref name="TResult" /> that satisfies the given condition.</returns>
  /// <remarks>
  ///   <para>
  ///     This method is useful for generating values that meet specific criteria in situations where it is
  ///     more efficient to simply retry value generation than to create a custom generation function.
  ///   </para>
  ///   <para>
  ///     For example, generating a pseudorandom number which is a multiple of 3 can be done as follows:
  ///   </para>
  ///   <code>int multipleOfThree = Randomizer.Shared.GenerateUntil(static source =&gt; source.IntPositive(), static value =&gt; value &gt; 0 &amp;&amp; value % 3 == 0);</code>
  /// </remarks>
  public static TResult GenerateUntil<TResult>(
    this IRandomizationSource randomizationSource,
    Func<IRandomizationSource, TResult> generator,
    Func<TResult, bool> condition
  )
  {
    while (true)
    {
      var result = generator(randomizationSource);
      if (condition(result))
      {
        return result;
      }
    }
  }

  /// <summary>
  ///   Continuously generates values using the provided generator function until a value satisfies the given condition.
  /// </summary>
  /// <param name="randomizationSource">The source of randomization.</param>
  /// <param name="generator">
  ///   A function that generates values of type <typeparamref name="TResult" />.
  ///   This function receives this <see cref="IRandomizationSource" /> as a parameter.
  /// </param>
  /// <param name="condition">
  ///   <para>
  ///     A predicate function that determines whether the generated value satisfies a condition.
  ///     The first value which satisfies the condition is returned.
  ///   </para>
  ///   <para>Provide a <paramref name="cancellationToken" /> to prevent infinite loops.</para>
  /// </param>
  /// <param name="cancellationToken">
  ///   An asynchronous cancellation token that can be used to cancel the operation.
  ///   This token is verified before each iteration to allow for cancellation of the operation.
  /// </param>
  /// <typeparam name="TResult">The type of the values being generated.</typeparam>
  /// <returns>A value of type <typeparamref name="TResult" /> that satisfies the given condition.</returns>
  /// <remarks>
  ///   <para>
  ///     This method is useful for generating values that meet specific criteria in situations where it is
  ///     more efficient to simply retry value generation than to create a custom generation function.
  ///   </para>
  /// </remarks>
  /// <exception cref="OperationCanceledException">Thrown when <paramref name="cancellationToken" /> is cancelled.</exception>
  /// <exception cref="ObjectDisposedException">
  ///   Thrown when the source associated with <paramref name="cancellationToken" /> is disposed.
  /// </exception>
  /// "
  public static async Task<TResult> GenerateUntilAsync<TResult>(
    this IRandomizationSource randomizationSource,
    Func<IRandomizationSource, Task<TResult>> generator,
    Func<TResult, bool> condition,
    CancellationToken cancellationToken = default
  )
  {
    while (true)
    {
      cancellationToken.ThrowIfCancellationRequested();
      var result = await generator(randomizationSource);
      if (condition(result))
      {
        return result;
      }
    }
  }

  /// <summary>
  ///   Continuously generates values using the provided generator function until a value satisfies the given condition.
  /// </summary>
  /// <param name="randomizationSource">The source of randomization.</param>
  /// <param name="generator">
  ///   A function that generates values of type <typeparamref name="TResult" />.
  ///   This function receives this <see cref="IRandomizationSource" /> as a parameter.
  /// </param>
  /// <param name="condition">
  ///   <para>
  ///     A predicate function that determines whether the generated value satisfies a condition.
  ///     The first value which satisfies the condition is returned.
  ///   </para>
  ///   <para>Provide a <paramref name="cancellationToken" /> to prevent infinite loops.</para>
  /// </param>
  /// <param name="cancellationToken">
  ///   An asynchronous cancellation token that can be used to cancel the operation.
  ///   This token is verified before each iteration to allow for cancellation of the operation.
  /// </param>
  /// <typeparam name="TResult">The type of the values being generated.</typeparam>
  /// <returns>A value of type <typeparamref name="TResult" /> that satisfies the given condition.</returns>
  /// <remarks>
  ///   <para>
  ///     This method is useful for generating values that meet specific criteria in situations where it is
  ///     more efficient to simply retry value generation than to create a custom generation function.
  ///   </para>
  /// </remarks>
  /// <exception cref="OperationCanceledException">Thrown when <paramref name="cancellationToken" /> is cancelled.</exception>
  /// <exception cref="ObjectDisposedException">
  ///   Thrown when the source associated with <paramref name="cancellationToken" /> is disposed.
  /// </exception>
  /// "
  public static async Task<TResult> GenerateUntilAsync<TResult>(
    this IRandomizationSource randomizationSource,
    Func<IRandomizationSource, Task<TResult>> generator,
    Func<TResult, Task<bool>> condition,
    CancellationToken cancellationToken = default
  )
  {
    while (true)
    {
      cancellationToken.ThrowIfCancellationRequested();
      var result = await generator(randomizationSource);
      if (await condition(result))
      {
        return result;
      }
    }
  }
}
