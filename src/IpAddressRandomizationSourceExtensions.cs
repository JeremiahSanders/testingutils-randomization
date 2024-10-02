namespace Jds.TestingUtils.Randomization;

/// <summary>
///   Methods extending <see cref="IRandomizationSource" /> to generate pseudo-random IP addresses.
/// </summary>
public static class IpAddressRandomizationSourceExtensions
{
  /// <summary>
  ///   Generates a pseudo-random IP v4 address in traditional dot-decimal notation (e.g., <c>127.0.0.1</c>).
  /// </summary>
  /// <param name="randomizationSource">This <see cref="IRandomizationSource" /> instance.</param>
  /// <param name="octet1">Optional. First octet of the IP v4 address.</param>
  /// <param name="octet2">Optional. Second octet of the IP v4 address.</param>
  /// <param name="octet3">Optional. Third octet of the IP v4 address.</param>
  /// <param name="octet4">Optional. Fourth octet of the IP v4 address.</param>
  /// <returns>An IP address.</returns>
  public static string IpV4(this IRandomizationSource randomizationSource, byte? octet1 = null,
    byte? octet2 = null, byte? octet3 = null, byte? octet4 = null)
  {
    return
      $"{octet1 ?? randomizationSource.Byte()}.{octet2 ?? randomizationSource.Byte()}.{octet3 ?? randomizationSource.Byte()}.{octet4 ?? randomizationSource.Byte()}";
  }

  /// <summary>
  ///   Generates a pseudo-random IP v6 address in
  ///   <a href="https://datatracker.ietf.org/doc/html/rfc1884#page-4">RFC-1884 address text representation format</a>.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     Example IP v6 addresses from <a href="https://datatracker.ietf.org/doc/html/rfc1884#page-4">RFC-1884</a>:
  ///     <c>FEDC:BA98:7654:3210:FEDC:BA98:7654:3210</c>, <c>1080:0:0:0:8:800:200C:417A</c>.
  ///   </para>
  /// </remarks>
  /// <param name="randomizationSource">This <see cref="IRandomizationSource" /> instance.</param>
  /// <param name="piece1">Optional. First 16-bit piece of the address.</param>
  /// <param name="piece2">Optional. Second 16-bit piece of the address.</param>
  /// <param name="piece3">Optional. Third 16-bit piece of the address.</param>
  /// <param name="piece4">Optional. Fourth 16-bit piece of the address.</param>
  /// <param name="piece5">Optional. Fifth 16-bit piece of the address.</param>
  /// <param name="piece6">Optional. Sixth 16-bit piece of the address.</param>
  /// <param name="piece7">Optional. Seventh 16-bit piece of the address.</param>
  /// <param name="piece8">Optional. Eighth 16-bit piece of the address.</param>
  /// <returns></returns>
  public static string IpV6(
    this IRandomizationSource randomizationSource,
    ushort? piece1 = null,
    ushort? piece2 = null,
    ushort? piece3 = null,
    ushort? piece4 = null,
    ushort? piece5 = null,
    ushort? piece6 = null,
    ushort? piece7 = null,
    ushort? piece8 = null
  )
  {
    return
      $"{Default(piece1):x4}:{Default(piece2):x4}:{Default(piece3):x4}:{Default(piece4):x4}:{Default(piece5):x4}:{Default(piece6):x4}:{Default(piece7):x4}:{Default(piece8):x4}";

    ushort Default(ushort? value)
    {
      return value ?? randomizationSource.UShort();
    }
  }
}
