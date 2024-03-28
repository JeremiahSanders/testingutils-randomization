using System.Text;

namespace Jds.TestingUtils.Randomization;

public static class LoremIpsumRandomizationSourceExtensions
{
  private const int LoremIpsumCharacterChainLength = 3;

  private static
    Lazy<IReadOnlyDictionary<IReadOnlyList<char>, MarkovStringRandomizationSourceExtensions.MarkovResult<char>>>
    LipsumWordGenerator { get; } = new(LipsumWordGeneratorDictionaryFactory);

  private static IReadOnlyDictionary<IReadOnlyList<char>, MarkovStringRandomizationSourceExtensions.MarkovResult<char>>
    LipsumWordGeneratorDictionaryFactory()
  {
    return MarkovStringRandomizationSourceExtensions.GetMarkovProbability(LoremIpsumSources
      .ParseIntoWords(LoremIpsumSources.LoremIpsumSource.MeditatioIvDeVeroEtFalso)
      .Concat(LoremIpsumSources.ParseIntoWords(LoremIpsumSources.LoremIpsumSource
        .MeditatioViDeRerumMaterialiumExistentiaEtRealiMentisACorporeDistinctione))
      .Concat(LoremIpsumSources.ParseIntoWords(LoremIpsumSources.LoremIpsumSource.CiceroDeFinibusBonorumEtMalorum))
      .Select(source => source.ToCharArray())
      .ToList(), LoremIpsumCharacterChainLength);
  }

  /// <summary>
  ///   Generate a pseudo-random Latin-derived word of up to <paramref name="maxLength" /> characters.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="maxLength">A maximum number of characters in the generated word.</param>
  /// <returns>A pseudo-random Latin-derived word.</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxLength" /> is less than 1.</exception>
  public static string LoremIpsumWord(this IRandomizationSource randomizationSource, int maxLength)
  {
    if (maxLength <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(maxLength));
    }

    var stringBuilder = new StringBuilder();

    return randomizationSource.GenerateRandomMarkov(LipsumWordGenerator.Value, maxLength)
      .Take(maxLength)
      .Aggregate(stringBuilder, static (builder, c) => builder.Append(c))
      .ToString();
    
    return string.Concat(randomizationSource.GenerateRandomMarkov(LipsumWordGenerator.Value, maxLength)
      .Take(maxLength));
  }

  /// <summary>
  ///   Generate a pseudo-random Latin-derived sentence.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="sentenceParameters">A sentence word count and maximum characters in each word.</param>
  /// <returns>A pseudo-random Latin-derived sentence.</returns>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when any property of <paramref name="sentenceParameters" /> is
  ///   less than 1.
  /// </exception>
  public static string LoremIpsumSentence(this IRandomizationSource randomizationSource,
    (int WordCount, int MaxWordLength) sentenceParameters
  )
  {
    var (wordCount, maxWordLength) = sentenceParameters;
    if (wordCount <= 0 || maxWordLength <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(sentenceParameters));
    }

    return string.Concat(string
      .Join(' ',
        Enumerable.Range(1, wordCount)
          .Select(_ => LoremIpsumWord(randomizationSource, maxWordLength))
      )
      .ToCharArray()
      .Select(UpperCaseFirstCharacter)
      .Append('.'));

    static char UpperCaseFirstCharacter(char character, int index)
    {
      return index == 0 ? char.ToUpperInvariant(character) : character;
    }
  }

  /// <summary>
  ///   Generate a pseudo-random Latin-derived paragraph.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="paragraphParameters">A paragraph word count and maximum characters in each word.</param>
  /// <returns>A pseudo-random Latin-derived sentence.</returns>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when any property of <paramref name="paragraphParameters" /> is
  ///   less than 1.
  /// </exception>
  public static string LoremIpsumParagraph(this IRandomizationSource randomizationSource,
    (int WordCount, int MaxWordLength) paragraphParameters
  )
  {
    const int minPlannedWordsPerSentence = 3;
    const int exclusiveMaxPlannedWordsPerSentence = 18;
    var (wordCount, maxWordLength) = paragraphParameters;
    if (wordCount <= 0 || maxWordLength <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(paragraphParameters));
    }

    var state = (WordsCreated: 0, Sentences: (IEnumerable<string>)Array.Empty<string>());
    while (state.WordsCreated < wordCount)
    {
      var sentenceWordCount = Math.Min(
        wordCount - state.WordsCreated,
        randomizationSource.IntInRange(minPlannedWordsPerSentence, exclusiveMaxPlannedWordsPerSentence)
      );
      state = (state.WordsCreated + sentenceWordCount,
          state.Sentences.Append(
            LoremIpsumSentence(randomizationSource,
              (sentenceWordCount, maxWordLength))
          )
        );
    }
    
    
    

    return string.Join(' ',
      state.Sentences
    );
  }
}
