# StatefulRandomizationSourceMonadExtensions.WithState&lt;TState&gt; method

Creates a [`IStatefulRandomizationSource`](../IStatefulRandomizationSource-1.md) from this [`IRandomizationSource`](../IRandomizationSource.md) having an initial [`State`](../IStatefulRandomizationSource-1/State.md) of *state*.

```csharp
public static IStatefulRandomizationSource<TState> WithState<TState>(
    this IRandomizationSource source, TState state)
```

| parameter | description |
| --- | --- |
| TState | A state object. |
| source | This [`IRandomizationSource`](../IRandomizationSource.md). |
| state | An initial state value. |

## Return Value

A [`IStatefulRandomizationSource`](../IStatefulRandomizationSource-1.md).

## See Also

* interface [IStatefulRandomizationSource&lt;TState&gt;](../IStatefulRandomizationSource-1.md)
* interface [IRandomizationSource](../IRandomizationSource.md)
* class [StatefulRandomizationSourceMonadExtensions](../StatefulRandomizationSourceMonadExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

<!-- DO NOT EDIT: generated by xmldocmd for TestingUtils.Randomization.dll -->