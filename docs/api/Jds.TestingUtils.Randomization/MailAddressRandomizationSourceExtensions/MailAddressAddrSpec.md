# MailAddressRandomizationSourceExtensions.MailAddressAddrSpec method (1 of 2)

Generates a pseudo-random mail address compliant with `addr-spec` of RFC-2822.

```csharp
public static string MailAddressAddrSpec(this IRandomizationSource randomizationSource, 
    (int LocalPartLength, int DomainLength) componentLengths)
```

| parameter | description |
| --- | --- |
| randomizationSource | A [`IRandomizationSource`](../IRandomizationSource.md) providing values. |
| componentLengths |  |

## Return Value

A pseudo-random mail address compliant with `addr-spec` of RFC-2822.

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentOutOfRangeException | Thrown when the local part length is less than 1 or greater than 64, when the domain length is less than 1 or greater than 255, or when the sum of local and domain lengths is greater than 253. (Max mail address length, including @ sign, must be 254 characters or less.) |

## Remarks

While values generated should be compliant with `addr-spec` defined in RFC-2822 section 3.4.1, they do not exercise all possibilities.

Generated `local-part` does not generate `quoted-string` or `obs-local-part` values.

Generated `domain` returns a RFC-1035 [`DomainName`](../DomainRandomizationSourceExtensions/DomainName.md). RFC-2822 allows many `domain` values which do not comply with RFC-1035. E.g., "----------" and "/" are valid `domain-literal` values.

See https://datatracker.ietf.org/doc/html/rfc2822#section-3.4.1

## See Also

* interface [IRandomizationSource](../IRandomizationSource.md)
* class [MailAddressRandomizationSourceExtensions](../MailAddressRandomizationSourceExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

---

# MailAddressRandomizationSourceExtensions.MailAddressAddrSpec method (2 of 2)

Generates a pseudo-random mail address compliant with `addr-spec` of RFC-2822.

```csharp
public static string MailAddressAddrSpec(this IRandomizationSource randomizationSource, int length)
```

| parameter | description |
| --- | --- |
| randomizationSource | A [`IRandomizationSource`](../IRandomizationSource.md) providing values. |
| length | An address length. |

## Return Value

A pseudo-random mail address compliant with `addr-spec` of RFC-2822.

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentOutOfRangeException | Thrown when *length* is less than 3 or greater than 254. |

## Remarks

While values generated should be compliant with `addr-spec` defined in RFC-2822 section 3.4.1, they do not exercise all possibilities.

Generated `local-part` does not generate `quoted-string` or `obs-local-part` values.

Generated `domain` returns a RFC-1035 [`DomainName`](../DomainRandomizationSourceExtensions/DomainName.md). RFC-2822 allows many `domain` values which do not comply with RFC-1035. E.g., "----------" and "/" are valid `domain-literal` values.

See https://datatracker.ietf.org/doc/html/rfc2822#section-3.4.1

## See Also

* interface [IRandomizationSource](../IRandomizationSource.md)
* class [MailAddressRandomizationSourceExtensions](../MailAddressRandomizationSourceExtensions.md)
* namespace [Jds.TestingUtils.Randomization](../../TestingUtils.Randomization.md)

<!-- DO NOT EDIT: generated by xmldocmd for TestingUtils.Randomization.dll -->
