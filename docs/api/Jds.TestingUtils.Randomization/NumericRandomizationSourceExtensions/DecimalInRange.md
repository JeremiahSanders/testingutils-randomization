# NumericRandomizationSourceExtensions.DecimalInRange method

Gets a pseudo-random Decimal between *minInclusive* and *maxExclusive*.

```csharp
public static decimal DecimalInRange(this IRandomizationSource randomizationSource, 
    decimal minInclusive, decimal maxExclusive)
```

| parameter | description |
| --- | --- |
| randomizationSource | A [`IRandomizationSource`](../IRandomizationSource.md) providing values. |
| minInclusive | A minimum limit. |
| maxExclusive | An exclusive upper limit. |

## Return Value

A Decimal between *minInclusive* and *maxExclusive*.

## See Also

* interface [IRandomizationSource](../IRandomizationSource.md)
* class [NumericRandomizationSourceExtensions](../NumericRandomizationSourceExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

<!-- DO NOT EDIT: generated by xmldocmd for TestingUtils.Randomization.dll -->
