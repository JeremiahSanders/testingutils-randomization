namespace Jds.TestingUtils.Randomization;

public static class SelectionRandomizationSourceExtensions
{
  /// <summary>
  ///   Retrieves a random item from <paramref name="items" />, assumed to be non-empty.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="items">A collection of items from which an item is retrieved.</param>
  /// <typeparam name="T">A collection item type.</typeparam>
  /// <returns>A randomly-selected item from <paramref name="items" />.</returns>
  /// <exception cref="ArgumentException">Thrown if <paramref name="items" /> is empty.</exception>
  public static T RandomListItem<T>(this IRandomizationSource randomizationSource, IReadOnlyList<T> items)
  {
    if (!items.Any())
    {
      throw new ArgumentException("Collection is empty", nameof(items));
    }

    return items[randomizationSource.NextIntInRange(0, items.Count)];
  }

  /// <summary>
  ///   Retrieves a random value from <typeparamref name="TEnum" />, assumed to be non-empty.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <typeparam name="TEnum">An enumeration.</typeparam>
  /// <returns>A randomly-selected value from <typeparamref name="TEnum" />.</returns>
  /// <exception cref="ArgumentException">Thrown if <typeparamref name="TEnum" /> is empty.</exception>
  public static TEnum RandomEnumValue<TEnum>(this IRandomizationSource randomizationSource)
    where TEnum : struct, Enum
  {
    return randomizationSource.RandomListItem(Enum.GetValues<TEnum>());
  }
}
