# StatefulRandomizationSourceMonadExtensions.Map&lt;TStateA,TStateB&gt; method

Creates a new [`IStatefulRandomizationSource`](../IStatefulRandomizationSource-1.md) having updated state generated by *mapper*.

```csharp
public static IStatefulRandomizationSource<TStateB> Map<TStateA, TStateB>(
    this IStatefulRandomizationSource<TStateA> statefulSource, Func<TStateA, TStateB> mapper)
```

| parameter | description |
| --- | --- |
| TStateA | A current state type. |
| TStateB | A next state type. |
| statefulSource | A stateful randomization source. |
| mapper | A method which accepts the current state and returns the new state. |

## Return Value

A new stateful randomization source.

## See Also

* interface [IStatefulRandomizationSource&lt;TState&gt;](../IStatefulRandomizationSource-1.md)
* class [StatefulRandomizationSourceMonadExtensions](../StatefulRandomizationSourceMonadExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

<!-- DO NOT EDIT: generated by xmldocmd for TestingUtils.Randomization.dll -->