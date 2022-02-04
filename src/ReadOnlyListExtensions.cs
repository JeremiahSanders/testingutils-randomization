namespace Jds.TestingUtils.Randomization;

public static class ReadOnlyListExtensions
{
  /// <summary>
  ///   Retrieves a random item from <paramref name="items" />, assumed to be non-empty.
  /// </summary>
  /// <remarks>Uses <see cref="Randomizer.Shared" /> to provide randomization.</remarks>
  /// <param name="items">A collection of items from which an item is retrieved.</param>
  /// <typeparam name="T">A collection item type.</typeparam>
  /// <returns>A randomly-selected item from <paramref name="items" />.</returns>
  /// <exception cref="ArgumentException">Thrown if <paramref name="items" /> is empty.</exception>
  public static T GetRandomItem<T>(this IReadOnlyList<T> items)
  {
    return Randomizer.Shared.RandomListItem(items);
  }

  /// <inheritdoc cref="SelectionRandomizationSourceExtensions.RandomListItem{T}" />
  public static T GetRandomItem<T>(this IReadOnlyList<T> items, IRandomizationSource randomizationSource)
  {
    return randomizationSource.RandomListItem(items);
  }

  /// <summary>
  ///   Generates a random <see cref="string" /> from <paramref name="strings" />, assumed to be non-empty.
  /// </summary>
  /// <remarks>Uses <see cref="Randomizer.Shared" /> to provide randomization.</remarks>
  /// <param name="strings">A collection of <see cref="string" /> from which items are retrieved.</param>
  /// <param name="length">A string length.</param>
  /// <returns>A randomly-selected item from <paramref name="strings" />.</returns>
  /// <exception cref="ArgumentException">Thrown if <paramref name="strings" /> is empty.</exception>
  public static string GenerateRandomString(this IReadOnlyList<string> strings, int length)
  {
    return Randomizer.Shared.RandomString(length, strings);
  }

  /// <inheritdoc
  ///   cref="StringRandomizationSourceExtensions.RandomString(Jds.TestingUtils.Randomization.IRandomizationSource,int,System.Collections.Generic.IReadOnlyList{string})" />
  public static string GenerateRandomString(this IReadOnlyList<string> strings,
    IRandomizationSource randomizationSource,
    int length
  )
  {
    return randomizationSource.RandomString(length, strings);
  }

  /// <summary>
  ///   Generates a random <see cref="string" /> from <paramref name="chars" />, assumed to be non-empty.
  /// </summary>
  /// <remarks>Uses <see cref="Randomizer.Shared" /> to provide randomization.</remarks>
  /// <param name="chars">A collection of <see cref="char" /> from which items are retrieved.</param>
  /// <param name="length">A string length.</param>
  /// <returns>A randomly-selected item from <paramref name="chars" />.</returns>
  /// <exception cref="ArgumentException">Thrown if <paramref name="chars" /> is empty.</exception>
  public static string GenerateRandomString(this IReadOnlyList<char> chars, int length)
  {
    return Randomizer.Shared.RandomString(length, chars);
  }

  /// <inheritdoc
  ///   cref="StringRandomizationSourceExtensions.RandomString(Jds.TestingUtils.Randomization.IRandomizationSource,int,System.Collections.Generic.IReadOnlyList{char})" />
  public static string GenerateRandomString(this IReadOnlyList<char> chars,
    IRandomizationSource randomizationSource,
    int length
  )
  {
    return randomizationSource.RandomString(length, chars);
  }

  /// <inheritdoc
  ///   cref="SelectionRandomizationSourceExtensions.WeightedRandomListItem{T}(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyList{(T item, double weight)})" />
  public static T GetWeightedRandomItem<T>(this IReadOnlyList<(T Key, double Weight)> weightedItems,
    IRandomizationSource randomizationSource)
  {
    return randomizationSource.WeightedRandomListItem(weightedItems);
  }

  /// <inheritdoc
  ///   cref="SelectionRandomizationSourceExtensions.WeightedRandomListItem{T}(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyList{(T item, double weight)})" />
  public static T GetWeightedRandomItem<T>(this IReadOnlyList<(T Key, double Weight)> weightedItems)
  {
    return Randomizer.Shared.WeightedRandomListItem(weightedItems);
  }

  /// <inheritdoc
  ///   cref="SelectionRandomizationSourceExtensions.WeightedRandomListItem{T}(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyList{(T item, int weight)})" />
  public static T GetWeightedRandomItem<T>(this IReadOnlyList<(T Key, int Weight)> weightedItems,
    IRandomizationSource randomizationSource)
  {
    return randomizationSource.WeightedRandomListItem(weightedItems);
  }

  /// <inheritdoc
  ///   cref="SelectionRandomizationSourceExtensions.WeightedRandomListItem{T}(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyList{(T item, int weight)})" />
  public static T GetWeightedRandomItem<T>(this IReadOnlyList<(T Key, int Weight)> weightedItems)
  {
    return Randomizer.Shared.WeightedRandomListItem(weightedItems);
  }
}
