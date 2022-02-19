namespace Jds.TestingUtils.Randomization;

public static class DemographicsRandomizationSourceExtensions
{
  private const int SurnameCharacterChainLength = 3;

  private const int ForenameCharacterChainLength = 3;

  private static
    Lazy<IReadOnlyDictionary<IReadOnlyList<char>, MarkovStringRandomizationSourceExtensions.MarkovResult<char>>>
    SurnameUsaMarkovModel
  {
    get;
  } = new(() =>
    MarkovStringRandomizationSourceExtensions.GetMarkovProbability(
      sources: MarkovSourceHelpers.ParseIntoWords(DemographicsSources.SurnameUsa)
        .Select(source => source.ToCharArray())
        .ToArray(),
      SurnameCharacterChainLength
    ));

  private static
    Lazy<IReadOnlyDictionary<IReadOnlyList<char>, MarkovStringRandomizationSourceExtensions.MarkovResult<char>>>
    ForenameUsaMarkovModel
  {
    get;
  } = new(() =>
    MarkovStringRandomizationSourceExtensions.GetMarkovProbability(
      sources: MarkovSourceHelpers.ParseIntoWords(DemographicsSources.ForenameUsa)
        .Select(source => source.ToCharArray())
        .ToArray(),
      ForenameCharacterChainLength
    ));

  /// <summary>
  ///   Generates a pseudo-random <see cref="DateTime" /> representing a date of birth.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="ageRange">An age range.</param>
  /// <param name="relativeTo">A <see cref="DateTime" /> to which the age is relative.</param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when any age range value is &lt; 1.</exception>
  public static DateTime DemographicsBirthDateTime(this IRandomizationSource randomizationSource,
    (int MinAgeInYears, int MaxAgeInYearsExclusive) ageRange, DateTime? relativeTo = null)
  {
    var (minAgeInYears, maxAgeInYears) = ageRange;
    if (minAgeInYears <= 0 || maxAgeInYears <= 0)
    {
      throw new ArgumentOutOfRangeException(paramName: nameof(ageRange),
        message: $"Both {nameof(ageRange.MinAgeInYears)} and {nameof(ageRange.MaxAgeInYearsExclusive)} must be > 0."
      );
    }

    var referenceDate = relativeTo ?? DateTime.UtcNow;
    var minAge = Math.Min(minAgeInYears, maxAgeInYears);
    var maxAge = Math.Max(minAgeInYears, maxAgeInYears);
    var ageValue = minAge + randomizationSource.NextDouble() * (maxAge - minAge);

    return referenceDate.Subtract(value: TimeSpan.FromDays(value: ageValue * 365.25));
  }

  /// <summary>
  ///   Generates a pseudo-random <see cref="DateTime" /> representing a date of birth. Uses an age range of 18-95,
  ///   inclusive.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="relativeTo">A <see cref="DateTime" /> to which the age is relative.</param>
  public static DateTime DemographicsBirthDateTime(this IRandomizationSource randomizationSource,
    DateTime? relativeTo = null)
  {
    return randomizationSource.DemographicsBirthDateTime(
      ageRange: (MinAgeInYears: 18, MaxAgeInYearsExclusive: 96),
      relativeTo
    );
  }

  /// <summary>
  ///   Generate a pseudo-random USA forename of up to <paramref name="maxLength" /> characters.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="maxLength">A maximum number of characters in the generated name.</param>
  /// <returns>A pseudo-random USA forename.</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxLength" /> is less than 1.</exception>
  public static string DemographicsForenameUsa(this IRandomizationSource randomizationSource, int maxLength = 20)
  {
    if (maxLength <= 0)
    {
      throw new ArgumentOutOfRangeException(paramName: nameof(maxLength));
    }

    return string.Concat(values: randomizationSource.GenerateRandomMarkov(ForenameUsaMarkovModel.Value, maxLength)
      .Take(maxLength)
      .Select((character, index) => index == 0 ? char.ToUpperInvariant(character) : character));
  }

  /// <summary>
  ///   Generate a pseudo-random USA surname of up to <paramref name="maxLength" /> characters.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="maxLength">A maximum number of characters in the generated name.</param>
  /// <returns>A pseudo-random USA surname.</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxLength" /> is less than 1.</exception>
  public static string DemographicsSurnameUsa(this IRandomizationSource randomizationSource, int maxLength = 20)
  {
    if (maxLength <= 0)
    {
      throw new ArgumentOutOfRangeException(paramName: nameof(maxLength));
    }

    return string.Concat(values: randomizationSource.GenerateRandomMarkov(SurnameUsaMarkovModel.Value, maxLength)
      .Take(maxLength)
      .Select((character, index) => index == 0 ? char.ToUpperInvariant(character) : character));
  }
}
