using System;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.IpAddressRandomizationSourceExtensionsTests;

public class IpV4Tests
{
  [Fact]
  public void UsesParameters()
  {
    var octet1 = Randomizer.Shared.Byte();
    var octet2 = Randomizer.Shared.Byte();
    var octet3 = Randomizer.Shared.Byte();
    var octet4 = Randomizer.Shared.Byte();
    var expected = $"{octet1}.{octet2}.{octet3}.{octet4}";

    var actual = Randomizer.Shared.IpV4(octet1, octet2, octet3, octet4);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GivenSharedSource_GeneratesAddresses()
  {
    var actual = Randomizer.Shared.IpV4();

    // Convert method implicitly asserts value range
    var segments = actual.Split('.').Select(octet => Convert.ToByte(octet)).ToList();

    segments.Should().HaveCount(4);
  }
}
