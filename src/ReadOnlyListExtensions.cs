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
}
