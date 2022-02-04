# Testing Utils: Randomization

This collection of randomization test utilities supports creating test arrangements.

## How to Use

> All examples below use the thread-safe, static `Jds.TestingUtils.Randomization.Randomizer.Shared` instance of `IRandomizationSource`, which generates random values using `System.Random.Shared`. This `IRandomizationSource` is advised for most tests.
>
> If a different algorithm is needed, e.g., a [cryptographically strong random number generator][] is required, consider creating a `Jds.TestingUtils.Randomization.ArrangedRandomizationSource`. It uses provided `Func<IEnumerable<>>` delegates to supply values when requested.
>
> If a specific set of random-seeming data is needed, consider creating a `Jds.TestingUtils.Randomization.DeterministicRandomizationSource`. It uses provided `IEnumerable<>` sources to supply values when requested.

### Add `Jds.TestingUtils.Randomization` NuGet Package and Add `using`

Add `Jds.TestingUtils.Randomization` NuGet package to the test project.

Add the extensions to your test files with the following `using` statement:

```c#
using Jds.TestingUtils.Randomization;
```

All examples below assume the following _additional_ `using` statements:

```c#
using System;
using System.Collections.Generic;
using System.Linq;
```

### Generate Pseudo-random Numbers

* `IRandomizationSource.Byte()`
  * Gets a pseudo-random `byte`, greater than or equal to `byte.MinValue`, and less than `byte.MaxValue`.
* `IRandomizationSource.ByteInRange(byte minInclusive, byte maxExclusive)`
  * Gets a pseudo-random `byte`, greater than or equal to `minInclusive`, and less than `maxExclusive`.
* `IRandomizationSource.Double()`
  * Gets a pseudo-random `double`, using `IRandomizationSource.NextDouble`. The value should be greater than or equal to `0.0`, and less than `1.0`.
* `IRandomizationSource.Float()`
  * Gets a pseudo-random `float`, using `IRandomizationSource.NextFloat`. The value should be greater than or equal to `0.0`, and less than `1.0`.
* `IRandomizationSource.Int()`
  * Gets a pseudo-random `int`, using `IRandomizationSource.NextIntInRange`. The value should be greater than or equal to `int.MinValue`, and less than `int.MaxValue`.
* `IRandomizationSource.IntInRange(int minInclusive, int maxExclusive)`
  * Gets a pseudo-random `int`, using `IRandomizationSource.NextIntInRange`. The value should be greater than or equal to `minInclusive`, and less than `maxExclusive`.
* `IRandomizationSource.IntNegative()`
  * Gets a pseudo-random `int`, using `IRandomizationSource.NextIntInRange`. The value should be greater than or equal to `int.MinValue`, and less than `0`.
* `IRandomizationSource.IntPositive()`
  * Gets a pseudo-random `int`, using `IRandomizationSource.NextIntInRange`. The value should be greater than or equal to `0`, and less than `int.MaxValue`.
* `IRandomizationSource.Long()`
  * Gets a pseudo-random `long`, using `IRandomizationSource.NextLongInRange`. The value should be greater than or equal to `long.MinValue`, and less than `long.MaxValue`.
* `IRandomizationSource.LongInRange(long minInclusive, long maxExclusive)`
  * Gets a pseudo-random `long`, using `IRandomizationSource.NextLongInRange`. The value should be greater than or equal to `minInclusive`, and less than `maxExclusive`.
* `IRandomizationSource.LongNegative()`
  * Gets a pseudo-random `long`, using `IRandomizationSource.NextLongInRange`. The value should be greater than or equal to `long.MinValue`, and less than `0`.
* `IRandomizationSource.LongPositive()`
  * Gets a pseudo-random `long`, using `IRandomizationSource.NextLongInRange`. The value should be greater than or equal to `0`, and less than `long.MaxValue`.

### Select a Random Item or Enumeration Value

* `IRandomizationSource.RandomEnumValue<TEnum>()`
  * Selects a pseudo-random value from the `enum` of type specified (`TEnum`).
  * Example: `Randomizer.Shared.RandomEnumValue<System.Net.HttpStatusCode>()`
* `IRandomizationSource.RandomListItem<T>(IReadOnlyList<T> items)`
* `IReadOnlyList<T>.GetRandomItem<T>()`
  * Selects a pseudo-random item from provided `items`.
  * Example: `Randomizer.Shared.RandomListItem(System.Linq.Enumerable.Range(1, 20).ToArray())`

### Select a _Weighted_ Random Item or Dictionary Key

* `IRandomizationSource.RandomListItem<T>(IReadOnlyList<(T Item, double Weight)> weightedItems)`
* `IRandomizationSource.RandomListItem<T>(IReadOnlyList<(T Item, int Weight)> weightedItems)`
* `IReadOnlyList<(T Key, double Weight)>.GetWeightedRandomItem<T>()`
* `IReadOnlyList<(T Key, int Weight)>.GetWeightedRandomItem<T>()`
    * Selects a pseudo-random `.Item` from provided `weightedItems`; item selection is weighted based upon relative `.Weight`.
    * Example:
```c#
Randomizer.Shared.WeightedRandomListItem(
  new[] { ("Sure", 1000), ("Likely", 500), ("Possible", 200), ("Unlikely", 50), ("Rare", 5), ("Apocryphal", 1) }
);
```

* `IRandomizationSource.GetWeightedRandomKey<T>(IReadOnlyDictionary<T, double> weightedKeys)`
* `IRandomizationSource.GetWeightedRandomKey<T>(IReadOnlyDictionary<T, int> weightedKeys)`
* `IReadOnlyDictionary<T, double>.GetWeightedRandomKey<T>()`
* `IReadOnlyDictionary<T, int>.GetWeightedRandomKey<T>()`
    * Selects a pseudo-random `.Key` from provided `weightedItems`; item selection is weighted based upon relative `.Value`.
    * Example:
```c#
Randomizer.Shared.WeightedRandomKey(new Dictionary<string, double>
{
  { "North", 0.4 }, { "East", 0.1 }, { "West", 0.1 }, { "South", 0.4 }
});
```

### Generate a Random String

* `IRandomizationSource.RandomString(int length, IReadOnlyList<char> chars)`
  * Generates a random `string` of `length` characters, using provided `chars`. Random selections from `chars` are concatenated until reaching `length` characters.
* `IRandomizationSource.RandomString(int length, IReadOnlyList<string> strings)`
  * Generates a random `string` of `length` characters, using provided `strings`. Random selections from `strings` are concatenated until reaching `length` characters. The result is truncated to `length` characters.
* `IRandomizationSource.RandomStringLatin(int length, bool uppercase = false, bool alphanumeric = false)`
  * Generates a random `string` of `length` characters using ASCII Latin characters. Uses `a` - `z` by default. If `uppercase`, uses `A` - `Z`. If `alphanumeric`, also includes `0` - `9` with either casing.

[cryptographically strong random number generator]: https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-6.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32-system-int32)