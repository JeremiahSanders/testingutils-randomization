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
