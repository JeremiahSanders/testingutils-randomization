# DomainRandomizationSourceExtensions.DomainName method (1 of 2)

Generates a pseudo-random domain name.

```csharp
public static string DomainName(this IRandomizationSource randomizationSource, int domainLength)
```

| parameter | description |
| --- | --- |
| randomizationSource | A [`IRandomizationSource`](../IRandomizationSource.md) providing values. |
| domainLength | A desired domain name length. |

## Return Value

A pseudo-random domain name which conforms to RFC-1035.

## Remarks

See https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.4

```csharp
<domain> ::= <subdomain> | " "

<subdomain> ::= <label> | <subdomain> "." <label>

<label> ::= <letter> [ [ <ldh-str> ] <let-dig> ]

<ldh-str> ::= <let-dig-hyp> | <let-dig-hyp> <ldh-str>

<let-dig-hyp> ::= <let-dig> | "-"

<let-dig> ::= <letter> | <digit>

<letter> ::= any one of the 52 alphabetic characters A through Z in
upper case and a through z in lower case

<digit> ::= any one of the ten digits 0 through 9
```

Source https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.1

## See Also

* interface [IRandomizationSource](../IRandomizationSource.md)
* class [DomainRandomizationSourceExtensions](../DomainRandomizationSourceExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

---

# DomainRandomizationSourceExtensions.DomainName method (2 of 2)

Generates a pseudo-random domain name.

```csharp
public static string DomainName(this IRandomizationSource randomizationSource, 
    IReadOnlyList<int> domainLabelLengths)
```

| parameter | description |
| --- | --- |
| randomizationSource | A [`IRandomizationSource`](../IRandomizationSource.md) providing values. |
| domainLabelLengths | A list of domain label segment lengths. |

## Return Value

A pseudo-random domain name which conforms to RFC-1035.

## Remarks

See https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.4

```csharp
<domain> ::= <subdomain> | " "

<subdomain> ::= <label> | <subdomain> "." <label>

<label> ::= <letter> [ [ <ldh-str> ] <let-dig> ]

<ldh-str> ::= <let-dig-hyp> | <let-dig-hyp> <ldh-str>

<let-dig-hyp> ::= <let-dig> | "-"

<let-dig> ::= <letter> | <digit>

<letter> ::= any one of the 52 alphabetic characters A through Z in
upper case and a through z in lower case

<digit> ::= any one of the ten digits 0 through 9
```

Source https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.1

## See Also

* interface [IRandomizationSource](../IRandomizationSource.md)
* class [DomainRandomizationSourceExtensions](../DomainRandomizationSourceExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

<!-- DO NOT EDIT: generated by xmldocmd for TestingUtils.Randomization.dll -->