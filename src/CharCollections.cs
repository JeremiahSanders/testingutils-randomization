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
      .ToArray();
    LatinUppercase = Enumerable.Range(UppercaseA, 26)
      .Select(Convert.ToChar)
      .ToArray();
    Integers = Enumerable.Range(Zero, 10)
      .Select(Convert.ToChar)
      .ToArray();

    LatinAlphanumericLowercase = LatinLowercase.Concat(Integers).ToArray();
    LatinAlphanumericUppercase = LatinUppercase.Concat(Integers).ToArray();
  }

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
}
