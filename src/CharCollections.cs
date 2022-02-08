using System.Collections.Immutable;

namespace Jds.TestingUtils.Randomization;

/// <summary>
///   Collections of <see cref="char" /> suitable for use with
///   <see cref="StringRandomizationSourceExtensions.RandomString" />.
/// </summary>
internal static class CharCollections
{
  private const int LowercaseA = 'a';
  private const int UppercaseA = 'A';
  private const int Zero = '0';

  static CharCollections()
  {
    LatinLowercase = Enumerable.Range(LowercaseA, 26)
      .Select(Convert.ToChar)
      .ToImmutableArray();
    LatinUppercase = Enumerable.Range(UppercaseA, 26)
      .Select(Convert.ToChar)
      .ToImmutableArray();
    Integers = Enumerable.Range(Zero, 10)
      .Select(Convert.ToChar)
      .ToImmutableArray();

    LatinAlpha = LatinLowercase.Concat(LatinUppercase).ToImmutableArray();

    LatinAlphanumericLowercase = LatinLowercase.Concat(Integers).ToImmutableArray();
    LatinAlphanumericUppercase = LatinUppercase.Concat(Integers).ToImmutableArray();

    LettersDigits = LatinAlpha
      .Concat(Integers)
      .ToImmutableArray();
  }

  /// <summary>
  ///   <see cref="char" /> values between `a` and `z` and values between `A` and `Z`.
  /// </summary>
  public static IReadOnlyList<char> LatinAlpha { get; }

  /// <summary>
  ///   <see cref="char" /> values between `0` and `9`.
  /// </summary>
  public static IReadOnlyList<char> Integers { get; }

  /// <summary>
  ///   <see cref="char" /> values between `a` and `z` and between `0` and `9`.
  /// </summary>
  public static IReadOnlyList<char> LatinAlphanumericLowercase { get; }

  /// <summary>
  ///   <see cref="char" /> values between `A` and `Z` and between `0` and `9`.
  /// </summary>
  public static IReadOnlyList<char> LatinAlphanumericUppercase { get; }

  /// <summary>
  ///   <see cref="char" /> values between `a` and `z`.
  /// </summary>
  public static IReadOnlyList<char> LatinLowercase { get; }

  /// <summary>
  ///   <see cref="char" /> values between `0` and `9`.
  /// </summary>
  public static IReadOnlyList<char> LatinUppercase { get; }
  
  /// <summary>
  ///   <see cref="char" /> values between `a` and `z`, values between `A` and `Z`, and values between `0` and `9`.
  /// </summary>
  public static IReadOnlyList<char> LettersDigits { get; }
}
