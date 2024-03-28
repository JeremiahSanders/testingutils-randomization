namespace Jds.TestingUtils.Randomization;

public static class MarkovStringRandomizationSourceExtensions
{
  /// <summary>
  ///   Generates a <see cref="string" />, derived from the patterns in <paramref name="sources" />, which is
  ///   no longer than <paramref name="maxLength" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="sources">Example strings.</param>
  /// <param name="maxLength">A desired maximum length. Defaults to the longest sequence in <see cref="sources" />.</param>
  /// <param name="chainLength">
  ///   Count of <see cref="char" /> items which are used to select the next <see cref="char" /> in the
  ///   <see cref="string" />.
  /// </param>
  /// <returns>
  ///   A <see cref="string" />, derived from the patterns in <paramref name="sources" />, which is no longer
  ///   than <paramref name="maxLength" />.
  /// </returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="chainLength" /> is less than 1.</exception>
  /// <exception cref="ArgumentException">
  ///   Thrown when <paramref name="sources" /> is empty, if any items in
  ///   <paramref name="sources" /> are empty, or if <paramref name="chainLength" /> exceeds the longest sequence in
  ///   <paramref name="sources" />.
  /// </exception>
  public static string GenerateRandomMarkov(this IRandomizationSource randomizationSource,
    IEnumerable<string> sources, int? maxLength = null, int chainLength = 1)
  {
    return string.Concat(
      randomizationSource.GenerateRandomMarkov(
        sources.Select(source => source.ToCharArray()).ToArray(), maxLength, chainLength)
    );
  }

  /// <summary>
  ///   Generates a sequence of <typeparamref name="T" />, derived from the patterns in <paramref name="sources" />, which is
  ///   no longer than <paramref name="maxLength" />.
  /// </summary>
  /// <typeparam name="T">An item.</typeparam>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="sources">Example sequences of <typeparamref name="T" />.</param>
  /// <param name="maxLength">A desired maximum length. Defaults to the longest sequence in <see cref="sources" />.</param>
  /// <param name="chainLength">
  ///   Count of sequential <typeparamref name="T" /> items which are used to select the next item.
  /// </param>
  /// <returns>
  ///   A sequence of <typeparamref name="T" />, derived from the patterns in <paramref name="sources" />, which is no longer
  ///   than <paramref name="maxLength" />.
  /// </returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="chainLength" /> is less than 1.</exception>
  /// <exception cref="ArgumentException">
  ///   Thrown when <paramref name="sources" /> is empty, if any items in
  ///   <paramref name="sources" /> are empty, or if <paramref name="chainLength" /> exceeds the longest sequence in
  ///   <paramref name="sources" />.
  /// </exception>
  public static IReadOnlyList<T> GenerateRandomMarkov<T>(this IRandomizationSource randomizationSource,
    IReadOnlyCollection<IReadOnlyList<T>> sources,
    int? maxLength = null,
    int chainLength = 1) where T : notnull, IEquatable<T>
  {
    if (chainLength < 1)
    {
      throw new ArgumentOutOfRangeException(nameof(chainLength),
        $"{nameof(chainLength)} must be > 0.");
    }

    if (!sources.Any() || sources.Any(source => !source.Any()))
    {
      throw new ArgumentException($"Items in {nameof(sources)} cannot be empty.", nameof(sources));
    }

    var actualMaxLength = maxLength ?? sources.Max(list => list.Count);

    if (chainLength > actualMaxLength)
    {
      throw new ArgumentException($"{nameof(chainLength)} may not exceed {nameof(maxLength)}.");
    }

    return randomizationSource.GenerateRandomMarkov(
      GetMarkovProbability(sources, chainLength),
      actualMaxLength
    );
  }

  /// <summary>
  ///   Creates a function which generates <see cref="string" /> Markov Chains.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="sources">Example strings which will train the function.</param>
  /// <param name="chainLength">
  ///   Desired length of <see cref="char" /> sequences, obtained from <paramref name="sources" />, which will train the
  ///   'next
  ///   item' selections made by the created function.
  /// </param>
  /// ///
  /// <returns>
  ///   A function which accepts a maximum length and returns a string, derived from the <see cref="char" /> sequences in
  ///   each <see cref="string" /> in <paramref name="sources" />.
  /// </returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="chainLength" /> is less than 1.</exception>
  /// <exception cref="ArgumentException">
  ///   Thrown when <paramref name="sources" /> is empty, if any items in
  ///   <paramref name="sources" /> are empty, or if <paramref name="chainLength" /> exceeds the longest sequence in
  ///   <paramref name="sources" />.
  /// </exception>
  public static Func<int, string> CreateMarkovGenerator(this IRandomizationSource randomizationSource,
    IEnumerable<string> sources, int chainLength = 1)
  {
    var charListGenerator = randomizationSource.CreateMarkovGenerator(
      sources.Select(source => source.ToCharArray()).ToList(),
      chainLength
    );

    return Generator;

    string Generator(int maxLength)
    {
      return string.Concat(charListGenerator(maxLength));
    }
  }

  /// <summary>
  ///   Creates a function which generates Markov Chains.
  /// </summary>
  /// <typeparam name="T">An item.</typeparam>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="sources">Example sequences of <typeparamref name="T" /> which will train the function.</param>
  /// <param name="chainLength">
  ///   Desired length of sequences, obtained from <paramref name="sources" />, which will train the 'next
  ///   item' selections made by the created function.
  /// </param>
  /// <returns>
  ///   A function which accepts a maximum length and returns a Markov Chain of <typeparamref name="T" />, derived from the
  ///   item sequences in <paramref name="sources" />.
  /// </returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="chainLength" /> is less than 1.</exception>
  /// <exception cref="ArgumentException">
  ///   Thrown when <paramref name="sources" /> is empty, if any items in
  ///   <paramref name="sources" /> are empty, or if <paramref name="chainLength" /> exceeds the longest sequence in
  ///   <paramref name="sources" />.
  /// </exception>
  public static Func<int, IReadOnlyList<T>> CreateMarkovGenerator<T>(this IRandomizationSource randomizationSource,
    IReadOnlyCollection<IReadOnlyList<T>> sources, int chainLength = 1) where T : notnull, IEquatable<T>
  {
    if (chainLength < 1)
    {
      throw new ArgumentOutOfRangeException(nameof(chainLength),
        $"{nameof(chainLength)} must be > 0.");
    }

    if (!sources.Any() || sources.Any(source => !source.Any()))
    {
      throw new ArgumentException($"Items in {nameof(sources)} cannot be empty.", nameof(sources));
    }

    var largestSourceCount = sources.Max(list => list.Count);

    if (chainLength > largestSourceCount)
    {
      throw new ArgumentException($"{nameof(chainLength)} may not exceed largest item in {nameof(sources)}.");
    }

    var markovProbability = GetMarkovProbability(sources, chainLength);

    IReadOnlyList<T> MarkovGenerator(int maxLength)
    {
      return randomizationSource.GenerateRandomMarkov(markovProbability, maxLength);
    }

    return MarkovGenerator;
  }

  internal static IReadOnlyList<T> GenerateRandomMarkov<T>(this IRandomizationSource randomizationSource,
    IReadOnlyDictionary<IReadOnlyList<T>, MarkovResult<T>> markovProbability, int maxLength)
    where T : notnull, IEquatable<T>
  {
    var firstItems = markovProbability.Select(kvp => (kvp.Key, Weight: kvp.Value.FirstItemCount))
      .Where(tuple => tuple.Weight > 0).ToArray();

    var nextItemLookup = markovProbability.Select(keyValuePair =>
        new KeyValuePair<IReadOnlyList<T>, Dictionary<OptionalValue<T>, int>>(
          keyValuePair.Key,
          keyValuePair.Value.GetNextItems()
        )
      )
      .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    var results = new List<T>();

    var firstItem = firstItems.GetWeightedRandomItem(randomizationSource);
    results.AddRange(firstItem);

    var previousItem = firstItem;
    var finished = false;
    var defaultKvp = new KeyValuePair<IReadOnlyList<T>, Dictionary<OptionalValue<T>, int>>();
    while (!finished)
    {
      var loopPreviousItem = previousItem; // Addresses 'captured variable is modified in outer scope
      var possiblePreviousItemResults =
        nextItemLookup.FirstOrDefault(kvp => kvp.Key.SequenceEqual(loopPreviousItem), defaultKvp);

      var possibleNext = !Equals(possiblePreviousItemResults, defaultKvp) && results.Count < maxLength
        ? possiblePreviousItemResults.Value.GetWeightedRandomKey(randomizationSource)
        : OptionalValue<T>.None();

      if (possibleNext.IsSome)
      {
        results.Add(possibleNext.Value!);
        previousItem = previousItem.Skip(1).Append(possibleNext.Value).ToList()!;
      }
      else
      {
        finished = true;
      }
    }

    return results;
  }

  private static int GetGroupedItemIteratorCount<T>(IReadOnlyList<T> items, int itemsPerGroup)
  {
    return items.Count <= itemsPerGroup ? 1 : items.Count - itemsPerGroup + 1;
  }

  /// <summary>
  ///   Iterates through <paramref name="items" /> in groups of <paramref name="itemsPerGroup" />.
  /// </summary>
  /// <typeparam name="T">An item type.</typeparam>
  /// <param name="items">Items to iterate.</param>
  /// <param name="itemsPerGroup">Count of items in each group.</param>
  /// <returns>An <see cref="IEnumerable{T}" /> of grouped items.</returns>
  internal static IEnumerable<(IReadOnlyList<T> Items, int Index)> IterateGroupedItems<T>(IReadOnlyList<T> items,
    int itemsPerGroup)
  {
    if (items.Count <= itemsPerGroup)
    {
      yield return (items, 0);
    }
    else
    {
      var itemsCountByOrder = GetGroupedItemIteratorCount(items, itemsPerGroup);
      for (var i = 0; i < itemsCountByOrder; i++)
      {
        yield return (items.Skip(i).Take(itemsPerGroup).ToList(), i);
      }
    }
  }

  /// <summary>
  ///   Analyze <paramref name="sources" /> and create <see cref="MarkovResult{T}" /> for item sequences of
  ///   <paramref name="itemsPerGroup" />.
  /// </summary>
  /// <typeparam name="T">An item type.</typeparam>
  /// <param name="sources">A collection of reference sequences.</param>
  /// <param name="itemsPerGroup">Count of items in each probability chain.</param>
  /// <returns>A Markov Chain model, for each <paramref name="itemsPerGroup" /> in <paramref name="sources" /> elements.</returns>
  internal static IReadOnlyDictionary<IReadOnlyList<T>, MarkovResult<T>> GetMarkovProbability<T>(
    IReadOnlyCollection<IReadOnlyList<T>> sources,
    int itemsPerGroup
  ) where T : notnull, IEquatable<T>
  {
    var state = new Dictionary<IReadOnlyList<T>, MarkovResult<T>>();
    var defaultKvp = default(KeyValuePair<IReadOnlyList<T>, MarkovResult<T>>);
    var finalState = sources.Select(source => GetMarkovProbabilities(source, itemsPerGroup))
      .Aggregate(state, (lastState, result) =>
      {
        foreach (var keyValuePair in result)
        {
          var possibleExistingKey = lastState.FirstOrDefault(kvp => kvp.Key.SequenceEqual(keyValuePair.Key));
          var referenceKey = !possibleExistingKey.Equals(defaultKvp) ? possibleExistingKey.Key : keyValuePair.Key;
          if (!lastState.ContainsKey(referenceKey))
          {
            lastState[referenceKey] = new MarkovResult<T>();
          }

          var existingState = lastState[referenceKey];
          lastState[referenceKey] = new MarkovResult<T>
          {
            FirstItemCount = existingState.FirstItemCount + keyValuePair.Value.FirstItemCount,
            LastItemCount = existingState.LastItemCount + keyValuePair.Value.LastItemCount,
            NextItemCounts = MergeDictionaries(existingState.NextItemCounts, keyValuePair.Value.NextItemCounts),
            Occurrences = existingState.Occurrences + keyValuePair.Value.Occurrences
          };
        }

        return lastState;
      });

    return finalState;

    static Dictionary<IReadOnlyList<T>, MarkovResult<T>> GetMarkovProbabilities(IReadOnlyList<T> items, int order)
    {
      var groupedItemCount = GetGroupedItemIteratorCount(items, order);

      var state = new Dictionary<IReadOnlyList<T>, MarkovResult<T>>();

      foreach (var (item, index) in IterateGroupedItems(items, order))
      {
        if (!state.ContainsKey(item))
        {
          state[item] = new MarkovResult<T>();
        }

        state[item] = state[item] with {Occurrences = state[item].Occurrences + 1};

        if (index == 0)
        {
          state[item] = state[item] with {FirstItemCount = state[item].FirstItemCount + 1};
        }

        if (index + 1 == groupedItemCount)
        {
          state[item] = state[item] with {LastItemCount = state[item].LastItemCount + 1};
        }

        var getNextItem = TryGetNextItem(index);
        if (getNextItem.IsSome)
        {
          var nextItem = getNextItem.Value!;
          var itemState = state[item];
          itemState.NextItemCounts.TryAdd(nextItem, 0); // Ensure exists

          itemState.NextItemCounts[nextItem] += 1;
        }
      }

      return state;

      OptionalValue<T> TryGetNextItem(int index)
      {
        return index + order < items.Count
          ? OptionalValue<T>.Of(items[index + order])
          : OptionalValue<T>.None();
      }
    }

    static Dictionary<T, int> MergeDictionaries(IDictionary<T, int> a, IDictionary<T, int> b)
    {
      var dictionary = new Dictionary<T, int>(a);
      foreach (var keyValuePair in b)
      {
        dictionary.TryAdd(keyValuePair.Key, 0); // Ensure exists

        dictionary[keyValuePair.Key] += keyValuePair.Value;
      }

      return dictionary;
    }
  }

  /// <summary>
  ///   A Markov Chain known state transition occurrence model.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  internal record MarkovResult<T> where T : notnull
  {
    /// <summary>
    ///   Gets a value indicating the count of times item <typeparamref name="T" /> was the first item in a list.
    /// </summary>
    public int FirstItemCount { get; init; }

    /// <summary>
    ///   Gets a dictionary of relative <typeparamref name="T" /> weights, each a count of times a given value was the next
    ///   item in sequence.
    /// </summary>
    public IDictionary<T, int> NextItemCounts { get; init; } = new Dictionary<T, int>();

    /// <summary>
    ///   Gets a value indicating the count of times item <typeparamref name="T" /> was the last item in a list.
    /// </summary>
    public int LastItemCount { get; init; }

    /// <summary>
    ///   Gets a value indicating the count of types item <typeparamref name="T" /> occurred.
    /// </summary>
    public int Occurrences { get; init; }

    /// <summary>
    ///   Gets a new <see cref="Dictionary{TKey,TValue}" /> using <see cref="NextItemCounts" /> and
    ///   <see cref="LastItemCount" />.
    /// </summary>
    /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
    public Dictionary<OptionalValue<T>, int> GetNextItems()
    {
      return NextItemCounts
        .Select(kvp => new KeyValuePair<OptionalValue<T>, int>(OptionalValue<T>.Of(kvp.Key), kvp.Value))
        .Append(new KeyValuePair<OptionalValue<T>, int>(OptionalValue<T>.None(), LastItemCount))
        .Where(kvp => kvp.Value > 0)
        .ToDictionary(innerKvp => innerKvp.Key, innerKvp => innerKvp.Value);
    }
  }
}
