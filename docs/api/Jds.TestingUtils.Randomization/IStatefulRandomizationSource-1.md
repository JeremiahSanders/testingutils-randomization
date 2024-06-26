# IStatefulRandomizationSource&lt;TState&gt; interface

A [`IRandomizationSource`](./IRandomizationSource.md) which contains state.

```csharp
public interface IStatefulRandomizationSource<out TState> : IRandomizationSource
```

| parameter | description |
| --- | --- |
| TState | A state type. This object generally contains parameters or objects which will be used by extension methods (which you define). |

## Members

| name | description |
| --- | --- |
| [State](IStatefulRandomizationSource-1/State.md) { get; } | Gets the current randomization source state. |

## Remarks

A stateful randomization source is useful for creating a randomization domain-specific language which is often implemented using "fluent" syntax (i.e., extension methods).

## See Also

* interface [IRandomizationSource](./IRandomizationSource.md)
* namespace [Jds.TestingUtils.Randomization](../TestingUtils.Randomization.md)
* [IStatefulRandomizationSource.cs](https://github.com/JeremiahSanders/testingutils-randomization/tree/main/src/IStatefulRandomizationSource.cs)

<!-- DO NOT EDIT: generated by xmldocmd for TestingUtils.Randomization.dll -->
