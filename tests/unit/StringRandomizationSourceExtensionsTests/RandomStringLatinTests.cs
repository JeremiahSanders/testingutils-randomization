using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.StringRandomizationSourceExtensionsTests;

public class RandomStringLatinTests
{
  private readonly ITestOutputHelper _outputHelper;

  public RandomStringLatinTests(ITestOutputHelper outputHelper)
  {
    _outputHelper = outputHelper;
  }

  private static IReadOnlyList<char> NumericChars { get; } = Enumerable.Range('0', 10).Select(Convert.ToChar).ToArray();

  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] GenerateTestCase(bool uppercase, bool alphanumeric)
    {
      return new object[] { uppercase, alphanumeric };
    }

    return new[]
    {
      GenerateTestCase(false, false), GenerateTestCase(false, true), GenerateTestCase(true, false),
      GenerateTestCase(true, true)
    };
  }

  [Theory]
  [MemberData(nameof(CreateTestCases))]
  public void GeneratesExpectedStringTypes(bool uppercase, bool alphanumeric)
  {
    void CheckResult(string actual)
    {
      Assert.Equal(uppercase ? actual.ToUpperInvariant() : actual.ToLowerInvariant(), actual);

      if (!alphanumeric)
      {
        foreach (var charValue in actual)
        {
          Assert.DoesNotContain(charValue, NumericChars);
        }
      }
    }

    var actual = RandomRandomizationSource.Shared.RandomStringLatin(
      Randomizer.Shared.IntInRange(100, 1000),
      uppercase,
      alphanumeric
    );

    _outputHelper.WriteLine($"Checking for {actual}");
    CheckResult(actual);
  }
}
