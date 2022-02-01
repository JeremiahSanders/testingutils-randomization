using System.Text;

namespace Jds.TestingUtils.Randomization;

public static class StringRandomizationSourceExtensions
{
  /// <summary>
  ///   Generates a random <see cref="string" /> from <paramref name="chars" />, assumed to be non-empty.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length">A string length.</param>
  /// <param name="chars">A collection of chars from which the random string characters are selected.</param>
  /// <returns>
  ///   A randomly-generated <see cref="string" /> from <paramref name="chars" /> of <paramref name="length" />
  ///   characters.
  /// </returns>
  /// <exception cref="ArgumentException">Thrown if <paramref name="chars" /> is empty.</exception>
  public static string RandomString(this IRandomizationSource randomizationSource,
    int length,
    IReadOnlyList<char> chars
  )
  {
    if (!chars.Any())
    {
      throw new ArgumentException("Collection is empty", nameof(chars));
    }

    return length < 1
      ? string.Empty
      : string.Concat(
        Enumerable.Range(0, length).Select(_ => chars[randomizationSource.NextIntInRange(0, chars.Count)])
      );
  }

  /// <summary>
  ///   Generates a random <see cref="string" /> from <paramref name="strings" />, assumed to be non-empty.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length">A string length.</param>
  /// <param name="strings">A collection of strings from which the random string elements are selected.</param>
  /// <returns>
  ///   A randomly-generated <see cref="string" />, assembled from <paramref name="strings" />, of <paramref name="length" />
  ///   characters.
  /// </returns>
  /// <exception cref="ArgumentException">Thrown if <paramref name="strings" /> is empty.</exception>
  public static string RandomString(this IRandomizationSource randomizationSource,
    int length,
    IReadOnlyList<string> strings
  )
  {
    if (!strings.Any())
    {
      throw new ArgumentException("Collection is empty", nameof(strings));
    }

    var stringBuilder = new StringBuilder();

    while (stringBuilder.Length < length)
    {
      var segment = strings[randomizationSource.NextIntInRange(0, strings.Count)];
      stringBuilder.Append(segment);
    }

    return stringBuilder.ToString()[..length];
  }

  /// <summary>
  ///   Generates a random <see cref="string" /> using ASCII Latin characters.
  /// </summary>
  /// <remarks>
  ///   This method is equivalent to
  ///   <see
  ///     cref="RandomString(Jds.TestingUtils.Randomization.IRandomizationSource,int,System.Collections.Generic.IReadOnlyList{char})" />
  ///   , but uses predefined char ranges of `a` to `z` for Latin lowercase, `A` to `Z`, for Latin uppercase, and `0` to `9`
  ///   for numeric.
  /// </remarks>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length">A string length.</param>
  /// <param name="uppercase">A flag indicating whether generated characters should be uppercase. False indicates lowercase.</param>
  /// <param name="alphanumeric">
  ///   A flag indicating whether generate characters should be alphanumeric. False indicates
  ///   alphabetic.
  /// </param>
  /// <returns>A randomly-generated <see cref="string" /> of <paramref name="length" /> characters.</returns>
  public static string RandomStringLatin(this IRandomizationSource randomizationSource,
    int length,
    bool uppercase = false,
    bool alphanumeric = false
  )
  {
    IReadOnlyList<char> chars;
    if (uppercase)
    {
      chars = alphanumeric ? CharCollections.LatinAlphanumericUppercase : CharCollections.LatinUppercase;
    }
    else
    {
      chars = alphanumeric ? CharCollections.LatinAlphanumericLowercase : CharCollections.LatinLowercase;
    }

    return randomizationSource.RandomString(length, chars);
  }
}
