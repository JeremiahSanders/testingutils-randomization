# SelectionRandomizationSourceExtensions.WeightedRandomListItem&lt;T&gt; method (1 of 2)

Retrieves a weighted random item from *weightedItems*, assumed to be non-empty.

```csharp
public static T WeightedRandomListItem<T>(this IRandomizationSource randomizationSource, 
    IReadOnlyList<(T Item, double Weight)> weightedItems)
```

| parameter | description |
| --- | --- |
| T | A collection item type. |
| randomizationSource | A [`IRandomizationSource`](../IRandomizationSource.md) providing values. |
| weightedItems | A collection of weighted items from which an item is retrieved. |

## Return Value

A randomly selected *T*.

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown when *weightedItems* is empty. |
| ArgumentOutOfRangeException | Thrown when any item's weight is below 0 or if the sum is above Double.MaxValue. |

## See Also

* interface [IRandomizationSource](../IRandomizationSource.md)
* class [SelectionRandomizationSourceExtensions](../SelectionRandomizationSourceExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

---

# SelectionRandomizationSourceExtensions.WeightedRandomListItem&lt;T&gt; method (2 of 2)

Retrieves a weighted random item from *weightedItems*, assumed to be non-empty.

```csharp
public static T WeightedRandomListItem<T>(this IRandomizationSource randomizationSource, 
    IReadOnlyList<(T Item, int Weight)> weightedItems)
```

| parameter | description |
| --- | --- |
| T | A collection item type. |
| randomizationSource | A [`IRandomizationSource`](../IRandomizationSource.md) providing values. |
| weightedItems | A collection of weighted items from which an item is retrieved. |

## Return Value

A randomly selected *T*.

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown when *weightedItems* is empty. |
| ArgumentOutOfRangeException | Thrown when any item's weight is below 0 or if the sum is above Double.MaxValue. |
| OverflowException | Thrown when item weights' sum exceeds MaxValue. |

## See Also

* interface [IRandomizationSource](../IRandomizationSource.md)
* class [SelectionRandomizationSourceExtensions](../SelectionRandomizationSourceExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

<!-- DO NOT EDIT: generated by xmldocmd for TestingUtils.Randomization.dll -->
