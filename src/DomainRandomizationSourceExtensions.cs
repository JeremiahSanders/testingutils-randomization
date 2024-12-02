using System.Collections.Immutable;
using System.Text;

namespace Jds.TestingUtils.Randomization;

public static class DomainRandomizationSourceExtensions
{
  private const int MaxBytesPerLabel = 63;
  private const int MaxBytesPerDomain = 253; // Max of 255, but requires two buffer bytes.

  /// <summary>
  ///   <see cref="char" /> values between `a` and `z`, values between `A` and `Z`, values between `0` and `9`, and the value `-`.
  /// </summary>
  private static IReadOnlyList<char> LettersDigitsHyphen { get; } = CharCollections.LettersDigits
    .Append('-')
    .ToImmutableArray();

  /// <summary>
  /// Validates that the middle of a domain label appears valid.
  /// </summary>
  /// <remarks>
  /// The label 'middle' starts at index 1 and ends with labelLength-1. The third and fourth characters of the label (indexes 2 and 3), are second and third characters (indexes 1 and 2) of the 'middle'.
  /// </remarks>
  /// <param name="possibleLabelMiddle"></param>
  /// <returns></returns>
  internal static bool IsDomainLabelMiddleInvalid(string possibleLabelMiddle)
  {
    // The third and fourth characters of a label may not both be hyphens.
    // The label 'middle' starts at index 1 and ends with labelLength-1. The third and fourth characters of the label (indexes 2 and 3), are second and third characters (indexes 1 and 2) of the 'middle'.
    const int thirdCharacterOfLabelMiddleIndex = 1;
    return possibleLabelMiddle.Substring(thirdCharacterOfLabelMiddleIndex, 2) == "--";
  }

  /// <summary>
  ///   Replaces the initial <paramref name="middle"/> if <see cref="IsDomainLabelMiddleInvalid"/>.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="middle"></param>
  /// <returns></returns>
  internal static string RegenerateLabelMiddleWithAlphaIfInvalid(IRandomizationSource randomizationSource,
    string middle)
  {
    // The third and fourth characters of a label may not both be hyphens.
    return IsDomainLabelMiddleInvalid(middle)
      // Since the first attempt, with hyphens, was invalid, just fall back to all alphanumeric.
      ? randomizationSource.RandomString(middle.Length, CharCollections.LettersDigits)
      : middle;
  }

  /// <summary>
  ///   Generate a pseudo-random domain label.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     See https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.4
  ///   </para>
  ///   <code>
  ///  &lt;label&gt; ::= &lt;letter&gt; [ [ &lt;ldh-str&gt; ] &lt;let-dig&gt; ]
  ///
  ///  &lt;ldh-str&gt; ::= &lt;let-dig-hyp&gt; | &lt;let-dig-hyp&gt; &lt;ldh-str&gt;
  ///
  ///  &lt;let-dig-hyp&gt; ::= &lt;let-dig&gt; | "-"
  ///
  ///  &lt;let-dig&gt; ::= &lt;letter&gt; | &lt;digit&gt;
  ///
  ///  &lt;letter&gt; ::= any one of the 52 alphabetic characters A through Z in
  ///  upper case and a through z in lower case
  ///
  ///  &lt;digit&gt; ::= any one of the ten digits 0 through 9
  /// </code>
  ///   <para>
  ///     Source https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.1
  ///   </para>
  /// </remarks>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  internal static string DomainLabel(this IRandomizationSource randomizationSource, int length)
  {
    if (length > MaxBytesPerLabel)
    {
      throw new ArgumentOutOfRangeException(nameof(length), $"Maximum length {MaxBytesPerLabel}");
    }

    if (length < 1)
    {
      throw new ArgumentOutOfRangeException(nameof(length), "Minimum length 1");
    }


    var stringBuilder = new StringBuilder();

    stringBuilder.Append(
      randomizationSource.RandomListItem(CharCollections.LatinAlpha)); // First character must be a letter.

    if (length > 2)
    {
      var middleLength = length - 2;
      var middle = randomizationSource.RandomString(middleLength, LettersDigitsHyphen);
      if (length > 4)
      {
        middle = RegenerateLabelMiddleWithAlphaIfInvalid(randomizationSource, middle);
      }

      stringBuilder.Append(middle);
    }

    if (length > 1)
    {
      stringBuilder.Append(
        randomizationSource.RandomListItem(CharCollections.LettersDigits)); // Final character must be letter or digit.
    }

    return stringBuilder.ToString();
  }

  private static int GetDistributionLength(IReadOnlyList<int> lengths)
  {
    var dotLength = lengths.Count - 1;
    return lengths.Sum() + dotLength;
  }

  /// <summary>
  ///   Generates a pseudo-random list of domain label lengths.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length"></param>
  /// <returns></returns>
  internal static IReadOnlyList<int> GenerateLabelLengths(this IRandomizationSource randomizationSource, int length)
  {
    IReadOnlyList<int> lengths;
    if (length < 3)
    {
      // Can't do subdomains with < 3 total domain characters.
      lengths = new[] { length };
    }
    else
    {
      bool IsDistributionValid(IReadOnlyList<int> distribution)
      {
        // Valid distribution is:
        // * Each item is <= maxBytesPerLabel
        // * (Sum of items) + (items.Length -1) == totalDomainLength
        return distribution.All(labelLength => labelLength <= MaxBytesPerLabel) &&
               length == distribution.Sum() + (distribution.Count - 1);
      }

      IReadOnlyList<int> GenerateLengths()
      {
        var randomLengths = new List<int>();

        var currentDistributionLength = GetDistributionLength(randomLengths);
        while (currentDistributionLength < length)
        {
          var next = randomizationSource.IntInRange(
            1,
            Math.Clamp(length - GetDistributionLength(randomLengths), 1, MaxBytesPerLabel)
          );
          var remainingAfterNext = length - currentDistributionLength - (next + 1);
          if (remainingAfterNext < 3)
          {
            // There will only be <3 characters left after this.
            // If possible, we need to extend this to capture those.
            if ((remainingAfterNext + next) <= MaxBytesPerLabel)
            {
              next += remainingAfterNext;
            }
          }

          randomLengths.Add(next);
          currentDistributionLength = GetDistributionLength(randomLengths);
        }

        return randomLengths;
      }

      do
      {
        lengths = GenerateLengths();
      } while (!IsDistributionValid(lengths));
    }

    return lengths;
  }

  /// <summary>
  ///   Generates a pseudo-random domain name.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     See https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.4
  ///   </para>
  ///   <code>
  ///  &lt;domain&gt; ::= &lt;subdomain&gt; | " "
  ///
  ///  &lt;subdomain&gt; ::= &lt;label&gt; | &lt;subdomain&gt; "." &lt;label&gt;
  ///
  ///  &lt;label&gt; ::= &lt;letter&gt; [ [ &lt;ldh-str&gt; ] &lt;let-dig&gt; ]
  ///
  ///  &lt;ldh-str&gt; ::= &lt;let-dig-hyp&gt; | &lt;let-dig-hyp&gt; &lt;ldh-str&gt;
  ///
  ///  &lt;let-dig-hyp&gt; ::= &lt;let-dig&gt; | "-"
  ///
  ///  &lt;let-dig&gt; ::= &lt;letter&gt; | &lt;digit&gt;
  ///
  ///  &lt;letter&gt; ::= any one of the 52 alphabetic characters A through Z in
  ///  upper case and a through z in lower case
  ///
  ///  &lt;digit&gt; ::= any one of the ten digits 0 through 9
  /// </code>
  ///   <para>
  ///     Source https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.1
  ///   </para>
  /// </remarks>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="domainLabelLengths">A list of domain label segment lengths.</param>
  /// <returns>A pseudo-random domain name which conforms to RFC-1035.</returns>
  public static string DomainName(this IRandomizationSource randomizationSource, IReadOnlyList<int> domainLabelLengths)
  {
    if (!domainLabelLengths.Any())
    {
      throw new ArgumentException("At least one label length required", nameof(domainLabelLengths));
    }


    var length = GetDistributionLength(domainLabelLengths);

    if (length < 1)
    {
      throw new ArgumentOutOfRangeException(nameof(domainLabelLengths), "Total length must be > 0");
    }

    if (length > MaxBytesPerDomain)
    {
      throw new ArgumentOutOfRangeException(nameof(domainLabelLengths),
        $"Maximum domain length is {MaxBytesPerDomain}");
    }

    if (domainLabelLengths.Any(labelLength => labelLength > MaxBytesPerLabel))
    {
      throw new ArgumentOutOfRangeException((nameof(domainLabelLengths)),
        $"No domain labels may be over {MaxBytesPerLabel} characters");
    }

    return String.Join(".", domainLabelLengths.Select(randomizationSource.DomainLabel));
  }

  /// <summary>
  ///   Generates a pseudo-random domain name.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     See https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.4
  ///   </para>
  ///   <code>
  ///  &lt;domain&gt; ::= &lt;subdomain&gt; | " "
  ///
  ///  &lt;subdomain&gt; ::= &lt;label&gt; | &lt;subdomain&gt; "." &lt;label&gt;
  ///
  ///  &lt;label&gt; ::= &lt;letter&gt; [ [ &lt;ldh-str&gt; ] &lt;let-dig&gt; ]
  ///
  ///  &lt;ldh-str&gt; ::= &lt;let-dig-hyp&gt; | &lt;let-dig-hyp&gt; &lt;ldh-str&gt;
  ///
  ///  &lt;let-dig-hyp&gt; ::= &lt;let-dig&gt; | "-"
  ///
  ///  &lt;let-dig&gt; ::= &lt;letter&gt; | &lt;digit&gt;
  ///
  ///  &lt;letter&gt; ::= any one of the 52 alphabetic characters A through Z in
  ///  upper case and a through z in lower case
  ///
  ///  &lt;digit&gt; ::= any one of the ten digits 0 through 9
  /// </code>
  ///   <para>
  ///     Source https://datatracker.ietf.org/doc/html/rfc1035#section-2.3.1
  ///   </para>
  /// </remarks>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="domainLength">A desired domain name length.</param>
  /// <returns>A pseudo-random domain name which conforms to RFC-1035.</returns>
  public static string DomainName(this IRandomizationSource randomizationSource, int domainLength)
  {
    if (domainLength > MaxBytesPerDomain)
    {
      throw new ArgumentOutOfRangeException(nameof(domainLength),
        $"Maximum domain length is {MaxBytesPerDomain}");
    }

    return randomizationSource.DomainName(randomizationSource.GenerateLabelLengths(domainLength));
  }
}
