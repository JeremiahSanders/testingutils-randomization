using System;
using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.StringRandomizationSourceExtensionsTests;

public static class RandomStringTests
{
  public static class StringList
  {
    private static IReadOnlyList<string> Strings { get; } = new[] { "ab", "am", "an", "ak", "pa", "ra", "ma" };

    public class GivenSpecifiedRandomizer
    {
      private readonly RandomRandomizationSource _randomizer;

      public GivenSpecifiedRandomizer()
      {
        _randomizer = new RandomRandomizationSource(new Random());
      }

      [Fact]
      public void GeneratesExpectedLength()
      {
        var expected = _randomizer.IntInRange(5, 500);

        var actual = _randomizer.RandomString(expected, Strings).Length;

        actual.Should().Be(expected);
      }

      [Fact]
      public void GivenLength0_ReturnsEmptyString()
      {
        var actual = _randomizer.RandomString(0, Strings);

        actual.Should().BeEmpty();
      }

      [Fact]
      public void GivenNoStrings_ThrowsArgumentException()
      {
        Assert.Throws<ArgumentException>(() =>
          _randomizer.RandomString(_randomizer.IntInRange(1, 5), Array.Empty<string>())
        );
      }
    }
  }

  public static class CharList
  {
    private static IReadOnlyList<char> Chars { get; } = new[] { '_', ' ', '\n', '5', '%' };

    public class GivenSpecifiedRandomizer
    {
      private readonly RandomRandomizationSource _randomizer;

      public GivenSpecifiedRandomizer()
      {
        _randomizer = new RandomRandomizationSource(new Random());
      }

      [Fact]
      public void GeneratesExpectedLength()
      {
        var expected = _randomizer.IntInRange(5, 500);

        var actual = _randomizer.RandomString(expected, Chars).Length;

        actual.Should().Be(expected);
      }

      [Fact]
      public void GivenLength0_ReturnsEmptyString()
      {
        var actual = _randomizer.RandomString(0, Chars);

        actual.Should().BeEmpty();
      }

      [Fact]
      public void GivenNoStrings_ThrowsArgumentException()
      {
        Assert.Throws<ArgumentException>(() =>
          _randomizer.RandomString(_randomizer.IntInRange(1, 5), Array.Empty<string>())
        );
      }
    }
  }
}
