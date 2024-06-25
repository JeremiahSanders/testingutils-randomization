# Testing Utils: Randomization

[![NuGet](https://badgen.net/nuget/v/Jds.TestingUtils.Randomization/)](https://www.nuget.org/packages/Jds.TestingUtils.Randomization/)
[![Publish Prerelease](https://github.com/JeremiahSanders/testingutils-randomization/actions/workflows/publish-prerelease.yml/badge.svg?branch=dev)](https://github.com/JeremiahSanders/testingutils-randomization/actions/workflows/publish-prerelease.yml)
[![Publish Release](https://github.com/JeremiahSanders/testingutils-randomization/actions/workflows/publish-release.yml/badge.svg?branch=main)](https://github.com/JeremiahSanders/testingutils-randomization/actions/workflows/publish-release.yml)

This collection of randomization test utilities supports creating test arrangements.

## [API Documentation][]

See [API Documentation][].

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

* `IRandomizationSource.Boolean()`
  * Gets a pseudo-random `bool`.
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

### Generate a Random Number of Generated Items

* `IRandomizationSource.Enumerable<T>(Func<T> factory, int inclusiveMinCount, int exclusiveMaxCount)`
* `IRandomizationSource.Enumerable<T>(Func<int, T> factory, int inclusiveMinCount, int exclusiveMaxCount)`
  * Creates an `IEnumerable<T>` of a randomly-generated length using values provided by a factory method.
  * Example: `Randomizer.Shared.Enumerable(Guid.NewGuid, 3, 7)` - which would generate a sequence of `3` to `6` `Guid` values.

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
  * Generates a pseudo-random `string` of `length` characters, using provided `chars`. Random selections from `chars` are concatenated until reaching `length` characters.
* `IRandomizationSource.RandomString(int length, IReadOnlyList<string> strings)`
  * Generates a pseudo-random `string` of `length` characters, using provided `strings`. Random selections from `strings` are concatenated until reaching `length` characters. The result is truncated to `length` characters.
* `IRandomizationSource.RandomStringLatin(int length, bool uppercase = false, bool alphanumeric = false)`
  * Generates a pseudo-random `string` of `length` characters using ASCII Latin characters. Uses `a` - `z` by default. If `uppercase`, uses `A` - `Z`. If `alphanumeric`, also includes `0` - `9` with either casing.

### Generate a Mail Address

* `IRandomizationSource.MailAddress()`
* `IRandomizationSource.MailAddress(int length)`
  * Generates a pseudo-random `System.Net.Mail.MailAddress`. The generated `System.Net.Mail.MailAddress.User` will be a `dot-atom` form of `local-part` (see [RFC-2822 section 3.4.1][addr-spec]). The generated `System.Net.Mail.MailAddress.Host` will be a `domain` (see [RFC-1035 section 2.3.1][domain-names]).
* `IRandomizationSource.MailAddressAddrSpec(int length)`
* `IRandomizationSource.MailAddressAddrSpec((int LocalPartLength, int DomainLength) componentLengths)`
  * Generates a pseudo-random `string` according to `addr-spec` (see [RFC-2822 section 3.4.1][addr-spec]). The generated `local-part` will be of `dot-atom` form (see [RFC-2822 section 3.4.1][addr-spec]). The generated `domain` will be of `dot-atom` form (see [RFC-2822 section 3.4.1][addr-spec]), and its value will be generated as a [RFC-1035 section 2.3.1 `domain`][domain-names].

### Generate a Domain

* `IRandomizationSource.DomainName(int length)`
* `IRandomizationSource.DomainName(IReadOnlyList<int> domainLabelLengths)`
    * Generates a pseudo-random `string` according to `domain` (see [RFC-1035 section 2.3.1][domain-names]).

### Generate a URL

* `IRandomizationSource.RandomUrl(int hostLength, int pathLength = 0, int queryLength = 0, int fragmentLength = 0, string scheme = "https", int? port = null)`
    * Generates a pseudo-random `string` URL according to [RFC-3986 URI syntax][]. The `host` segment is generated using `IRandomizationSource.DomainName(int length)`.

### Generate a Sequence of Items from a [Markov Chain][] Model

* `IRandomizationSource.CreateMarkovGenerator(IReadOnlyCollection<IReadOnlyList<T>> sources, int chainLength = 1) where T : notnull, IEquatable<T>`
    * Generates a `Func<int, IReadOnlyList<T>>` which accepts an `int maxLength` and uses a [Markov Chain][] model, derived from `sources`, to generate sequences of `T` of up to length `maxLength`.
        * The `chainLength` determines how many `T` are grouped to determine [Markov Chain][] probability.
* `IRandomizationSource.CreateMarkovGenerator(IEnumerable<string> sources, int chainLength = 1)`
    * Generates a `Func<int, string>` which accepts an `int maxLength` and uses a [Markov Chain][] model, derived from `sources`, to generate strings of up to length `maxLength`.
        * The `chainLength` determines how many characters in each input `string` are grouped to determine [Markov Chain][] probability.
    * Example uses: create a random word or name generator using a list of example words or names.
    * Example:
```c#
var exampleFruitNames = new[]
  {
    "apple", "apricot", "avocado", "banana", "blackberry", "blackcurrant", "blueberry", "boysenberry", "cantaloupe",
    "caper", "cherry", "cranberry", "elderberry", "fig", "gooseberry", "grape", "grapefruit", "guava", "jujube",
    "kiwi", "kumquat", "lemon", "lime", "lychee", "mango", "mulberry", "olive", "orange", "papaya", "pear",
    "persimmon", "pineapple", "plantain", "plum", "pomegranate", "raspberry", "starfruit", "strawberry", "tangerine",
    "watermelon"
  };

Func<int, string> fruitGenerator = Randomizer.Shared.CreateMarkovGenerator(sources: exampleFruitNames, chainLength: 2);

string generatedFruit = fruitGenerator(maxLength: 12);
string possiblyLongerGeneratedFruit = fruitGenerator(maxLength: 20);
string likelyShorterGeneratedFruit = fruitGenerator(maxLength: 6);
```

* `IRandomizationSource.GenerateRandomMarkov<T>(IReadOnlyCollection<IReadOnlyList<T>> sources, int? maxLength = null, int chainLength = 1)`
    * Generates a `IReadOnlyList<T>` based upon a [Markov Chain][] model, derived from `sources`.
        * The `chainLength` determines how many items in each input `IReadOnlyList<T>` are grouped to determine [Markov Chain][] probability.
    * Use `IRandomizationSource.CreateMarkovGenerator()` (above), instead of this function, unless only a **single** value is needed.
        * The resources needed to generate the [Markov Chain][] model are non-trivial. This function creates a new model each time it is executed, consuming both computational and memory resources.
* `IRandomizationSource.GenerateRandomMarkov(IEnumerable<string> sources, int? maxLength = null, int chainLength = 1)`
    * Generates a `string` based upon a [Markov Chain][] model, derived from `sources`..
        * The `chainLength` determines how many characters in each input `string` are grouped to determine [Markov Chain][] probability.
    * Use `IRandomizationSource.CreateMarkovGenerator()` (above), instead of this function, unless only a **single** value is needed.
        * The resources needed to generate the [Markov Chain][] model are non-trivial. This function creates a new model each time it is executed, consuming both computational and memory resources.
    * Example uses: create a random word or name generator using a list of example words or names.
    * Example:
```c#
var exampleFruitNames = new[]
  {
    "apple", "apricot", "avocado", "banana", "blackberry", "blackcurrant", "blueberry", "boysenberry", "cantaloupe",
    "caper", "cherry", "cranberry", "elderberry", "fig", "gooseberry", "grape", "grapefruit", "guava", "jujube",
    "kiwi", "kumquat", "lemon", "lime", "lychee", "mango", "mulberry", "olive", "orange", "papaya", "pear",
    "persimmon", "pineapple", "plantain", "plum", "pomegranate", "raspberry", "starfruit", "strawberry", "tangerine",
    "watermelon"
  };

string similarGeneratedFruit = Randomizer.Shared.GenerateRandomMarkov(sources: exampleFruitNames, maxLength: 12, chainLength: 2);
string slightlySimilarGeneratedFruit = Randomizer.Shared.GenerateRandomMarkov(sources: exampleFruitNames, maxLength: 20, chainLength: 1);
string verySimilarGeneratedFruit = Randomizer.Shared.GenerateRandomMarkov(sources: exampleFruitNames, maxLength: 15, chainLength: 3);
```

### Generate a Word, Sentence, or Paragraph ([Lorem Ipsum][])

> The [Lorem Ipsum][] random generators create Latin-like words, sentences, and paragraphs. They use [Markov Chain][] models trained on multiple Latin sources: the traditional [Lorem Ipsum][] excerpts of Cicero's De Finibus Bonorum et Malorum, and excerpts of René Descartes's Meditationes de Prima Philosophia.

* `IRandomizationSource.LoremIpsumParagraph((int WordCount, int MaxWordLength) paragraphParameters)`
    * Generates a paragraph `string` of `paragraphParameters.WordCount` words, broken into a random number of sentences. Each word in the paragraph is no more than `paragraphParameters.MaxWordLength` characters.
* `IRandomizationSource.LoremIpsumSentence((int WordCount, int MaxWordLength) sentenceParameters)`
    * Generates a sentence `string` of `sentenceParameters.WordCount` words, each word no more than `sentenceParameters.MaxWordLength` characters. Sentences always begin with a capital letter and end with a period (`.`).
* `IRandomizationSource.LoremIpsumWord(int maxLength)`
    * Generates a word `string` of no more than `maxLength` characters.

### Generate User Demographics

> The name generators use [Markov Chain][] models trained on public census and government data.

* `IRandomizationSource.DemographicsBirthDateTime(DateTime? relativeTo = null)`
* `IRandomizationSource.DemographicsBirthDateTime((int MinAgeInYears, int MaxAgeInYearsExclusive) ageRange, DateTime? relativeTo = null)`
    * Generates a date of birth `DateTime` within the `ageRange` specified, relative to `relativeTo`. If not provided, `ageRange` defaults to `(MinAgeInYears: 18, MaxAgeInYearsExclusive: 96)`. If not provided, `relativeTo` defaults to `DateTime.UtcNow`.
* `IRandomizationSource.DemographicsForenameUsa(int maxLength)`
    * Generates a forename `string` of no more than `maxLength` characters. Generated names have an initial capital letter and all subsequent characters are lowercase.
* `IRandomizationSource.DemographicsSurnameUsa(int maxLength)`
    * Generates a surname `string` of no more than `maxLength` characters. Generated names have an initial capital letter and all subsequent characters are lowercase.

### Create a Custom Domain-Specific Language to Generate Complex or Custom Types

This library does not provide a domain-specific language. However, by creating your own `static` methods extending `IStatefulRandomizationSource<TState>` (where `TState` should be _your generator options_), you can easily construct a domain-specific language to generate complex or custom types.

> This library's unit tests include an [example Domain-Specific Language which generates characters for a role-playing game][example-stateful-dsl]. The documented example illustrates:
>
> * creating a randomization state object (to provide configuration)
> * creating a domain value object (the _thing_ we want to generate)
> * creating a domain-specific language (a `static` method extending `IStatefulRandomizationSource<TState>` which used the configuration and generated the value object)
> * creating unit tests which use the domain-specific language

This functionality was already possible by extending `IRandomizationSource` with methods accepting parameters. However, `IStatefulRandomizationSource<TState>` provides greater flexibility. By embedding state object types into your extension methods, you can simplify verbose generator method signatures.

The `IStatefulRandomizationSource<TState>` extends `IRandomizationSource` with a `public TState State { get; }` property.

#### Construction

Use `Randomizer.WithState()` static methods to create a new stateful randomizer based upon an initial state value.

Use the `.WithState(TState initialState)` extension method on `IRandomizationSource` to create a derived stateful randomizer using that randomization source instance.

#### Modifying State

In some scenarios, you may want to modify the existing randomization source state (e.g., when designing a "builder" interface). This is handled using some monadic methods.

> It is intended that the state values used in a randomization source are immutable. When the state needs to change, the following monadic methods allow you to derive a new `IStatefulRandomizationSource<TState>` using the current state value.

* `IStatefulRandomizationSource<TStateCurrent,TStateNew>.Bind(Func<TStateCurrent,IStatefulRandomizationSource<TStateNew>>)`
  * Use `Bind` to replace the stateful randomization source with a new stateful randomization source derived from current state. _This is an uncommon `IStatefulRandomizationSource` operation, normally only used when swapping the underlying `IRandomizationSource`._
* `IStatefulRandomizationSource<TStateCurrent,TStateNew>.Map(Func<TStateCurrent,TStateNew>)`
  * Use `Map` to replace the state contained within the stateful randomization source with a new state (which may be of the same type or another). **This is the most common state operation.**

[addr-spec]: https://datatracker.ietf.org/doc/html/rfc2822#section-3.4.1
[API Documentation]: ./docs/api/TestingUtils.Randomization.md
[cryptographically strong random number generator]: https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-6.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32-system-int32)
[domain-names]: https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.1
[example-stateful-dsl]: https://github.com/JeremiahSanders/testingutils-randomization/blob/main/tests/unit/StatefulTests/RpgCharacterBuilderStateExample.cs
[Lorem Ipsum]: https://en.wikipedia.org/wiki/Lorem_ipsum
[Markov Chain]: https://en.wikipedia.org/wiki/Markov_chain
[RFC-3986 URI syntax]: https://datatracker.ietf.org/doc/html/rfc3986#section-3
