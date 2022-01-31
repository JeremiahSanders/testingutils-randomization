# Testing Utils: Randomization

This collection of randomization test utilities supports creating test arrangements.

## How to Use

> All examples below use the thread-safe, static `Jds.TestingUtils.Randomization.Randomizer.Shared` instance of `IRandomizationSource`, which generates random values using `System.Random.Shared`. This `IRandomizationSource` is advised for most tests.
>
> If a different algorithm is needed, e.g., a [cryptographically strong random number generator][] is required, consider creating a `Jds.TestingUtils.Randomization.ArrangedRandomizationSource`. It uses provided `Func<IEnumerable<>>` delegates to supply values when requested.
>
> If a specific set of random-seeming data is needed, consider creating a `Jds.TestingUtils.Randomization.DeterministicRandomizationSource`. It uses provided `IEnumerable<>` sources to supply values when requested.

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
  * Example: `Jds.TestingUtils.Randomization.Randomizer.Shared.RandomEnumValue<System.Net.HttpStatusCode>()`
* `IRandomizationSource.RandomListItem<T>(IReadOnlyList<T> items)`
  * Selects a pseudo-random item from provided `items`.
  * Example: `Jds.TestingUtils.Randomization.Randomizer.Shared.RandomListItem(System.Linq.Enumerable.Range(1, 20).ToArray())`

[cryptographically strong random number generator]: https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-6.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32-system-int32)