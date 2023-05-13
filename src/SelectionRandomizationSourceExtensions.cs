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
    var enumValues = Enum.GetValues<TEnum>();

    if (!enumValues.Any())
    {
      throw new ArgumentException($"Enum {typeof(TEnum).Name} has no values.");
    }

    return randomizationSource.RandomListItem(enumValues);
  }

  /// <summary>
  ///   Retrieves a random value (excluding <paramref name="except" />) from <typeparamref name="TEnum" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="except"><typeparamref name="TEnum" /> value(s) to exclude.</param>
  /// <typeparam name="TEnum">An enumeration.</typeparam>
  /// <returns>A randomly-selected value from <typeparamref name="TEnum" />.</returns>
  /// <exception cref="ArgumentException">Thrown if <typeparamref name="TEnum" /> is empty.</exception>
  /// <exception cref="InvalidOperationException">
  ///   Thrown if no values of <typeparamref name="TEnum" /> remain after
  ///   <paramref name="except" /> are excluded.
  /// </exception>
  public static TEnum RandomEnumValue<TEnum>(this IRandomizationSource randomizationSource, params TEnum[] except)
    where TEnum : struct, Enum
  {
    var enumValues = Enum.GetValues<TEnum>();

    if (!enumValues.Any())
    {
      throw new ArgumentException($"Enum {typeof(TEnum).Name} has no values.");
    }

    var values = enumValues.Where(element => !except.Contains(element)).ToList();

    if (!values.Any())
    {
      throw new InvalidOperationException("Cannot select random enum value. No enum values remain after filtering.");
    }

    return randomizationSource.RandomListItem(values);
  }

  /// <summary>
  ///   Retrieves a weighted random item from <paramref name="weightedItems" />, assumed to be non-empty.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="weightedItems">A collection of weighted items from which an item is retrieved.</param>
  /// <typeparam name="T">A collection item type.</typeparam>
  /// <returns>A randomly selected <typeparamref name="T" />.</returns>
  /// <exception cref="ArgumentException">Thrown when <paramref name="weightedItems" /> is empty.</exception>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when any item's weight is below 0 or if the sum is above Double.MaxValue.
  /// </exception>
  public static T WeightedRandomListItem<T>(this IRandomizationSource randomizationSource,
    IReadOnlyList<(T Item, double Weight)> weightedItems)
  {
    if (!weightedItems.Any())
    {
      throw new ArgumentException("Item collection is empty.", nameof(weightedItems));
    }

    var totalWeight = weightedItems.Sum(choice =>
    {
      if (choice.Weight < double.Epsilon)
      {
        throw new ArgumentOutOfRangeException(nameof(weightedItems), "Weights must be > 0");
      }

      return choice.Weight;
    });

    if (double.IsInfinity(totalWeight) || double.IsNegative(totalWeight))
    {
      throw new ArgumentOutOfRangeException(nameof(weightedItems), "Weight sum must be > 0 and < Double.MaxValue.");
    }

    var randomValue = randomizationSource.NextDouble() * totalWeight;

    (double currentRangeEnd, bool selected, T? value) MakeNext(
      (double currentRangeEnd, bool selected, T? value) state,
      (T item, double weight) choice)
    {
      if (state.selected)
      {
        return state;
      }

      var (item, weight) = choice;
      var newRangeEnd = state.currentRangeEnd + weight;
      var inRange = newRangeEnd >= randomValue;
      return (currentRangeEnd: newRangeEnd, selected: inRange, value: inRange ? item : default(T));
    }

    return weightedItems.OrderBy(choice => choice.Weight)
      .Aggregate((currentRangeEnd: 0d, selected: false, value: default(T?)), MakeNext)
      .value ?? weightedItems[0].Item;
  }

  /// <summary>
  ///   Retrieves a weighted random item from <paramref name="weightedItems" />, assumed to be non-empty.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="weightedItems">A collection of weighted items from which an item is retrieved.</param>
  /// <typeparam name="T">A collection item type.</typeparam>
  /// <returns>A randomly selected <typeparamref name="T" />.</returns>
  /// <exception cref="ArgumentException">Thrown when <paramref name="weightedItems" /> is empty.</exception>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when any item's weight is below 0 or if the sum is above Double.MaxValue.
  /// </exception>
  /// <exception cref="OverflowException">Thrown when item weights' sum exceeds <see cref="int.MaxValue" />.</exception>
  public static T WeightedRandomListItem<T>(this IRandomizationSource randomizationSource,
    IReadOnlyList<(T Item, int Weight)> weightedItems)
  {
    if (!weightedItems.Any())
    {
      throw new ArgumentException("Item collection is empty.", nameof(weightedItems));
    }

    var totalWeight = weightedItems.Sum(choice =>
    {
      if (choice.Weight < 1)
      {
        throw new ArgumentOutOfRangeException(nameof(weightedItems), "Weights must be > 0");
      }

      return choice.Weight;
    });

    var randomValue = Convert.ToInt32(Math.Floor(randomizationSource.NextDouble() * totalWeight));

    (int currentRangeEnd, bool selected, T? value) MakeNext(
      (int currentRangeEnd, bool selected, T? value) state,
      (T item, int weight) choice)
    {
      if (state.selected)
      {
        return state;
      }

      var (item, weight) = choice;
      var newRangeEnd = state.currentRangeEnd + weight;
      var inRange = newRangeEnd >= randomValue;
      return (currentRangeEnd: newRangeEnd, selected: inRange, value: inRange ? item : default(T));
    }

    return weightedItems.OrderBy(choice => choice.Weight)
      .Aggregate((currentRangeEnd: 0, selected: false, value: default(T?)), MakeNext)
      .value ?? weightedItems[0].Item;
  }

  /// <summary>
  ///   Retrieves a weighted random key from <paramref name="weightedKeys" />, assumed to be non-empty.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="weightedKeys">A collection of weighted items from which an item is retrieved.</param>
  /// <typeparam name="T">A collection item type.</typeparam>
  /// <returns>A randomly selected <typeparamref name="T" />.</returns>
  /// <exception cref="ArgumentException">Thrown when <paramref name="weightedKeys" /> is empty.</exception>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when any item's weight is below 0 or if the sum is above
  ///   Double.MaxValue.
  /// </exception>
  public static T WeightedRandomKey<T>(this IRandomizationSource randomizationSource,
    IReadOnlyDictionary<T, double> weightedKeys)
  {
    return randomizationSource.WeightedRandomListItem(
      weightedKeys.Select(keyValuePair =>
          (keyValuePair.Key, keyValuePair.Value))
        .ToList()
    );
  }

  /// <summary>
  ///   Retrieves a weighted random key from <paramref name="weightedKeys" />, assumed to be non-empty.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="weightedKeys">A collection of weighted items from which an item is retrieved.</param>
  /// <typeparam name="T">A collection item type.</typeparam>
  /// <returns>A randomly selected <typeparamref name="T" />.</returns>
  /// <exception cref="ArgumentException">Thrown when <paramref name="weightedKeys" /> is empty.</exception>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when any item's weight is below 0 or if the sum is above
  ///   Double.MaxValue.
  /// </exception>
  public static T WeightedRandomKey<T>(this IRandomizationSource randomizationSource,
    IReadOnlyDictionary<T, int> weightedKeys)
  {
    return randomizationSource.WeightedRandomListItem(
      weightedKeys.Select(keyValuePair =>
          (keyValuePair.Key, keyValuePair.Value))
        .ToList()
    );
  }
}
