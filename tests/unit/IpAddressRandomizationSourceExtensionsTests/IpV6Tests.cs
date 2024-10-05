using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.IpAddressRandomizationSourceExtensionsTests;

public class IpV6Tests
{
  [Fact]
  public void UsesParameters()
  {
    var piece1 = Randomizer.Shared.UShort();
    var piece2 = Randomizer.Shared.UShort();
    var piece3 = Randomizer.Shared.UShort();
    var piece4 = Randomizer.Shared.UShort();
    var piece5 = Randomizer.Shared.UShort();
    var piece6 = Randomizer.Shared.UShort();
    var piece7 = Randomizer.Shared.UShort();
    var piece8 = Randomizer.Shared.UShort();
    var expected = $"{piece1:x4}:{piece2:x4}:{piece3:x4}:{piece4:x4}:{piece5:x4}:{piece6:x4}:{piece7:x4}:{piece8:x4}";

    var actual = Randomizer.Shared.IpV6(piece1, piece2, piece3, piece4, piece5, piece6, piece7, piece8);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GivenSharedSource_GeneratesAddresses()
  {
    var actual = Randomizer.Shared.IpV6();

    // Convert method implicitly asserts value range
    IReadOnlyList<ushort> segments = actual.Split(':').Select(value => Convert.ToUInt16(value, 16)).ToList();

    segments.Should().HaveCount(8);
  }
}
