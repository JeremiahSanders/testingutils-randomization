using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace Jds.TestingUtils.Randomization;

public static class MailAddressRandomizationSourceExtensions
{
  [ExcludeFromCodeCoverage]
  internal static class MailAddressConstants
  {
    /// <summary>
    ///   Max length of a mail address local part, per SMTP rules.
    /// </summary>
    /// <remarks>
    /// <para>See https://datatracker.ietf.org/doc/html/rfc2821#section-4.5.3.1</para>
    /// </remarks>
    public const int SmtpLocalPartMaxLength = 64;

    /// <summary>
    ///   Max length of a domain, per SMTP rules.
    /// </summary>
    /// <remarks>
    /// <para>See https://datatracker.ietf.org/doc/html/rfc2821#section-4.5.3.1</para>
    /// </remarks>
    public const int SmtpDomainMaxLength = 255;

    /// <summary>
    ///   Max length of a mail address.
    /// </summary>
    /// <remarks>
    /// <para>Original: https://datatracker.ietf.org/doc/html/rfc3696#section-3</para>
    /// <para>Errata: https://www.rfc-editor.org/errata_search.php?rfc=3696&amp;eid=1690</para>
    /// </remarks>
    public const int MailAddressMaxLength = 254;

    /// <summary>
    ///   Characters designated `NO-WS-CTL`.
    /// </summary>
    /// <remarks>
    /// <code>
    /// NO-WS-CTL       =       %d1-8 /         ; US-ASCII control characters
    ///                         %d11 /          ;  that do not include the
    ///                         %d12 /          ;  carriage return, line feed,
    ///                         %d14-31 /       ;  and white space characters
    ///                         %d127
    /// </code>
    /// <para>Source https://datatracker.ietf.org/doc/html/rfc2822#section-3.2.1</para>
    /// </remarks>
    public static IReadOnlyList<char> NoWsCtl { get; } = Enumerable.Range(1, 8)
      .Append(11)
      .Append(12)
      .Concat(Enumerable.Range(14, 31 - 14))
      .Append(127)
      .Select(Convert.ToChar)
      .ToImmutableArray();

    /// <summary>
    ///   Characters designated `qtext`.
    /// </summary>
    /// <remarks>
    /// <code>
    /// qtext           =       NO-WS-CTL /     ; Non white space controls
    ///                         %d33 /          ; The rest of the US-ASCII
    ///                         %d35-91 /       ;  characters not including "\"
    ///                         %d93-126        ;  or the quote character
    /// </code>
    /// </remarks>
    public static IReadOnlyList<char> QStringChars { get; } =
      new[] { '!' }
        .Concat(Enumerable.Range('#', '[' - '#').Select(value => (char)value))
        .Concat(Enumerable.Range(']', '~' - ']').Select(value => (char)value))
        .ToImmutableArray();

    /// <summary>
    ///   Characters designated `atext`.
    /// </summary>
    /// <remarks>
    /// <code>
    /// atext           =       ALPHA / DIGIT / ; Any character except controls,
    ///                         "!" / "#" /     ;  SP, and specials.
    ///                         "$" / "%" /     ;  Used for atoms
    ///                         "&amp;" / "'" /
    ///                         "*" / "+" /
    ///                         "-" / "/" /
    ///                         "=" / "?" /
    ///                         "^" / "_" /
    ///                         "`" / "{" /
    ///                         "|" / "}" /
    ///                         "~"
    /// </code>
    /// <para>Source https://datatracker.ietf.org/doc/html/rfc2822#section-3.2.4</para>
    /// </remarks>
    public static IReadOnlyList<char> ATextChars { get; } = CharCollections.LettersDigits.Concat(new[]
    {
      '!', '#', '$', '%', '&', '\'', '*', '+', '-', '/', '=', '?', '^', '_', '`', '{', '|', '}', '~'
    }).ToImmutableArray();

    /// <summary>
    ///   Characters allowed the tail of a `dot-atom-text`.
    /// </summary>
    /// <remarks>
    /// <code>
    /// dot-atom-text   =       1*atext *("." 1*atext)
    /// </code>
    /// <para>Source https://datatracker.ietf.org/doc/html/rfc2822#section-3.2.4</para>
    /// </remarks>
    public static IReadOnlyList<char> DotAtomTextTailChars { get; } = ATextChars.Append('.').ToImmutableArray();
  }

  private class AText
  {
    public char Value { get; }

    public AText(char value)
    {
      Value = value;
    }

    public static AText Random(IRandomizationSource randomizationSource)
    {
      return new AText(MailAddressConstants.ATextChars.GetRandomItem(randomizationSource));
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
      return Value.ToString();
    }
  }

  private class DotAtomText
  {
    public string Value { get; }

    public DotAtomText(string value)
    {
      Value = value;
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
      return Value;
    }


    [ExcludeFromCodeCoverage(Justification = "Private class. No thrown exceptions provides black-box testing.")]
    public static DotAtomText Of(AText aTextHead, char[] aTextDots)
    {
      if (aTextDots.Any(aTextDot => !MailAddressConstants.DotAtomTextTailChars.Contains(aTextDot)))
      {
        throw new ArgumentException("One or more invalid characters detected.", nameof(aTextDots));
      }

      return new DotAtomText($"{aTextHead.Value}{new string(aTextDots)}");
    }

    public static DotAtomText Random(IRandomizationSource randomizationSource, int length)
    {
      if (length < 1)
      {
        throw new ArgumentOutOfRangeException(nameof(length), "Length must be > 0");
      }

      return length switch
      {
        1 => Of(AText.Random(randomizationSource), Array.Empty<char>()),
        _ => Of(AText.Random(randomizationSource),
          randomizationSource.RandomString(length - 1, MailAddressConstants.DotAtomTextTailChars).ToCharArray()
        )
      };
    }
  }

  private class DotAtom
  {
    public string Value { get; }

    public DotAtom(string value)
    {
      Value = value;
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
      return Value;
    }

    public static DotAtom Of(DotAtomText dotAtomText)
    {
      return new DotAtom(dotAtomText.Value);
    }

    public static DotAtom Random(IRandomizationSource randomizationSource, int textLength)
    {
      return Of(DotAtomText.Random(randomizationSource, textLength));
    }
  }

  private class LocalPart
  {
    public string Value { get; }

    private LocalPart(string value)
    {
      Value = value;
    }

    public static LocalPart Of(DotAtom dotAtom)
    {
      return new LocalPart(dotAtom.Value);
    }

    public static LocalPart Random(IRandomizationSource randomizationSource, int length)
    {
      return Of(DotAtom.Random(randomizationSource, length));
    }
  }

  /// <summary>
  ///   Generates a `local-part`.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <remarks>
  ///   <para>See https://datatracker.ietf.org/doc/html/rfc2822#section-3.4.1</para>
  ///   <para>See https://datatracker.ietf.org/doc/html/rfc2821#section-4.5.3.1</para>
  /// </remarks>
  internal static string MailAddressAddrSpecLocalPart(this IRandomizationSource randomizationSource, int length)
  {
    if (length > MailAddressConstants.SmtpLocalPartMaxLength)
    {
      throw new ArgumentOutOfRangeException(nameof(length),
        $"Maximum local part length is {MailAddressConstants.SmtpLocalPartMaxLength} characters, per RFC 2821, Section 4.5.3.1.");
    }

    return LocalPart.Random(randomizationSource, length).Value;
  }

  /// <summary>
  ///   Generates a `domain`.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <remarks>
  ///   <para>See https://datatracker.ietf.org/doc/html/rfc2822#section-3.4.1</para>
  ///   <para>See https://datatracker.ietf.org/doc/html/rfc2821#section-4.5.3.1</para>
  /// </remarks>
  internal static string MailAddressAddrSpecDomain(this IRandomizationSource randomizationSource, int length)
  {
    if (length > MailAddressConstants.SmtpDomainMaxLength)
    {
      throw new ArgumentOutOfRangeException(nameof(length),
        $"Maximum domain length is {MailAddressConstants.SmtpDomainMaxLength} characters, per RFC 2821, Section 4.5.3.1."
      );
    }

    return randomizationSource.DomainName(length);
  }

  /// <summary>
  ///   Generates a pseudo-random mail address compliant with `addr-spec` of RFC-2822.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     While values generated should be compliant with `addr-spec` defined in RFC-2822 section 3.4.1, they do not
  ///     exercise all possibilities.
  ///   </para>
  ///   <para>
  ///     Generated `local-part` does not generate `quoted-string` or `obs-local-part` values.
  ///   </para>
  ///   <para>
  ///     Generated `domain` returns a RFC-1035 <see
  ///       cref="DomainRandomizationSourceExtensions.DomainName(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyList{int})"
  ///     />. RFC-2822 allows many `domain` values which do not comply with RFC-1035. E.g., "----------" and "/" are valid
  ///     `domain-literal` values.
  ///   </para>
  ///   <para>See https://datatracker.ietf.org/doc/html/rfc2822#section-3.4.1</para>
  /// </remarks>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="componentLengths"></param>
  /// <returns>A pseudo-random mail address compliant with `addr-spec` of RFC-2822.</returns>
  /// <exception cref="ArgumentOutOfRangeException">
  ///   Thrown when the local part length is less than 1 or greater than 64, when the domain length is less than 1 or
  ///   greater than 255, or when the sum of local and domain lengths is greater than 253. (Max mail address length,
  ///   including @ sign, must be 254 characters or less.)
  /// </exception>
  public static string MailAddressAddrSpec(this IRandomizationSource randomizationSource,
    (int LocalPartLength, int DomainLength) componentLengths)
  {
    // Extra +1 below accounts for "@".
    if (componentLengths.LocalPartLength + componentLengths.DomainLength + 1 >
        MailAddressConstants.MailAddressMaxLength)
    {
      throw new ArgumentOutOfRangeException(
        nameof(componentLengths),
        $"Maximum mail address length is {MailAddressConstants.MailAddressMaxLength}, per errata to RFC-3696."
      );
    }

    var localPart = randomizationSource.MailAddressAddrSpecLocalPart(componentLengths.LocalPartLength);
    var domainHost = randomizationSource.MailAddressAddrSpecDomain(componentLengths.DomainLength);

    return $"{localPart}@{domainHost}";
  }

  /// <summary>
  ///   Generates a pseudo-random mail address compliant with `addr-spec` of RFC-2822.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     While values generated should be compliant with `addr-spec` defined in RFC-2822 section 3.4.1, they do not
  ///     exercise all possibilities.
  ///   </para>
  ///   <para>
  ///     Generated `local-part` does not generate `quoted-string` or `obs-local-part` values.
  ///   </para>
  ///   <para>
  ///     Generated `domain` returns a RFC-1035 <see
  ///       cref="DomainRandomizationSourceExtensions.DomainName(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyList{int})"
  ///     />. RFC-2822 allows many `domain` values which do not comply with RFC-1035. E.g., "----------" and "/" are valid
  ///     `domain-literal` values.
  ///   </para>
  ///   <para>See https://datatracker.ietf.org/doc/html/rfc2822#section-3.4.1</para>
  /// </remarks>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length">An address length.</param>
  /// <returns>A pseudo-random mail address compliant with `addr-spec` of RFC-2822.</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="length"/> is less than 3 or greater than 254.</exception>
  public static string MailAddressAddrSpec(this IRandomizationSource randomizationSource, int length)
  {
    switch (length)
    {
      case > MailAddressConstants.MailAddressMaxLength:
        throw new ArgumentOutOfRangeException(
          nameof(length),
          $"Maximum mail address length is {MailAddressConstants.MailAddressMaxLength}, per errata to RFC-3696."
        );
      case < 3:
        throw new ArgumentOutOfRangeException(nameof(length),
          "Mail address must be at least three characters: local part, @ sign, domain"
        );
    }

    var localPartLength = randomizationSource.IntInRange(1,
      Math.Clamp(length - 2, 1, MailAddressConstants.SmtpLocalPartMaxLength)
    );
    var domainLength = length - 1 - localPartLength;

    return randomizationSource.MailAddressAddrSpec((localPartLength, domainLength));
  }

  /// <summary>
  ///   Generates a pseudo-random <see cref="System.Net.Mail.MailAddress"/> using <see cref="MailAddressAddrSpec(Jds.TestingUtils.Randomization.IRandomizationSource, int)"/>
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <param name="length">An address length.</param>
  /// <returns>A <see cref="System.Net.Mail.MailAddress"/>.</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="length"/> is less than 3 or greater than 254.</exception>
  public static MailAddress MailAddress(this IRandomizationSource randomizationSource, int length)
  {
    return new MailAddress(randomizationSource.MailAddressAddrSpec(length));
  }

  /// <summary>
  ///   Generates a pseudo-random <see cref="System.Net.Mail.MailAddress"/> using <see cref="MailAddressAddrSpec(Jds.TestingUtils.Randomization.IRandomizationSource, int)"/>
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" /> providing values.</param>
  /// <returns>A <see cref="System.Net.Mail.MailAddress"/>.</returns>
  public static MailAddress MailAddress(this IRandomizationSource randomizationSource)
  {
    return new MailAddress(
      randomizationSource.MailAddressAddrSpec(
        randomizationSource.IntInRange(3, MailAddressConstants.MailAddressMaxLength)
      )
    );
  }
}
