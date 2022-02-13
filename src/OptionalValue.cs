namespace Jds.TestingUtils.Randomization;

/// <summary>
///   Simple wrapper type to safely deal with nullable values.
/// </summary>
/// <typeparam name="T">A wrapped type.</typeparam>
internal record OptionalValue<T>
{
  public T? Value { get; init; }

  public bool IsSome => Value != null && !Value.Equals(obj: default(T?));

  /// <summary>
  ///   Static constructor of nullable values.
  /// </summary>
  /// <param name="possibleValue">A possible value.</param>
  /// <returns>An <see cref="OptionalValue{T}" />.</returns>
  public static OptionalValue<T> Of(T? possibleValue)
  {
    return possibleValue == null
      ? new OptionalValue<T>()
      : new OptionalValue<T> { Value = possibleValue };
  }

  /// <summary>
  ///   Static constructor of none/null values.
  /// </summary>
  /// <returns>An <see cref="OptionalValue{T}" /> with a <see cref="Value" /> of null.</returns>
  public static OptionalValue<T> None()
  {
    return new OptionalValue<T>();
  }
}
