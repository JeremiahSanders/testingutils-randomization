using System;
using System.Collections.Generic;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.ReadOnlyListExtensionsTests;

public static class GenerateRandomStringTests
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

        var actual = Strings.GenerateRandomString(_randomizer, expected).Length;

        Assert.Equal(expected, actual);
      }

      [Fact]
      public void GivenLength0_ReturnsEmptyString()
      {
        var actual = Strings.GenerateRandomString(_randomizer, 0);

        Assert.Equal(string.Empty, actual);
      }

      [Fact]
      public void GivenNoStrings_ThrowsArgumentException()
      {
        Assert.Throws<ArgumentException>(() =>
          Array.Empty<string>().GenerateRandomString(_randomizer, _randomizer.IntInRange(1, 5))
        );
      }
    }

    public class GivenNoRandomizer
    {
      [Fact]
      public void GeneratesExpectedLength()
      {
        var expected = Randomizer.Shared.IntInRange(5, 500);

        var actual = Strings.GenerateRandomString(expected).Length;

        Assert.Equal(expected, actual);
      }

      [Fact]
      public void GivenLength0_ReturnsEmptyString()
      {
        var actual = Strings.GenerateRandomString(0);

        Assert.Equal(string.Empty, actual);
      }

      [Fact]
      public void GivenNoStrings_ThrowsArgumentException()
      {
        Assert.Throws<ArgumentException>(() =>
          Array.Empty<string>().GenerateRandomString(Randomizer.Shared.IntInRange(1, 5))
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

        var actual = Chars.GenerateRandomString(_randomizer, expected).Length;

        Assert.Equal(expected, actual);
      }

      [Fact]
      public void GivenLength0_ReturnsEmptyString()
      {
        var actual = Chars.GenerateRandomString(_randomizer, 0);

        Assert.Equal(string.Empty, actual);
      }

      [Fact]
      public void GivenNoStrings_ThrowsArgumentException()
      {
        Assert.Throws<ArgumentException>(() =>
          Array.Empty<string>().GenerateRandomString(_randomizer, _randomizer.IntInRange(1, 5))
        );
      }
    }

    public class GivenNoRandomizer
    {
      [Fact]
      public void GeneratesExpectedLength()
      {
        var expected = Randomizer.Shared.IntInRange(5, 500);

        var actual = Chars.GenerateRandomString(expected).Length;

        Assert.Equal(expected, actual);
      }

      [Fact]
      public void GivenLength0_ReturnsEmptyString()
      {
        var actual = Chars.GenerateRandomString(0);

        Assert.Equal(string.Empty, actual);
      }

      [Fact]
      public void GivenNoStrings_ThrowsArgumentException()
      {
        Assert.Throws<ArgumentException>(() =>
          Array.Empty<char>().GenerateRandomString(Randomizer.Shared.IntInRange(1, 5))
        );
      }
    }
  }
}
