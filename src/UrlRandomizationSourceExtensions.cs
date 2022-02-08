using System.Collections.Immutable;
using System.Text;

namespace Jds.TestingUtils.Randomization;

public static class UrlRandomizationSourceExtensions
{
  private static string UrlPathAbsolute(this IRandomizationSource randomizationSource, int length)
  {
    var stringBuilder = new StringBuilder();
    while (stringBuilder.Length < length)
    {
      if (length - stringBuilder.Length == 1)
      {
        // If just one, add another character... but skip plain ".", as that would make the path relative.
        var nextCharacter = UrlConstants.UnreservedChars.GetRandomItem(randomizationSource);

        while (nextCharacter == '.')
        {
          nextCharacter = UrlConstants.UnreservedChars.GetRandomItem(randomizationSource);
        }

        stringBuilder.Append(nextCharacter);
      }
      else
      {
        // If > 1, add a segment.
        stringBuilder.Append('/');
        var remaining = length - stringBuilder.Length;
        var segmentLength = randomizationSource.IntInRange(1, maxExclusive: remaining + 1);
        var segment = randomizationSource.RandomString(segmentLength, UrlConstants.UnreservedChars);
        while (segment.All(charValue => charValue == '.'))
        {
          // Regenerate if all segment characters are ".". That's valid as a relative path, but must be stripped during parsing.
          segment = randomizationSource.RandomString(segmentLength, UrlConstants.UnreservedChars);
        }

        stringBuilder.Append(segment);
      }
    }

    return stringBuilder.ToString();
  }

  private static string UrlPath(this IRandomizationSource randomizationSource, int length)
  {
    return length switch
    {
      0 => "",
      1 => "/",
      _ => randomizationSource.UrlPathAbsolute(length)
    };
  }

  private static string UrlQuery(this IRandomizationSource randomizationSource, int length)
  {
    string GenerateQuery()
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.Append('?');

      while (stringBuilder.Length < length)
      {
        stringBuilder.Append(value: UrlConstants.QueryChars.GetRandomItem(randomizationSource));
      }

      return stringBuilder.ToString();
    }

    return length switch
    {
      0 => "",
      1 => "?",
      _ => GenerateQuery()
    };
  }

  private static string UrlFragment(this IRandomizationSource randomizationSource, int length)
  {
    string GenerateFragment()
    {
      // fragment    = *( pchar / "/" / "?" )
      var stringBuilder = new StringBuilder();

      stringBuilder.Append('#');

      while (stringBuilder.Length < length)
      {
        stringBuilder.Append(value: UrlConstants.FragmentChars.GetRandomItem(randomizationSource));
      }

      return stringBuilder.ToString();
    }

    return length switch
    {
      0 => "",
      1 => "#",
      _ => GenerateFragment()
    };
  }


  /// <summary>
  ///   Generates a pseudo-random URL.
  /// </summary>
  /// <remarks>
  ///   <para>Host: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.2.2 </para>
  ///   <para>Port: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.2.3 </para>
  ///   <para>Path: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.3 </para>
  ///   <para>Query: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.4 </para>
  ///   <para>Fragment: See https://datatracker.ietf.org/doc/html/rfc3986#section-3.5 </para>
  /// </remarks>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="hostLength">A URL host length.</param>
  /// <param name="queryLength">A URL query length.</param>
  /// <param name="port">A port.</param>
  /// <param name="fragmentLength">A fragment length.</param>
  /// <param name="scheme">A communication scheme, e.g., http, https, ftp.</param>
  /// <param name="pathLength">A URL path length.</param>
  /// <returns>A randomly-generated <see cref="Uri" />.</returns>
  /// <exception cref="ArgumentException">Thrown if <paramref name="scheme" /> is empty.</exception>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="hostLength" /> is insufficient.</exception>
  public static string RandomUrl(this IRandomizationSource randomizationSource,
    int hostLength,
    int pathLength = 0,
    int queryLength = 0,
    int fragmentLength = 0,
    string scheme = "https",
    int? port = null
  )
  {
    if (string.IsNullOrWhiteSpace(scheme))
    {
      throw new ArgumentException(message: $"{nameof(scheme)} is required.", paramName: nameof(scheme));
    }

    if (hostLength < 1)
    {
      throw new ArgumentOutOfRangeException(paramName: nameof(hostLength),
        message: $"{nameof(hostLength)} must be > 0");
    }

    if (port is < 0)
    {
      throw new ArgumentOutOfRangeException(paramName: nameof(port), message: $"{nameof(port)} may not < 0");
    }

    var domain = randomizationSource.DomainName(hostLength);
    var portDisplay = port != null
      ? $":{port}"
      : "";
    var path = randomizationSource.UrlPath(pathLength);
    var query = randomizationSource.UrlQuery(queryLength);
    var fragment = randomizationSource.UrlFragment(fragmentLength);

    return $"{scheme}://{domain}{portDisplay}{path}{query}{fragment}";
  }

  internal static class UrlConstants
  {
    /// <summary>
    ///   Gets the chars allowed in an unreserved.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     unreserved    = ALPHA / DIGIT / "-" / "." / "_" / "~"
    ///   </para>
    ///   <para>See https://datatracker.ietf.org/doc/html/rfc3986#appendix-A</para>
    /// </remarks>
    public static IReadOnlyList<char> UnreservedChars { get; } =
      CharCollections.LatinAlpha.Concat(CharCollections.Integers)
        .Concat(second: new[] { '-', '.', '_', '~' })
        .ToImmutableArray();

    /// <summary>
    ///   Gets the chars allowed in a pchar.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     pchar         = unreserved / pct-encoded / sub-delims / ":" / "@"
    ///   </para>
    ///   <para>
    ///     unreserved    = ALPHA / DIGIT / "-" / "." / "_" / "~"
    ///   </para>
    ///   <para>See https://datatracker.ietf.org/doc/html/rfc3986#appendix-A</para>
    /// </remarks>
    public static IReadOnlyList<char> PCharChars { get; } =
      UnreservedChars
        .Concat(second: new[] { ':', '@' })
        .ToImmutableArray();

    /// <summary>
    ///   Gets the chars allowed in a query.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     query       = *( pchar / "/" / "?" )
    ///   </para>
    ///   <para>
    ///     pchar         = unreserved / pct-encoded / sub-delims / ":" / "@"
    ///   </para>
    ///   <para>
    ///     unreserved    = ALPHA / DIGIT / "-" / "." / "_" / "~"
    ///   </para>
    ///   <para>See https://datatracker.ietf.org/doc/html/rfc3986#appendix-A</para>
    /// </remarks>
    public static IReadOnlyList<char> QueryChars { get; } =
      PCharChars
        .Concat(second: new[] { '/', '?' })
        .ToImmutableArray();

    /// <summary>
    ///   Gets the chars allowed in a fragment.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     fragment    = *( pchar / "/" / "?" )
    ///   </para>
    ///   <para>
    ///     pchar         = unreserved / pct-encoded / sub-delims / ":" / "@"
    ///   </para>
    ///   <para>
    ///     unreserved    = ALPHA / DIGIT / "-" / "." / "_" / "~"
    ///   </para>
    ///   <para>See https://datatracker.ietf.org/doc/html/rfc3986#appendix-A</para>
    /// </remarks>
    public static IReadOnlyList<char> FragmentChars { get; } =
      PCharChars
        .Concat(second: new[] { '/', '?' })
        .ToImmutableArray();
  }
}
