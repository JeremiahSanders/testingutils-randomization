# UrlRandomizationSourceExtensions.RandomUrl method

Generates a pseudo-random URL.

```csharp
public static string RandomUrl(this IRandomizationSource randomizationSource, int hostLength, 
    int pathLength = 0, int queryLength = 0, int fragmentLength = 0, string scheme = "https", 
    int? port = null)
```

| parameter | description |
| --- | --- |
| randomizationSource | A [`IRandomizationSource`](../IRandomizationSource.md) providing values. |
| hostLength | A URL host length. |
| queryLength | A URL query length. |
| port | A port. |
| fragmentLength | A fragment length. |
| scheme | A communication scheme, e.g., http, https, ftp. |
| pathLength | A URL path length. |

## Return Value

A randomly-generated Uri.

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if *scheme* is empty. |
| ArgumentOutOfRangeException | Thrown if *hostLength* is insufficient. |

## Remarks

Host: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.2.2

Port: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.2.3

Path: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.3

Query: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.4

Fragment: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.5

## See Also

* interface [IRandomizationSource](../IRandomizationSource.md)
* class [UrlRandomizationSourceExtensions](../UrlRandomizationSourceExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

<!-- DO NOT EDIT: generated by xmldocmd for TestingUtils.Randomization.dll -->
